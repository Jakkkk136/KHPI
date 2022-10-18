using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Patterns.EntitiesManager
{
    [AutoCreateSingelton]
    public class EntitiesManager : Singleton<EntitiesManager>
    {
        private void OnDestroy()
        {
            enteties.Clear();
        }

        private static Dictionary<string, GameObject> enteties = new Dictionary<string, GameObject>();

        public static GameObject Get(string EntityID) => enteties.ContainsKey(EntityID) ? enteties[EntityID] : null;
        public static T GetAs<T>(string EntityID) where T : Component => enteties.ContainsKey(EntityID) ? enteties[EntityID].GetComponent<T>() : null;
        
        public void Register(EntityItem entityItem)
        {
            if (EntitiesManager.enteties.ContainsKey(entityItem.Id))
                Debug.LogWarning("Collection already contains " + entityItem.Id + "!", entityItem);
            else enteties.Add(entityItem.Id, entityItem.gameObject);
        }
    }
}