using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScene : MonoBehaviour
{
    public CameraControl cameraControl;
    public GameObject illustrated;
    public AudioSource[] allAudioSource;
    public GameObject[] allDialogBox;
    public MeshControl[] AllScenePeopleMesh;
    public GameObject OpenScene;
    public GameObject CloseScene;
    


    public void resetsence()
    {
        //音频关闭：
        if (allAudioSource.Length > 0)
        {
            foreach (var audioSource in allAudioSource)
            {
                audioSource.Stop();
            }
        }

        //图鉴和对话框关闭：
        if(illustrated != null)
        {
            illustrated.SetActive(false);
        }

        if (allDialogBox.Length > 0)
        {
            foreach (var diologbox in allDialogBox)
            {
                diologbox.SetActive(false);
            }
        }

        //摄像机复位：
        if (cameraControl != null)
        { 
            cameraControl.RestoreUI_x(); 
        }
        //对应场景开关
        if(OpenScene != null)
        {
            OpenScene.SetActive(true);
        }
        if (CloseScene != null)
        {
            CloseScene.SetActive(false);
        }

        //场景内mesh重置
        if (AllScenePeopleMesh.Length > 0)
        {
            foreach (var PeopleMesh in AllScenePeopleMesh)
            {
                PeopleMesh.ChangeToInit();
            }
        }
    }

    public void StopOtherAudio(int Keep)
    {
        for (int i = 0; i < allAudioSource.Length; i++)
        {
            if (i != Keep)
            {
                allAudioSource[i].Stop();
            }
            else
            {
                allAudioSource[i].Play();
            }
        }
    }
    public void StopOtherMesh(int Keep)
    {
        for (int i = 0; i < AllScenePeopleMesh.Length; i++)
        {
            if (i != Keep)
            {
                AllScenePeopleMesh[i].ChangeToInit();
            }
            else
            {
                AllScenePeopleMesh[i].ChangeToFini();
            }
        }
    }

}
