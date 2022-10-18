using System;
using Object = System.Object;


namespace _Scripts.Patterns.Events
{
    public sealed class ListenerData
    {
        public object Listener { get; }
        public BaseCallbackData Callback { get; }
        public string EventID { get; }

        public ListenerData(Object listener, BaseCallbackData callback, string eventID)
        {
            this.Listener = listener;
            this.Callback = callback;
            this.EventID = eventID;
        }

        public bool Equals(ListenerData other) => Equals(Listener, other.Listener) && EventID == other.EventID;
        public override bool Equals(object obj) => obj is ListenerData other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Listener, EventID);
    }
}
