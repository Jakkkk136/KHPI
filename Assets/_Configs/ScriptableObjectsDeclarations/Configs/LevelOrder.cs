using System;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs;
using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Patterns;
using _Scripts.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif

namespace _Configs.ScriptableObjectsDeclarations.Configs
{
    [Serializable]
    [CreateAssetMenu(fileName = "LevelOrder", menuName = "KHPI/Game Settings/LevelOrderSettings")]
    public class LevelOrder : SingletonScriptableObject<LevelOrder>
    {
        [FormerlySerializedAs("lowBorderOfRandomLevels")] [SerializeField] private int levelToStartOnSecondLap = 6;

        [Space]
        [Searchable]
        [ListDrawerSettings(ShowPaging = true, NumberOfItemsPerPage = 10, OnBeginListElementGUI = "OnBeginListElement", OnEndListElementGUI = "OnEndListElement")]
        [SerializeField] private List<BaseLevelConfig> _levelConfigs;

        private int debugSceneToLoadBuildIndex = -1;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            debugSceneToLoadBuildIndex = -1;
        }

        public BaseLevelConfig GetLevelConfig(int levelNumber)
        {
            int levelConfigIndex = CalculateLevelCurrentConfigIndex(levelNumber);
            
            PlayerPrefs.SetInt(PrefsNames.LAST_LEVEL_CONFIG, levelConfigIndex);
            
            return _levelConfigs[levelConfigIndex];
        }

        public int CalculateLevelCurrentConfigIndex(int levelNumber)
        {
            levelNumber--;
            int levelConfigIndex;
            
            if (false)
            {
                levelConfigIndex = PlayerPrefs.GetInt(PrefsNames.LAST_LEVEL_CONFIG, 0);
            }
            else if (levelNumber >= _levelConfigs.Count)
            {
                levelNumber = Mathf.Max(levelNumber - _levelConfigs.Count, 0);
                int levelInLap = levelNumber % (_levelConfigs.Count - levelToStartOnSecondLap + 1);
                levelNumber = levelInLap + levelToStartOnSecondLap - 1;
                
                levelConfigIndex = levelNumber;
            }
            else
            {
                levelConfigIndex = levelNumber;
            }

            return levelConfigIndex;
        }

        public void SetDebugNextScene(int sceneIndex)  
        {
            debugSceneToLoadBuildIndex = sceneIndex;
        }

        public int GetCurrentSceneBuildIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    

        #region Odin Inspector

#if UNITY_EDITOR
        private void OnBeginListElement(int index)
        {
            SirenixEditorGUI.BeginBox("Level: " + (index + 1));
        }

        private void OnEndListElement(int index)
        {
            SirenixEditorGUI.EndBox();
        }
#endif


        #endregion
    }

    [Serializable]
    public class LoadAnyLevelParams
    {
        public bool load = false;
        [Range(1, 998)] public int level = 1;
    }
}