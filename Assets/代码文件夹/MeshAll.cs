using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAll : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetMesh()
    {

        foreach (Transform child in transform)
        {
            child.GetComponent<MeshControl>().ChangeToInit();
        }
    }
}