using System;
using _Scripts.Patterns.Events;
using UnityEngine;

namespace _Scripts.Patterns.SharedData
{
    public abstract class SharedDataUserBehaviour : MonoBehaviour
    {
        protected SharedData sharedData;
        private bool isInternalInited = false;

        protected bool IsInited { get; private set; }

        protected virtual void Awake()
        {
            if (!isInternalInited) Init();
            
        }
        protected void Init()
        {
            if (isInternalInited) return;
            this.OnEvent(EventID.SHARED_DATA_REQUEST, new Action<SharedData>(HandleLevelData));
            this.isInternalInited = true;

            SharedData.OnDataRegenerated += SwapData;
        }

        private void OnDestroy()
        {
            SharedData.OnDataRegenerated -= SwapData;
        }

        protected void HandleLevelData(SharedData sharedData)
        {
            this.sharedData = sharedData;
            this.IsInited = true;
            this.OnInit();
        }

        private void SwapData(SharedData newData)
        {
            if(sharedData == null) return;
            
            sharedData = newData;
        }

        protected virtual void OnInit() { }
    }
}