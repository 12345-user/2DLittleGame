using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshControl : MonoBehaviour
{
    private MeshRenderer meshRender;
    public Material Init_mesh;
    public Material Fini_mesh;

    public void ChangeToFini()
    {
        meshRender = this.GetComponent<MeshRenderer>(); 
        meshRender.material = Fini_mesh;
    }
    public void ChangeToInit()
    {
        meshRender = this.GetComponent<MeshRenderer>();
        meshRender.material = Init_mesh;
    }

}