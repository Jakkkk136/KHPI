using UnityEngine;

namespace _Scripts.Helpers
{
    public class ActiveAfterEnable : MonoBehaviour
    {
        [SerializeField] private float activeAfterEnableTime;

        private float enableTime;
        
        private void OnEnable()
        {
            enableTime = Time.time;
        }

        private void Update()
        {
            if(Time.time - enableTime > activeAfterEnableTime) gameObject.SetActive(false);
        }
    }
}