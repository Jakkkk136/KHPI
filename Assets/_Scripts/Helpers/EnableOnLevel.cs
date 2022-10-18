using System;
using System.Collections.Generic;
using _Scripts.Controllers;
using _Scripts.Patterns.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Helpers
{
    public sealed class EnableOnLevel : MonoBehaviour
    {
        [Serializable]
        private enum eCheckLevelRule
        {
            equals,
            lessOrEqualsThen,
            moreOrEqualsThen,
            notEquals
        }

        [SerializeField] private int LevelToEnable;
        [SerializeField] private eCheckLevelRule rule;

        [BoxGroup("Event on disable")]
        [SerializeField] private bool sendEventIfDisabled;
        [BoxGroup("Event on disable")]
        [ValueDropdown("GetAllEventsNames")] 
        [SerializeField] public string eventIdToSendOnDisable;

        private bool needToStayEnabled;
        
        private void OnEnable()
        {
            needToStayEnabled = rule switch
            {
                eCheckLevelRule.equals => (SaveManager.LevelForPlayer == LevelToEnable),
                eCheckLevelRule.lessOrEqualsThen => (SaveManager.LevelForPlayer <= LevelToEnable),
                eCheckLevelRule.moreOrEqualsThen => (SaveManager.LevelForPlayer >= LevelToEnable),
                eCheckLevelRule.notEquals => (SaveManager.LevelForPlayer != LevelToEnable),
                _ => false
            };

            if (needToStayEnabled == false && sendEventIfDisabled == false)
            {
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            if (needToStayEnabled == false && sendEventIfDisabled == true)
            {
                this.OnEvent(eventIdToSendOnDisable);
                gameObject.SetActive(false);
            }
        }
        
        public IEnumerable<string> GetAllEventsNames()
        {
            return EventID.GetAllEventsNames();
        }
    }
}