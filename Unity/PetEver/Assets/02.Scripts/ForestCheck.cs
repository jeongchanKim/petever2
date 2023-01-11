using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

using UnityEngine.SceneManagement;

public class ForestCheck : MonoBehaviour
{
    private GameObject FollowCam;
    private GameObject MainCam;
    
    void Start()
    {
        Input.gyro.enabled = true;
        PlayerController.isForest = true;
        UnityEngine.Debug.Log("WOW");

        MainCam = GameObject.Find("ForestMainCam");
        FollowCam = GameObject.Find("ForestFollowCam");

        MainCam.transform.position = new Vector3(-34.9f, 8.6f, -51.2f);
        FollowCam.transform.position = new Vector3(-34.9f, 8.6f, -51.2f);
    }
   
    private void Update() {
        transform.Rotate(Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
        // UnityEngine.Debug.Log("x: " + Input.gyro.rotationRateUnbiased.x + " y: " + Input.gyro.rotationRateUnbiased.y + " z: " + Input.gyro.rotationRateUnbiased.z);
        if (Math.Abs(Input.gyro.rotationRateUnbiased.x) > 0.3f && Math.Abs(Input.gyro.rotationRateUnbiased.y) > 0.3f && Math.Abs(Input.gyro.rotationRateUnbiased.z) > 0.3f) {
            PlayerController.mov_x = 180;
            PlayerController.mov_y = 50;
            PlayerController.playerSpeed = 0.03f;
        } else {
            PlayerController.mov_x = 0;
            PlayerController.mov_y = 0;
        }
    }
}
