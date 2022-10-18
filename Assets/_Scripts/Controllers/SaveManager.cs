using System;
using _Scripts.Patterns.Events;
using _Scripts.Services;
using _Scripts.UI;
using UnityEngine;


namespace _Scripts.Controllers
{
    public static class SaveManager
    {
        [Serializable]
        private class SavedData
        {
            public int LevelForPlayer = 1;
            public int PlayerMoney = 0;
            public int PlayerMoneyCollectedOnLevel = 0;
            public int PlayerMoneyOnLevelEnd = 0;
        }
        
        private static readonly string saveName = PrefsNames.MAIN_DATA_SAVE;
        
        private static SavedData _savedData;
        private static SavedData savedData
        {
            get
            {
                if(_savedData == null) Load();
                return _savedData;
            }
        }

        public static int LevelForPlayer => savedData.LevelForPlayer;

        public static int MoneyCollectedOnLevel
        {
            get => savedData.PlayerMoneyCollectedOnLevel;
            private set => savedData.PlayerMoneyCollectedOnLevel = value < 0 ? 0 : value;
        }

        public static int PlayerMoney
        {
            get => savedData.PlayerMoney;
            private set => savedData.PlayerMoney = value < 0 ? 0 : value;
        }
        public static int PlayerMoneyOnLevelEnd 
        { 
            get => savedData.PlayerMoneyOnLevelEnd;
            set => savedData.PlayerMoneyOnLevelEnd = value < 0 ? 0 : value;
        }

        public static void SetInGameLevel(int level)
        {
            savedData.LevelForPlayer = Mathf.Max(0, level);
            Save();
        }

        public static void ResetPlayerMoneyOnLevel() => savedData.PlayerMoneyCollectedOnLevel = 0;
        
        public static void AddLevelToProgress()
        {
            savedData.LevelForPlayer++;
            
            Save();
        }

        public static void RemoveLevelFromProgress()
        {
            if (savedData.LevelForPlayer == 1) { }
            else
            {
                savedData.LevelForPlayer--;
            }
            
            Save();
        }
        
        public static bool PlayerHasMoney(int amount) => PlayerMoney >= amount;
    
        public static void ChangePlayerMoney(int amount, bool withAnim)
        {
            switch (amount)
            {
                case 0:
                    return;
                case >= 0:
                    PlayerMoney += amount;
                    break;
                default:
                {
                    if (PlayerMoney + amount >= 0)
                    {
                        PlayerMoney += amount;
                    }
                    else
                    {
                        PlayerMoney = 0;
                    }

                    break;
                }
            }

            Save();
            
            savedData.OnEvent<bool>(EventID.PLAYER_MONEY_CHANGED, withAnim);
        }

        public static void Save() {

            if (_savedData == null) return;
            
            var savedStr = JsonUtility.ToJson(_savedData);
            PlayerPrefs.SetString(saveName, savedStr);
        }

        private static void Load() 
        {
            if (PlayerPrefs.HasKey(saveName))
            {
                var savedStr = PlayerPrefs.GetString(saveName);
                _savedData = JsonUtility.FromJson<SavedData>(savedStr);
               
            }
            else
            {
                _savedData = new SavedData();
                
            }
        }
    }
}
