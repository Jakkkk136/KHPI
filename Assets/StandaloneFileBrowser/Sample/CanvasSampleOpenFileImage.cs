using System.Collections;
using _Scripts.Patterns.SharedData;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : SharedDataUserBehaviour, IPointerDownHandler 
{
    public RawImage output;

    private float defaultWidth, defaultHeight;

    protected void Awake()
    {
        defaultWidth = output.rectTransform.rect.width;
        defaultHeight = output.rectTransform.rect.height;
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".png, .jpg", false);
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
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png", false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif
    
    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);

        output.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultWidth);
        output.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultHeight);
        
        yield return loader;
        
        sharedData.LevelSo.SetLevelTexture(loader.texture);
    }
}