using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform CameraTransform;
    public float CameraMoveSpeed;
    public int CameraMoveDistanceLimit;
    public GameObject[] UIList;
    public float[] UI_x;
    // Update is called once per frame
    void Awake()
    {
        for (int i = 0; i < UIList.Length; i++)
        {
            UI_x[i] = UIList[i].transform.position.x;
        }
    }

    void Update()
    {
        MoveUI_x();
    }
    public void MoveUI_x()
    {
        if (Input.GetKey(KeyCode.D) && CameraTransform.position.x < CameraMoveDistanceLimit)
        {
            {
                // Move the camera along the x-axis
                CameraTransform.position += new Vector3(CameraMoveSpeed * Time.deltaTime, 0, 0);
                for (int i = 0; i < UIList.Length; i++)
                {
                    UIList[i].transform.position += new Vector3(CameraMoveSpeed * Time.deltaTime, 0, 0);
                }

}
        }
        if (Input.GetKey(KeyCode.A) && CameraTransform.position.x > (-1*CameraMoveDistanceLimit))
        {
            {
                // Move the camera along the x-axis
                CameraTransform.position += new Vector3(-CameraMoveSpeed * Time.deltaTime, 0, 0);
                for (int i = 0; i < UIList.Length; i++)
                {
                    UIList[i].transform.position += new Vector3(-CameraMoveSpeed * Time.deltaTime, 0, 0);
                }

            }
        }
    }
    public void MoveUI_x(bool direction)
    {
        if (direction && CameraTransform.position.x < CameraMoveDistanceLimit)
        {
            {
                // Move the camera along the x-axis
                CameraTransform.position += new Vector3(CameraMoveSpeed * Time.deltaTime, 0, 0);
                for (int i = 0; i < UIList.Length; i++)
                {
                    UIList[i].transform.position += new Vector3(CameraMoveSpeed * Time.deltaTime, 0, 0);
                }

            }
        }
        if (!direction && CameraTransform.position.x > (-1 * CameraMoveDistanceLimit))
        {
            {
                // Move the camera along the x-axis
                CameraTransform.position += new Vector3(-CameraMoveSpeed * Time.deltaTime, 0, 0);
                for (int i = 0; i < UIList.Length; i++)
                {
                    UIList[i].transform.position += new Vector3(-CameraMoveSpeed * Time.deltaTime, 0, 0);
                }

            }
        }
    }
    public void RestoreUI_x()
    {
        CameraTransform.position = new Vector3(0, 0, CameraTransform.position.z);
        for (int i = 0; i < UIList.Length; i++)
        {
            UIList[i].transform.position = new Vector3(UI_x[i], UIList[i].transform.position.y, UIList[i].transform.position.z);
        }
    }
}
