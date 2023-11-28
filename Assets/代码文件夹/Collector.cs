 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    public SonController PersonModelControl;
    public int Num;
    public GameObject[] AllPeopleInScene;
    private bool[] B1 = {true,true,false,true,false,false,true,true,true,true,true,false,true,false,false,true };
    private bool[] B2 = {true,false,false,false,true,true,true,true,true,false,true,false,true,false,true,false,true,true,true,true,false,true,true,false,false};
    public AudioSource[] RightAndWrong;
    public AudioSource ToPlay;


    public void GetNum(int N)
    {
        Num = N;
    }

    public void JudgeRight1(bool input)
    {
        if (input == B1[Num])
        {
            PersonModelControl.ChangeTheMesh(Num);
            ToPlay = RightAndWrong[0];
        }
        else
        {
            ToPlay = RightAndWrong[1];
        }
        AllPeopleInScene[Num].GetComponent<MeshControl>().ChangeToInit();
        AllPeopleInScene[Num].GetComponent<AudioSource>().Stop();
        Debug.Log("Toplay��ȡ��" + ToPlay.name);
        ToPlay.Play();
        Debug.Log("������ţ�" + Num + " ��ȡֵ�� " + B1[Num] + " ʵ���жϣ�" + input);
    }
    public void JudgeRight2(bool input)
    {
        if (input == B2[Num])
        {
            PersonModelControl.ChangeTheMesh(Num);
            ToPlay = RightAndWrong[0];
        }
        else
        {
            ToPlay = RightAndWrong[1];
        }
        AllPeopleInScene[Num].GetComponent<MeshControl>().ChangeToInit();
        AllPeopleInScene[Num].GetComponent<AudioSource>().Stop();
        Debug.Log("Toplay" + ToPlay.name);
        ToPlay.Play();
        Debug.Log("对应号码" + Num + " 预期判定 " + B2[Num] + "实际判定" + input);
    }


    
}
