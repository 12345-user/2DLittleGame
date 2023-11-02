using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThisXHGame
{
    /// <summary>
    /// ���޻���
    /// </summary>
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(ContentSizeFitter))]
    public class InfinityGridLayoutGroup : MonoBehaviour
    {
        /*  Ҫȷ����ǰ����canvas��rect�·�scale���ű�����Ҫ��֮����һ����  */
        float _scale = 0.061f;

        /* ʵ�����޹�������Ҫ�����ٵ�child������
         * ��Ļ���ܿ�����+һ�п������ģ�����������Ļ���ܿ��� 4 �У�ÿһ�� 3 ����
         * �����ֵΪ 4�� * 3�� + 1�� * 3�� = 15����*/
        int childrenAmount = 0;

        #region Private Attribute
        ScrollRect scrollRect;
        RectTransform rectTransform;
        GridLayoutGroup gridLayoutGroup;
        ContentSizeFitter contentSizeFitter;
        List<RectTransform> children = new List<RectTransform>();

        int amount = 0;
        int constraintCount;
        int realIndex = -1;

        float childHeight;

        bool hasInit = false;
        Vector2 startPosition;
        Vector2 gridLayoutSize;
        Vector2 gridLayoutPos;
        Dictionary<Transform, Vector2> childsAnchoredPosition = new Dictionary<Transform, Vector2>();
        Dictionary<Transform, int> childsSiblingIndex = new Dictionary<Transform, int>();
        #endregion

        public delegate void UpdateChildrenCallbackDelegate(int index, Transform trans);
        public UpdateChildrenCallbackDelegate updateChildrenCallback = null;

        void Start() => childrenAmount = transform.childCount;

        IEnumerator InitChildren()
        {
            yield return 0;

            if (!hasInit)
            {
                //��ȡGrid�Ŀ��;
                rectTransform = GetComponent<RectTransform>();

                gridLayoutGroup = GetComponent<GridLayoutGroup>();
                gridLayoutGroup.enabled = false;
                constraintCount = gridLayoutGroup.constraintCount;
                childHeight = gridLayoutGroup.cellSize.y;
                contentSizeFitter = GetComponent<ContentSizeFitter>();
                contentSizeFitter.enabled = false;

                gridLayoutPos = rectTransform.anchoredPosition;
                gridLayoutSize = rectTransform.sizeDelta;


                //ע��ScrollRect�����ص�;
                scrollRect = transform.parent.GetComponent<ScrollRect>();
                scrollRect.onValueChanged.AddListener((data) => { ScrollCallback(data); });

                //��ȡ����child anchoredPosition �Լ� SiblingIndex;
                for (int index = 0; index < childrenAmount; index++)
                {
                    Transform child = transform.GetChild(index);
                    RectTransform childRectTrans = child.GetComponent<RectTransform>();
                    childsAnchoredPosition.Add(child, childRectTrans.anchoredPosition);
                    childsSiblingIndex.Add(child, child.GetSiblingIndex());
                }
            }
            else
            {
                rectTransform.anchoredPosition = gridLayoutPos;
                rectTransform.sizeDelta = gridLayoutSize;

                children.Clear();

                realIndex = -1;

                //children������������˳��;
                foreach (var info in childsSiblingIndex)
                {
                    info.Key.SetSiblingIndex(info.Value);
                }

                //children��������anchoredPosition;
                for (int index = 0; index < childrenAmount; index++)
                {
                    Transform child = transform.GetChild(index);

                    RectTransform childRectTrans = child.GetComponent<RectTransform>();
                    if (childsAnchoredPosition.ContainsKey(child))
                    {
                        childRectTrans.anchoredPosition = childsAnchoredPosition[child];
                    }
                    else
                    {
                        Debug.LogError("Unity Error Log : childs Anchored Position are no contain " + child.name);
                    }
                }
            }

            //int needCount = (minAmount < amount) ? minAmount : amount;
            //��ȡ����child;
            for (int _idx = 0; _idx < childrenAmount; _idx++)
            {
                Transform child = transform.GetChild(_idx);
                child.gameObject.SetActive(true);

                RectTransform rect = child.GetComponent<RectTransform>();
                children.Add(rect);

                //��ʼ��ǰ�漸��;
                if (_idx < amount)
                {
                    UpdateChildrenInfoCallback(_idx, child);
                }
            }

            startPosition = rectTransform.anchoredPosition;

            realIndex = children.Count - 1;

            //Debug.Log( scrollRect.transform.TransformPoint(Vector3.zero));
            //Debug.Log(transform.TransformPoint(children[0].localPosition));

            hasInit = true;

            //�����Ҫ��ʾ�ĸ���С���趨�ĸ���;
            for (int index = 0; index < childrenAmount; index++)
            {
                children[index].gameObject.SetActive(index < amount);
            }

            if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
            {
                //���С��һ�У�����Ҫ��GridLayout�ĸ߶ȼ�ȥһ�еĸ߶�;
                int row = (childrenAmount - amount) / constraintCount;
                //Debug.Log($"---------minAmount = {minAmount}----amount = {amount}-----constraintCount = {constraintCount}-------row = {row}--- ");
                if (row > 0)
                {
                    rectTransform.sizeDelta -= new Vector2(0, (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y) * row);
                }
            }
            else
            {
                //���С��һ�У�����Ҫ��GridLayout�Ŀ�ȼ�ȥһ�еĿ��;
                int column = (childrenAmount - amount) / constraintCount;
                if (column > 0)
                {
                    rectTransform.sizeDelta -= new Vector2((gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x) * column, 0);
                }
            }
        }

        /// <summary>
        /// �����ܵĸ���;
        /// </summary>
        /// <param name="count">�ܸ���</param>
        public void InitSetAmount(int count)
        {
            amount = count;
            StartCoroutine(InitChildren());
        }

        /// <summary>
        /// �����ص�
        /// </summary>
        void ScrollCallback(Vector2 data)
        {
            if (data.y >= 1.0f)
                return;
            UpdateChildrenInfo();
        }

        /// <summary>
        /// ������ĸ���
        /// </summary>
        void UpdateChildrenInfo()
        {
            if (childrenAmount < transform.childCount)
                return;

            Vector2 currentPos = rectTransform.anchoredPosition;

            if (gridLayoutGroup.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
            {
                float offsetY = currentPos.y - startPosition.y;

                //Debug.Log("offsetY is " + (offsetY > 0.0f));
                if (offsetY > 0)
                {
                    //��������������չ;
                    {
                        if (realIndex >= amount - 1)
                        {
                            startPosition = currentPos;
                            return;
                        }

                        float scrollRectUp = scrollRect.transform.TransformPoint(Vector3.zero).y;

                        Vector3 childBottomLeft = new Vector3(children[0].anchoredPosition.x, children[0].anchoredPosition.y - gridLayoutGroup.cellSize.y, 0f);
                        float childBottom = transform.TransformPoint(childBottomLeft).y;

                        if (childBottom >= scrollRectUp + childHeight * _scale)
                        {
                            //�ƶ����ײ�;
                            for (int index = 0; index < constraintCount; index++)
                            {
                                children[index].SetAsLastSibling();

                                children[index].anchoredPosition = new Vector2(children[index].anchoredPosition.x, children[children.Count - 1].anchoredPosition.y - gridLayoutGroup.cellSize.y - gridLayoutGroup.spacing.y);

                                realIndex++;

                                if (realIndex > amount - 1)
                                {
                                    children[index].gameObject.SetActive(false);
                                }
                                else
                                {
                                    UpdateChildrenInfoCallback(realIndex, children[index]);
                                }
                            }

                            //GridLayoutGroup �ײ��ӳ�;
                            rectTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);

                            //����child;
                            for (int index = 0; index < children.Count; index++)
                            {
                                children[index] = transform.GetChild(index).GetComponent<RectTransform>();
                            }
                        }
                    }
                }
                else
                {
                    //����������������;
                    if (realIndex + 1 <= children.Count)
                    {
                        startPosition = currentPos;
                        return;
                    }
                    RectTransform scrollRectTransform = scrollRect.GetComponent<RectTransform>();
                    Vector3 scrollRectAnchorBottom = new Vector3(0, -scrollRectTransform.rect.height - gridLayoutGroup.spacing.y, 0f);
                    float scrollRectBottom = scrollRect.transform.TransformPoint(scrollRectAnchorBottom).y;

                    Vector3 childUpLeft = new Vector3(children[children.Count - 1].anchoredPosition.x, children[children.Count - 1].anchoredPosition.y, 0f);

                    float childUp = transform.TransformPoint(childUpLeft).y;

                    if (childUp < scrollRectBottom)
                    {
                        //�ѵײ���һ�� �ƶ�������
                        for (int index = 0; index < constraintCount; index++)
                        {
                            children[children.Count - 1 - index].SetAsFirstSibling();

                            children[children.Count - 1 - index].anchoredPosition = new Vector2(children[children.Count - 1 - index].anchoredPosition.x, children[0].anchoredPosition.y + gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);

                            children[children.Count - 1 - index].gameObject.SetActive(true);

                            UpdateChildrenInfoCallback(realIndex - children.Count - index, children[children.Count - 1 - index]);
                        }

                        realIndex -= constraintCount;

                        //GridLayoutGroup �ײ�����;
                        rectTransform.sizeDelta -= new Vector2(0, gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);

                        //����child;
                        for (int index = 0; index < children.Count; index++)
                        {
                            children[index] = transform.GetChild(index).GetComponent<RectTransform>();
                        }
                    }
                }
            }
            else
            {
                float offsetX = currentPos.x - startPosition.x;

                if (offsetX < 0)
                {
                    //��������������չ;
                    {
                        if (realIndex >= amount - 1)
                        {
                            startPosition = currentPos;
                            return;
                        }

                        float scrollRectLeft = scrollRect.transform.TransformPoint(Vector3.zero).x;

                        Vector3 childBottomRight = new Vector3(children[0].anchoredPosition.x + gridLayoutGroup.cellSize.x, children[0].anchoredPosition.y, 0f);
                        float childRight = transform.TransformPoint(childBottomRight).x;

                        if (childRight <= scrollRectLeft)
                        {
                            //�ƶ����ұ�;
                            for (int index = 0; index < constraintCount; index++)
                            {
                                children[index].SetAsLastSibling();

                                children[index].anchoredPosition = new Vector2(children[children.Count - 1].anchoredPosition.x + gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x, children[index].anchoredPosition.y);

                                realIndex++;

                                if (realIndex > amount - 1)
                                {
                                    children[index].gameObject.SetActive(false);
                                }
                                else
                                {
                                    UpdateChildrenInfoCallback(realIndex, children[index]);
                                }
                            }

                            //GridLayoutGroup �Ҳ�ӳ�;
                            rectTransform.sizeDelta += new Vector2(gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x, 0);

                            //����child;
                            for (int index = 0; index < children.Count; index++)
                            {
                                children[index] = transform.GetChild(index).GetComponent<RectTransform>();
                            }
                        }
                    }
                }
                else
                {
                    //���������ұ�����;
                    if (realIndex + 1 <= children.Count)
                    {
                        startPosition = currentPos;
                        return;
                    }
                    RectTransform scrollRectTransform = scrollRect.GetComponent<RectTransform>();
                    Vector3 scrollRectAnchorRight = new Vector3(scrollRectTransform.rect.width + gridLayoutGroup.spacing.x, 0, 0f);
                    float scrollRectRight = scrollRect.transform.TransformPoint(scrollRectAnchorRight).x;

                    Vector3 childUpLeft = new Vector3(children[children.Count - 1].anchoredPosition.x, children[children.Count - 1].anchoredPosition.y, 0f);

                    float childLeft = transform.TransformPoint(childUpLeft).x;

                    if (childLeft >= scrollRectRight)
                    {
                        //���ұߵ�һ�� �ƶ������;
                        for (int index = 0; index < constraintCount; index++)
                        {
                            children[children.Count - 1 - index].SetAsFirstSibling();

                            children[children.Count - 1 - index].anchoredPosition = new Vector2(children[0].anchoredPosition.x - gridLayoutGroup.cellSize.x - gridLayoutGroup.spacing.x, children[children.Count - 1 - index].anchoredPosition.y);

                            children[children.Count - 1 - index].gameObject.SetActive(true);

                            UpdateChildrenInfoCallback(realIndex - children.Count - index, children[children.Count - 1 - index]);
                        }

                        //GridLayoutGroup �Ҳ�����;
                        rectTransform.sizeDelta -= new Vector2(gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x, 0);

                        //����child;
                        for (int index = 0; index < children.Count; index++)
                        {
                            children[index] = transform.GetChild(index).GetComponent<RectTransform>();
                        }

                        realIndex -= constraintCount;
                    }
                }
            }

            startPosition = currentPos;
        }

        /// <summary>
        /// ���»ص�
        /// </summary>
        /// <param name="index">��ǰ����</param>
        /// <param name="trans">��ǰ����</param>
        void UpdateChildrenInfoCallback(int index, Transform trans)
        {
            updateChildrenCallback?.Invoke(index, trans);
        }
    }
}