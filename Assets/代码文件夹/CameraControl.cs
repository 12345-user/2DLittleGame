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

    public int ButtonDirection;

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
        MoveUI_x_continuous(ButtonDirection);
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
    public void MoveUI_x_continuous(int direction)
    {
        if (direction >0 && CameraTransform.position.x < CameraMoveDistanceLimit)
        {
            {
                // Move the camera along the x-axis
                CameraTransform.position += new Vector3(direction * CameraMoveSpeed * Time.deltaTime, 0, 0);
                for (int i = 0; i < UIList.Length; i++)
                {
                    UIList[i].transform.position += new Vector3(direction * CameraMoveSpeed * Time.deltaTime, 0, 0);
                }

            }
        }
        if (direction <0  && CameraTransform.position.x > (-1 * CameraMoveDistanceLimit))
        {
            // Move the camera along the x-axis
            CameraTransform.position += new Vector3(direction * CameraMoveSpeed * Time.deltaTime, 0, 0);
            for (int i = 0; i < UIList.Length; i++)
            {
                UIList[i].transform.position += new Vector3(direction * CameraMoveSpeed * Time.deltaTime, 0, 0);
            }

        }
    }
    public void SetDirection(int direction)
    {
        ButtonDirection = direction;
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
