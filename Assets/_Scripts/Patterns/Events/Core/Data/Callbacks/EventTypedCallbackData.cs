using System;

namespace _Scripts.Patterns.Events
{
    public sealed class EventTypedCallbackData<T> : BaseCallbackData
    {
        private readonly Action<T> _typedEventCallback;

        public EventTypedCallbackData(Action<T> callback) => this._typedEventCallback = callback;

        public override void Call(object param)
        {
            this._typedEventCallback.Invoke((T) param);
        }
    }
}
