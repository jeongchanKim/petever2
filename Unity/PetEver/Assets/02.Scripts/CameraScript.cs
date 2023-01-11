using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    public GameObject tPlayer;
    public Transform tFollowTarget;
    public GameObject FollowCam;
    public GameObject MainCam;
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        tPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (tPlayer == null){
            Debug.Log("MAN2~~~");
            tPlayer = GameObject.Find("Man");
            if (tPlayer != null){
                tFollowTarget = tPlayer.transform;
                vcam.Follow = tFollowTarget;
            }
        }
    }
}
