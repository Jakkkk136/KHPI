using System;
using _Scripts.Controllers;
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

            if (needToStayEnabled == false)
            {
                gameObject.SetActive(false);
            }
        }
    }
}