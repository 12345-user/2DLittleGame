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

        //������mesh����
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

    public void CloseDialogBoxAfterFiveSeconds(int i)
    
    {
        StartCoroutine(DelayMethod(5,i));
    }

    IEnumerator DelayMethod(float delayTime,int i)
    {
    //受到TimeScale影响
        yield return new WaitForSeconds(delayTime);
        allDialogBox[i].SetActive(false);

    }
}
