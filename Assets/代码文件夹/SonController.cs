using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonController : MonoBehaviour
{
    public MeshControl[] PeopleInIllustrations;
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
}