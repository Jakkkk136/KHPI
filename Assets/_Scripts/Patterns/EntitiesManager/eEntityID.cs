using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace _Scripts.Patterns.EntitiesManager
{
    public static class EntityID
    {
        public static string BED_JOINT = "BED_JOINT";
        
        public static IEnumerable<string> GetAllEntitiesNames()
        {
            return typeof(EntityID).GetFields().Select(fieldInfo => fieldInfo.Name);
        }
    }
}