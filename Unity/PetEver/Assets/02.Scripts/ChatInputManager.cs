using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatInputManager : MonoBehaviour
{
    public TMP_InputField chatInput;
    public GameObject chatTextPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputTextFinished()
    {
        if (chatInput.text != null)
        {
            GameObject myInstance = Instantiate(chatTextPrefab, GameObject.Find("Content").transform);

            TextMeshProUGUI mText = myInstance.GetComponent<TextMeshProUGUI>();
            if (mText != null)
            {
                mText.text = chatInput.text;
                chatInput.text = "";
            }
            else
            {
                Debug.Log("text null, myInstance.GetComponent<TextMeshPro>() : " + myInstance.GetComponent<TextMeshPro>());
            }
        }
    }
}