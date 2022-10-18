using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIWindow : MonoBehaviour
    {
        [field: SerializeField] public eWindowType  windowType { get; private set; }

        public void Show() { gameObject.SetActive(true); }
        public void Hide() { gameObject.SetActive(false); }
    }
}
