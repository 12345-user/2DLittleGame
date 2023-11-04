using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    public SonController PersonModelControl;
    public int Num;
    private bool[] B1 = {true,true,false,true,false,false,true,true,true,false,false,true,false,false,false };
    private bool[] B2 = {true,false,false,false,false,true,false,false,true,false,true,false,true,false,true,false,false,true,true,true,false,true,true,true,false,false,};
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
        ToPlay.Play();
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
        Debug.Log("Toplay获取：" + ToPlay.name);
        ToPlay.Play();
        Debug.Log("人物序号：" + Num + " 读取值： " + B2[Num] + " 实际判断：" + input);
    }
}
