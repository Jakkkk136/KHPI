using System;
using _Scripts.Patterns.Events;

public static class EventisObjectExtension
{
    private static EventisManager manager;
    static EventisObjectExtension() => manager = EventisManager.Instance;
        
    public static void Subscribe(this Object listener, string EventID, Action callback) =>
        manager.Subscribe(listener, callback, EventID);
    public static void Subscribe<T>(this Object listener, string EventID, Action<T> callback) =>
        manager.Subscribe<T>(listener, callback, EventID);
        
    public static void Unsubscribe(this Object listener, string EventID, Action callback) =>
        manager.Unsubscribe(listener, callback, EventID);
    public static void Unsubscribe<T>(this Object listener, string EventID, Action<T> callback) =>
        manager.Unsubscribe<T>(listener, callback, EventID);

    public static void OnEvent(this Object sender, string EventID) => manager.OnEvent(EventID);
        
    public static void OnEvent<T>(this Object sender, string EventID, T param) => manager.OnEvent<T>(EventID, param);
}