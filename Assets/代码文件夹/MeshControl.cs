using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public bool CurrentMeshFini()
    {
        meshRender = this.GetComponent<MeshRenderer>();
        Debug.Log(meshRender.material.name + " ," + Fini_mesh.name + " , " + meshRender.material.name.Replace(" (Instance)", "") == Fini_mesh.name);
        return meshRender.material.name.Replace(" (Instance)", "") == Fini_mesh.name;
    }
    public bool CurrentMeshInit()
    {
        meshRender = this.GetComponent<MeshRenderer>();
        Debug.Log("CurrentMeshInit(): " + meshRender.material.name + " ," + Init_mesh.name + " , " + (meshRender.material.name.Replace(" (Instance)", "") == Init_mesh.name));
        return meshRender.material.name.Replace(" (Instance)", "") == Init_mesh.name;
    }

}