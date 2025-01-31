using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingGuide : MonoBehaviour
{

    GameObject healingGuide;
    GameObject navi1;
    GameObject navi2;
    GameObject navi3;
    GameObject nextBtn1;
    GameObject nextBtnHide1;
    GameObject nextBtn2;
    GameObject nextBtnHide2;
    GameObject skipBtn;
    GameObject doneBtn;
    GameObject chatYellow;
    GameObject chatBlack;
    GameObject guide1;
    GameObject guide2;
    GameObject guide3;
    public static bool isHealingGuided = false;

    GameObject manCharacter;
    GameObject mainEvent;
    private static GameObject mainCanvas;

    public GameObject eventSystemPrefab;

    GameObject previousCanvas;
    GameObject previouEventSystem;

    public GameObject simpleTestPrefab;
    GameObject simpleTestCanvas;

    CanvasGroup healingGuideCG;


    void Awake() {
        PlayerInput.InitJoystick();
        PlayerController.isForest = false;

        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        mainEvent = GameObject.FindGameObjectWithTag("MainEventSystem");
        mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (mainCanvas) {
            mainCanvas.GetComponent<Canvas>().enabled = false;
        }

        healingGuideCG = GameObject.FindGameObjectWithTag("HealingForestGuide").GetComponent<CanvasGroup>();
        if (isHealingGuided == true) {
            //FIX ME : Allocate when first enter
            HideCanvasGroup(healingGuideCG);
            mainCanvas.GetComponent<Canvas>().enabled = true;
        } else {
            isHealingGuided = true;

            chatYellow = GameObject.Find("HealingGuideChat");
            chatBlack = GameObject.Find("HealingGuideChatBlack");

            guide1 = GameObject.Find("HealingGuide1");
            guide2 = GameObject.Find("HealingGuide2");
            guide3 = GameObject.Find("HealingGuide3");

            navi1 = GameObject.Find("Navigator_oxx");
            navi2 = GameObject.Find("Navigator_xox");
            navi3 = GameObject.Find("Navigator_xxo");
            nextBtn1 = GameObject.Find("Navigator_right");
            nextBtn2 = GameObject.Find("Navigator_right2");
            nextBtnHide1 = GameObject.Find("BtnHide1");
            nextBtnHide2 = GameObject.Find("BtnHide2");

            skipBtn = GameObject.Find("HealingGuideSkip");
            doneBtn = GameObject.Find("HealingGuideDone");

            doneBtn.SetActive(false);

            chatBlack.SetActive(false);

            navi1.SetActive(true);
            navi2.SetActive(false);
            navi3.SetActive(false);
            nextBtn1.SetActive(true);
            nextBtnHide1.SetActive(true);
            nextBtn2.SetActive(false);
            nextBtnHide2.SetActive(false);
        }
    }

    void showSimpleTest()
    {
        simpleTestCanvas = GameObject.FindGameObjectWithTag("SimpleTest");
        if (simpleTestCanvas == null)
        {
            GameObject canvas = Instantiate(simpleTestPrefab) as GameObject;
        }
    }

    public static void OnSimpleTestExitClicked()
    {
        mainCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void HideCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void onhealingGuideExit()
    {
        healingGuideCG = GameObject.FindGameObjectWithTag("HealingForestGuide").GetComponent<CanvasGroup>();
        HideCanvasGroup(healingGuideCG);
        showSimpleTest();
    }

    public void onNextFrom1st() {
        chatYellow.SetActive(false);
        navi1.SetActive(false);

        chatBlack.SetActive(true);
        navi2.SetActive(true);
        nextBtn2.SetActive(true);
        nextBtnHide2.SetActive(true);

        guide3.SetActive(false);
    }

    public void onNextFrom2nd() {
        navi2.SetActive(false);
        guide2.SetActive(false);

        navi3.SetActive(true);
        guide3.SetActive(true);

        skipBtn.SetActive(false);
        doneBtn.SetActive(true);
    }
}
