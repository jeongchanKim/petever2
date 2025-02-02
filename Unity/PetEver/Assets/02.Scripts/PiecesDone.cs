using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Android;
using System.IO;

public class PiecesDone : MonoBehaviour
{
    private static GameObject picFrame;
    
    private static RawImage displayImg;
    private static GameObject captureArea;
    private static Rect captureRect;
    private int captureWidth;
    private int captureHeight;
    RawImage ri;
    void Start()
    {
        captureArea = GameObject.Find("CaptureArea");
        picFrame = GameObject.Find("Frame/CreatedPannel/MemorialSceneCanvas/CreatedImg");
        displayImg = picFrame.GetComponent<RawImage>();
        displayImg.enabled = false;
        ri = GameObject.Find("TempImg").GetComponent<RawImage>();
    }

    private void SaveCreatedPicture(string filename, byte[] bytes)
    {
        string def_path = "/storage/emulated/0/DCIM/PetEver/";
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        File.WriteAllBytes(def_path + filename + "-" + timestamp + ".png", bytes);
    }

    private IEnumerator Screenshot(Action<Texture2D> onFinished)
    {
        yield return new WaitForEndOfFrame();

        // Create Texture
        Texture2D screenTex = new Texture2D(850, 850, TextureFormat.RGB24, false);
        // Texture2D screenTex = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

        Rect area = new Rect(120, 830, 1080, 2340);

        // Read the current screen pixels
        screenTex.ReadPixels(area, 0, 0);
        screenTex.Apply();

        // Encode to byte[], and Read the Image
        byte[] bytes = screenTex.EncodeToPNG();
        screenTex.LoadImage(bytes);
        SaveCreatedPicture("petever", bytes);
        
        ri.texture = screenTex;
        ri.SetNativeSize();
        ImageSizeSetting(ri, 100, 100);
        
        displayImg.texture = ri.texture;
        
        // Destroy(displayImg);

        onFinished?.Invoke(screenTex);
        NewpageUIManager.CloseUI();
    }
    
    public void onPieceDoneClicked()
    {
        displayImg.enabled = true;
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        StartCoroutine(Screenshot((texture2D) =>
        {
            // 스크린샷(texture2D)을 사용할 수 있는 시점
        }));
    }

    
    void ImageSizeSetting(RawImage img, float x, float y)
    {
        var imgX = img.rectTransform.sizeDelta.x;
        var imgY = img.rectTransform.sizeDelta.y;

        if (x / y > imgX / imgY) // if image height is longer than width 
        {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,y);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,imgX*(y/imgY));
        }
        else // if image width is longer than height 
        {
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,x);
            img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,imgY*(x/imgX)); 
        }
    }
}
