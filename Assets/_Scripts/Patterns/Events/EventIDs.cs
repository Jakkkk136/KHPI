using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Patterns.Events
{
    public static class EventID
    {
        public static string LEVEL_START = "LEVEL_START";
        public static string LEVEL_FAIL = "LEVEL_FAIL";
        public static string LEVEL_DONE = "LEVEL_DONE";
        public static string INPUT = "INPUT";
        public static string SHARED_DATA_REQUEST = "SHARED_DATA_REQUEST";
        public static string PLAYER_MONEY_CHANGED = "PLAYER_MONEY_CHANGED";
        public static string UPGRADE_WINDOW_OPENED = "UPGRADE_WINDOW_OPENED";

        public static IEnumerable<string> GetAllEventsNames()
        {
            return typeof(EventID).GetFields().Select(fieldInfo => fieldInfo.Name);
        }
    }
}
