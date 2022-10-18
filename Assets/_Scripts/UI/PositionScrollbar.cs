using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PositionScrollbar : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private float fromYpos;
    [SerializeField] private float toYpos;
    [SerializeField] private float screenScrollSpeed = 1f;
    [SerializeField] private float scrollbarSpeed = 1f;
        
    private float lastYPos, moveFactorY, lastMoveFactor;
    private float screenHeight;

    private bool smoothingToZero;
    private float startSmoothingTime;
    private float smoothingDuration = 1.5f;
    
    private void Start()
    {
        screenHeight = Screen.height;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != _scrollbar.gameObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastYPos = Input.mousePosition.y;
                
                smoothingToZero = false;
            }
            else if (Input.GetMouseButton(0))
            {
                moveFactorY = (Input.mousePosition.y - lastYPos) * (screenScrollSpeed) * Time.deltaTime;
                lastYPos = Input.mousePosition.y;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                startSmoothingTime = Time.time;
                smoothingToZero = true;
                lastMoveFactor = moveFactorY;
            }

            if (smoothingToZero)
            {
                float t = (Time.time - startSmoothingTime) / smoothingDuration;
                moveFactorY = Mathf.Lerp(lastMoveFactor, 0f, t);
            }

            _scrollbar.value = Mathf.Clamp01(_scrollbar.value + moveFactorY);
        }

        
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Lerp(fromYpos, toYpos, _scrollbar.value);
        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime * scrollbarSpeed);
    }
}
