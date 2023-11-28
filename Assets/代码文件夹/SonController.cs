using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonController : MonoBehaviour
{
    public MeshControl[] PeopleInIllustrations;
    public MeshControl[] OutLander;
    public GameObject finishGameObject;
    private bool showFinish = true;
    // Start is called before the first frame update
    public void ResetMesh()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform GrandSon in child)
            {
                GrandSon.gameObject.GetComponent<MeshControl>().ChangeToInit();
            }
        }
    }

    public void UnlockAllMesh()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform GrandSon in child)
            {
                GrandSon.gameObject.GetComponent<MeshControl>().ChangeToFini();
            }
        }
    }

    public void ShowTheSons(int i)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(i).gameObject.SetActive(true);
    

    }

    //Get the Right choose.
    public void ChangeTheMesh(int i)
    {
        PeopleInIllustrations[i].GetComponent<MeshControl>().ChangeToFini();
    }

    public void JudgeAll()
    {
        bool allchange = true;
        for (int i = 0; i < OutLander.Length; i++)
        {
            if (OutLander[i].CurrentMeshInit())
            {
                allchange = false;
                //Debug.Log("JudgeAll()�м�⵽��û�м�����ͼ��:" + i);
            }
        }
        if (allchange == true && showFinish == true)
        {
            finishGameObject.gameObject.SetActive(true);
            showFinish = false;
        }

    }
    public void DisAppearTheSons()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}