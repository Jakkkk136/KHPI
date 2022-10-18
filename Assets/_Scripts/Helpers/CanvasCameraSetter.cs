using UnityEngine;

public sealed class CanvasCameraSetter : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
