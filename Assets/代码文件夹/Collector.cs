using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    public SonController PersonModelControl;
    public int Num;
    private bool[] B1 = {true,true,false,true,false,false,true,true,true,false,false,true,false,false,false };

    public AudioSource[] RightAndWrong;
    private AudioSource ToPlay;

    public void GetNum(int N)
    {
        Num = N;
    }

    public void JudgeRight(bool input)
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
}
