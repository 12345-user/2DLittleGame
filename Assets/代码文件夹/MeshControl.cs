using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshControl : MonoBehaviour
{
    private MeshRenderer meshRender;
    public Material Init_mesh;
    public Material Fini_mesh;

    // Update is called once per frame
    void Start()
    {
        meshRender = this.GetComponent<MeshRenderer>();  //把该物体的组件赋值到声明的变量中
    }

    public void ChangeToFini()
    {
        meshRender.material = Fini_mesh;
    }
    public void ChangeToInit()
    {
        meshRender.material = Init_mesh;
    }

}
