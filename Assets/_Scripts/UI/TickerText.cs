using System;
using System.Text;
using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class TickerText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float scrollSpeed;

    private float width = 0;
    private float maxXDistance;
    private Vector3 teleportToRight;

    private void Awake()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Level ");
        sb.Append(SaveManager.LevelForPlayer);
        sb.Append("      WAKE UP PLEASE");

        text.text = sb.ToString();
    }


    private void Update()
    {
        if (width == 0)
        {
            width = (transform as RectTransform).rect.width;
            float parentWidth = (transform.parent as RectTransform).rect.width;
            float teleportToX = width / 2f - parentWidth;
            teleportToRight = new Vector3(teleportToX, 0f, 0f);
            maxXDistance = -width - parentWidth / 2f;
        }
        
        transform.Translate(-scrollSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        if (transform.localPosition.x <= maxXDistance) transform.localPosition = teleportToRight;
    }
}
