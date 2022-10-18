using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public sealed class UIManager : Singleton<UIManager>
    {
        [Serializable]
        private class LevelNumberTextAndPrefix
        {
            public Text text;
            public string prefix;
        }

        [SerializeField] private CanvasConfig config;

        [Space]
        [SerializeField] private List<LevelNumberTextAndPrefix> levelNumberTextAndPrefix;
        
        private UIWindow currentWindow;
        private Dictionary<eWindowType, UIWindow> allWindows = new Dictionary<eWindowType, UIWindow>();
        private Canvas canvas;
        private Camera mainCam;
        private CinemachineVirtualCamera vcam;

        public  eWindowType currentWindowType;
        
        public static eWindowType previousWindowType;
        
        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            
            canvas.worldCamera = mainCam = Camera.main;
            vcam = FindObjectOfType<CinemachineVirtualCamera>();

            EventSystem[] es = FindObjectsOfType<EventSystem>();
            if(es.Length > 1) es[1].gameObject.SetActive(false);
            
            InitWindows();
            ShowWindow(config.StartingWindow);

            foreach (var textAndPrefix in levelNumberTextAndPrefix)
            {
                textAndPrefix.text.text = $"{textAndPrefix.prefix} {SaveManager.LevelForPlayer}";
            }
        }

        private void OnEnable()
        {
            this.Subscribe(EventID.LEVEL_START, OnLevelStart);
            this.Subscribe(EventID.LEVEL_FAIL, OnLevelFail);
            this.Subscribe(EventID.LEVEL_DONE, OnLevelDone);
        }
        
        private void OnDisable()
        {
            this.Unsubscribe(EventID.LEVEL_START, OnLevelStart);
            this.Unsubscribe(EventID.LEVEL_FAIL, OnLevelFail);
            this.Unsubscribe(EventID.LEVEL_DONE, OnLevelDone);
        }
        private void OnLevelStart()
        {
            ShowWindow(eWindowType.Game);
        }

        private void OnLevelDone()
        {
            canvas.planeDistance = 1f;
            StartCoroutine(ShowWindowWithDealy(config.DelayShowingWinWindow, eWindowType.Win));
        }
        private void OnLevelFail()
        {
            StartCoroutine(ShowWindowWithDealy(config.DelayShowingLoseWindow, eWindowType.Lost));
        }
        

        private IEnumerator ShowWindowWithDealy(float delay, eWindowType windowType) {
            yield return new WaitForSeconds(delay);
            ShowWindow(windowType);
        }
        
        private void InitWindows() {
            for (int i = 0; i < transform.childCount; i++)
            {
                var window = transform.GetChild(i).GetComponent<UIWindow>();
                if (window != null) {
                    if (allWindows.ContainsKey(window.windowType))
                    { 
                        Debug.LogError($"Found duplicate uiWindow - {window.windowType}. Change window type");
                        continue;
                    }

                    allWindows.Add(window.windowType, window);
                    window.Hide();
                }
            }    
        }

        public void OpenItemsUI()
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            mainCam.orthographic = true;
            mainCam.orthographicSize = 1f;

            if (vcam != null)
            {
                vcam.m_Lens.Orthographic = true;
                vcam.m_Lens.OrthographicSize = 1;
            }

            ShowWindow(eWindowType.ItemsUI);
        }

        
        public void ShowWindow(eWindowType windowType) {
            if (currentWindow)
            {
                previousWindowType = currentWindow.windowType;
                currentWindow.Hide();
            }
            if (allWindows.TryGetValue(windowType, out currentWindow))
            {
                currentWindowType = windowType;
                currentWindow.Show();
            }
        }

        public void OpenPreviousWindow()
        {
            ShowWindow(previousWindowType);
        }
    }
}
