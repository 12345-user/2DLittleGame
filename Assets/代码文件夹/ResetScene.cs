using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScene : MonoBehaviour
{
    public CameraControl cameraControl;
    public GameObject illustrated;
    public AudioSource[] allAudioSource;
    public GameObject[] allDialogBox;
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

    }
}
