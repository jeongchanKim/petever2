using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MemorialUIManager : MonoBehaviour
{ 
    public GameObject NewpageUI;
    private Vector3 createPoint;

    public Button LoadNewButton;

    // public UnityAction action;

    void Awake()
    {
        createPoint = GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>().anchoredPosition;
        LoadNewButton = GameObject.Find("LoadNewButton").GetComponent<Button>();
 
        NewpageUI = Resources.Load<GameObject>("Prefabs/NewpageUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadNewButton.onClick.AddListener(delegate { LoadNewpage(); });
    }



    public void LoadNewpage()
    {
        Instantiate(NewpageUI, createPoint, Quaternion.identity, GameObject.Find("MemorialSceneCanvas").GetComponent<RectTransform>());
    }
}
