using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ForestCamera : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    public GameObject tPlayer;
    public Transform tFollowTarget;
    public GameObject FollowCam;
    public GameObject MainCam;
    private Scene currentScene;
    private bool isInit;

    // Start is called before the first frame update
    void Start()
    {
        // vcam = GetComponent<CinemachineVirtualCamera>();
        tPlayer = null;

        MainCam = GameObject.Find("ForestMainCam");
        FollowCam = GameObject.Find("ForestFollowCam");
        vcam = FollowCam.GetComponent<CinemachineVirtualCamera>();

        MainCam.transform.position = new Vector3(-34.9f, 8.6f, -51.2f);
        FollowCam.transform.position = new Vector3(-34.9f, 8.6f, -51.2f);

        // MainCam.transform.eulerAngles = new Vector3(4.928f, -1.265f, -2.536f);
        // FollowCam.transform.eulerAngles = new Vector3(4.928f, -1.265f, -2.536f);
        isInit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInit == false) {
            MainCam.transform.position = new Vector3(-34.9f, 8.6f, -51.2f);
            FollowCam.transform.position = new Vector3(-34.9f, 8.6f, -51.2f);

            MainCam.transform.eulerAngles = new Vector3(4.928f, -1.265f, -2.536f);
            FollowCam.transform.eulerAngles = new Vector3(4.928f, -1.265f, -2.536f);

            isInit = true;
        }

        if (tPlayer == null) {
            Debug.Log("MAN~~~");
            tPlayer = GameObject.Find("Man");
            if (tPlayer != null){
                tFollowTarget = tPlayer.transform;
                vcam.Follow = tFollowTarget;
                vcam.LookAt = tFollowTarget;
            }
        }
    }
}
