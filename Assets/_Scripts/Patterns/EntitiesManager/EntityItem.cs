using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Patterns.EntitiesManager
{
    public class EntityItem : MonoBehaviour
    {
        [ValueDropdown("GetAllEntitiesNames")] 
        [SerializeField] private string id;
        [SerializeField] private bool disableOnAwake;
        public string Id => id;
        
        public IEnumerable<string> GetAllEntitiesNames()
        {
            return EntityID.GetAllEntitiesNames();
        }

        protected void Awake()
        {
            EntitiesManager.Instance.Register(this);
            this.gameObject.SetActive(!disableOnAwake);
        }
    }
}