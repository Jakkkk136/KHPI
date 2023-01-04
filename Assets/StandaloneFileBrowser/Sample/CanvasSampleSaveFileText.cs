using System;
using System.IO;
using _Scripts.LevelCreator;
using _Scripts.Patterns.SharedData;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : SharedDataUserBehaviour, IPointerDownHandler {

    // Sample text data
    private string _data;
    

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        _data = LevelSerializer.SerializeLevel(sharedData.LevelSo);
        var bytes = Encoding.UTF8.GetBytes(_data);
        DownloadFile(gameObject.name, "OnFileDownload", "sample.txt", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
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

    public void OnClick()
    {
        CreatorWindow.Instance.FillInLevelData();
        _data = LevelSerializer.SerializeLevel(sharedData.LevelSo);
        
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "txt");
        if (!string.IsNullOrEmpty(path)) {
            File.WriteAllText(path, _data);
        }
    }
#endif
}