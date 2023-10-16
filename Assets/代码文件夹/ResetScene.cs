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
        //��Ƶ�رգ�
        if (allAudioSource.Length > 0)
        {
            foreach (var audioSource in allAudioSource)
            {
                audioSource.Stop();
            }
        }

        //ͼ���ͶԻ���رգ�
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

        //�������λ��
        if (cameraControl != null)
        { 
            cameraControl.RestoreUI_x(); 
        }
        //��Ӧ��������
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
