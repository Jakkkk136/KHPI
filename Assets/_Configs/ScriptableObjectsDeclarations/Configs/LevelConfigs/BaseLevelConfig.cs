using System;
using System.Collections.Generic;
using _Scripts.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs
{
    [Serializable][CreateAssetMenu(fileName = "Level Config", menuName = "KHPI/Level Config")]
    public class BaseLevelConfig : SerializedScriptableObject
    {
        [ValueDropdown("GetScenesDropdown")]
        public string scene;

        private BaseLevelConfig()
        {

        }

        private IEnumerable<string> GetScenesDropdown()
        {
            return ScenesDropdown.GetScenesInBuildSettings();
        }
    }
}