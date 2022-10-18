using System;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations.Configs;
using _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using _Scripts.Patterns.SharedData;
using _Scripts.Services;
using UnityEngine;

namespace _Scripts.Controllers
{
    public sealed class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private BaseLevelConfig levelConfig;

        private List<Action<SharedData>> waitingInitialize = new List<Action<SharedData>>();
        private SharedData data;
        
        public BaseLevelConfig LevelConfig => levelConfig;

        private bool levelEndSet = false;
        
        private void Awake()
        {
            levelConfig = LevelOrder.Instance.GetLevelConfig(SaveManager.LevelForPlayer);
            
            this.Subscribe<Action<SharedData>>(EventID.SHARED_DATA_REQUEST, HandleSharedDataRequest);
        }

        private void Start()
        {

            CreateLevelData();
            
            this.OnEvent(EventID.LEVEL_START);
        }

        private void OnDestroy()
        {
            this.Unsubscribe<Action<SharedData>>(EventID.SHARED_DATA_REQUEST, HandleSharedDataRequest);
        }

        private void HandleSharedDataRequest(Action<SharedData> param)
        {
            if (data == null) this.waitingInitialize.Add(param);
            else param.Invoke(data);
        }

        private void CreateLevelData()
        {
            this.data = SharedData.Generate();
            this.waitingInitialize.ForEach(x => x.Invoke(data));
            this.waitingInitialize.Clear();
        }
        
        public void SetLevelEnd(bool win)
        {
            if(levelEndSet == true) return;
            levelEndSet = true;
            
            if(win) SaveManager.AddLevelToProgress();

            this.OnEvent(win ? EventID.LEVEL_DONE : EventID.LEVEL_FAIL);
        }
    }
}
