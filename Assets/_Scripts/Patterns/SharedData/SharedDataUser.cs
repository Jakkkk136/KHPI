using System;
using _Scripts.Patterns.Events;


namespace _Scripts.Patterns.SharedData
{
    public abstract class SharedDataUser
    {
        protected SharedData sharedData;
        private bool isInternalInited = false;

        protected bool IsInited { get; private set; }
        
        
        protected SharedDataUser()
        {
            if (!isInternalInited) Init();
        }
        
        protected void Init()
        {
            if (isInternalInited) return;
            this.OnEvent(EventID.SHARED_DATA_REQUEST, new Action<SharedData>(HandleLevelData));
            this.isInternalInited = true;
        }

        protected void HandleLevelData(SharedData sharedData)
        {
            this.sharedData = sharedData;
            this.IsInited = true;
            this.OnInit();
        }

        protected virtual void OnInit() { }

    }
}