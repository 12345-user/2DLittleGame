
using UnityEngine;
using UnityEngine.UI;

namespace ThisXHGame
{
    /// <summary>
    /// 无限滑动控制器
    /// </summary>
    public class TheInfinityScrollController : MonoBehaviour
    {
        InfinityGridLayoutGroup infinityGridLayoutGroup;

        void Start()
        {
            //初始化数据列表;
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
        /// 通过当前缩略图索引从ios相册获取原图
        /// </summary>
        void OnClickButtonWithIndex(Text tex)
        {
            Debug.Log($" Unity log:     index is {tex.text} in your click button...");
        }

        /// <summary>
        /// 上下翻滚更新函数
        /// </summary>
        void UpdateChildrenCallback(int indx, Transform trans)
        {
            Text tex = trans.Find("Text").GetComponent<Text>();
            tex.text = indx.ToString();
        }
    }
}