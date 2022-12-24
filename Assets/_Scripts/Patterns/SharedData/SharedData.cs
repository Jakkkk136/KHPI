using System;
using _Configs.ScriptableObjectsDeclarations.Configs;

namespace _Scripts.Patterns.SharedData
{
    public sealed class SharedData
    {
        public LevelSO LevelSo;
        
        public static event Action<SharedData> OnDataRegenerated;
        
        private SharedData(LevelSO levelSo)
        {
            this.LevelSo = levelSo;
        }

        public static SharedData Generate(LevelSO levelSo)
        {
            SharedData newData = new SharedData(levelSo);
            OnDataRegenerated?.Invoke(newData);
            return newData;
        }
    }
}

