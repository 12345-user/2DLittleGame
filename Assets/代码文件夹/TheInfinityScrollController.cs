
using UnityEngine;
using UnityEngine.UI;

namespace ThisXHGame
{
    /// <summary>
    /// ���޻���������
    /// </summary>
    public class TheInfinityScrollController : MonoBehaviour
    {
        InfinityGridLayoutGroup infinityGridLayoutGroup;

        void Start()
        {
            //��ʼ�������б�;
            infinityGridLayoutGroup =
GameObject.FindObjectOfType<InfinityGridLayoutGroup>();

            infinityGridLayoutGroup.updateChildrenCallback = UpdateChildrenCallback;
            for (int i = 0; i < infinityGridLayoutGroup.transform.childCount; i++)
            {
                Transform child = infinityGridLayoutGroup.transform.GetChild(i);
                child.GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnClickButtonWithIndex(child.GetComponentInChildren<Text>());
                });
            }

            infinityGridLayoutGroup.InitSetAmount(100);
        }


        /// <summary>
        /// ͨ����ǰ����ͼ������ios����ȡԭͼ
        /// </summary>
        void OnClickButtonWithIndex(Text tex)
        {
            Debug.Log($" Unity log:     index is {tex.text} in your click button...");
        }

        /// <summary>
        /// ���·������º���
        /// </summary>
        void UpdateChildrenCallback(int indx, Transform trans)
        {
            Text tex = trans.Find("Text").GetComponent<Text>();
            tex.text = indx.ToString();
        }
    }
}