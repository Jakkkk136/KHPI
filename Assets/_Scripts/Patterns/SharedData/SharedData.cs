using System;
using _Configs.ScriptableObjectsDeclarations.Configs;

namespace _Scripts.Patterns.SharedData
{
    public sealed class SharedData
    {
        
        public static event Action<SharedData> OnDataRegenerated;
        
        private SharedData()
        {
        }

        public static SharedData Generate()
        {
            SharedData newData = new SharedData();
            OnDataRegenerated?.Invoke(newData);
            return newData;
        }
    }
}

