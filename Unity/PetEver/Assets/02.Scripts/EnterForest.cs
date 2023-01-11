using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnterForest : MonoBehaviour
{
    
    GameObject ManCharacter;
    GameObject MainEvent;
    private bool isEntered = false;
    private Vector3 m_currentDirection = Vector3.zero;

    void Start()
    {
        ManCharacter = GameObject.Find("Man");
        MainEvent = GameObject.Find("MainEventSystem");
        Input.gyro.enabled = true;
    }
   

    private void Update() {
        transform.Rotate(Input.gyro.rotationRateUnbiased.x, Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
    }

    IEnumerator<object> LoadYourAsyncScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TherapyForest", LoadSceneMode.Additive);
 
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
 
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("TherapyForest"));
        SceneManager.MoveGameObjectToScene(ManCharacter, SceneManager.GetSceneByName("TherapyForest"));
        SceneManager.MoveGameObjectToScene(MainEvent, SceneManager.GetSceneByName("TherapyForest"));
        SceneManager.UnloadSceneAsync(currentScene);
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (isEntered == false) {   
            StartCoroutine(LoadYourAsyncScene());
            isEntered = true;
       }
    }

    private void OnCollisionStay(Collision collision)
    {
       
    }

    private void OnCollisionExit(Collision collision)
    {
       
    }

}
