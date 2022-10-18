using System;

namespace _Scripts.Patterns.Events
{
    public sealed class EventCallbackData : BaseCallbackData
    {
        private readonly Action _emptyEventCallback;

        public EventCallbackData(Action callback) => this._emptyEventCallback = callback;

        public override void Call(object param)
        {
            this._emptyEventCallback.Invoke();
        }
    }
}
