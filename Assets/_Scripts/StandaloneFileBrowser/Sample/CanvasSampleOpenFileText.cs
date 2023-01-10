using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using _Scripts.Controllers;
using _Scripts.Patterns.SharedData;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : SharedDataUserBehaviour, IPointerDownHandler
{
    private string loadedString;
    
#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".txt", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    protected override void Start()
    {
        base.Start();

        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "txt", false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif

    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        loadedString = loader.text;
        
        JsonUtility.FromJsonOverwrite(loadedString, sharedData.LevelSo);
        
        OnLevelFileOpened();
    }

    protected virtual void OnLevelFileOpened() { }
}