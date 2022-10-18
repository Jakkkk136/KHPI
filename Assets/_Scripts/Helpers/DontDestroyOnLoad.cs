using UnityEngine;

public sealed class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
