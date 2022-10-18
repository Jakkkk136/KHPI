using UnityEngine;

namespace _Scripts.UI
{
    public sealed class TutorController : MonoBehaviour
    {
        [SerializeField] private float timeToDisable = 3f;

        private bool touchRecorded;

        private void Awake()
        {
            timeToDisable = OtherSettingsSO.Instance.TutorialDuration;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                touchRecorded = true;
            }
            
            if(!touchRecorded) return;
            
            timeToDisable -= Time.deltaTime;

            if (timeToDisable <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
