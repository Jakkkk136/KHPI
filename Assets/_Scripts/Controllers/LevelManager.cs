using System;
using System.Collections.Generic;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using _Scripts.Patterns.SharedData;

namespace _Scripts.Controllers
{
    public sealed class LevelManager : Singleton<LevelManager>
    {
        public LevelSO levelSo;

        private List<Action<SharedData>> waitingInitialize = new List<Action<SharedData>>();
        private SharedData data;
        
        private bool levelEndSet = false;
        
        private void Awake()
        {
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
            this.data = SharedData.Generate(levelSo);

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
