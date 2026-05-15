using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SUBS.Core.EventBus
{
    public static class SEventBus
    {
        private static Dictionary<string, Dictionary<int, Delegate>> _callbacksCacheByPriority = new();

        #region Helper methods
        public static void PrintEvents()
        {            
            List<string> eventNameColumn = new List<string>();
            List<string> methodNameColumn = new List<string>();
            List<string> priorityColumn = new List<string>();

            eventNameColumn.Add("Event name");
            methodNameColumn.Add("Method name");
            priorityColumn.Add("Priority");

            foreach (string @event in _callbacksCacheByPriority.Keys)
            {
                int[] priorities = _callbacksCacheByPriority[@event].Keys
                    .OrderBy(p => p)
                    .ToArray();

                foreach (int key in priorities)
                {
                    Delegate[] delegates = _callbacksCacheByPriority[@event][key].GetInvocationList();

                    foreach (Delegate @delegate in delegates)
                    {
                        string methodName = @delegate.Method.Name;
                        string className = @delegate.Method.ReflectedType.Name;
                        string name = $"{className}.{methodName}";
                        eventNameColumn.Add(@event);
                        methodNameColumn.Add(name);
                        priorityColumn.Add(key.ToString());
                    }
                }                
            }

            int column1MaxWidth = (eventNameColumn.Max(s => s.Length)) + 5;
            var formatColumn2 = string.Format("{{0, -{0}}}", column1MaxWidth);

            int column2MaxWidth = (methodNameColumn.Max(s => s.Length)) + 5;
            var formatColumn3 = string.Format("{{0, -{0}}}", column2MaxWidth);

            int num = 0;
            string column1 = "#  \t";
            string column2;
            string column3;

            Debug.Log("\t  === SEventBus EVENTS ===");

            for (int i = 0; i < eventNameColumn.Count; i++)
            {
                if (num > 0)
                { 
                    column1 = num.ToString("000\t");
                }

                column2 = string.Format(formatColumn2, eventNameColumn[i]);
                column3 = string.Format(formatColumn3, methodNameColumn[i]);                
                Debug.Log(column1 + column2 + column3 + priorityColumn[i]);                
                num++;
            }

            Debug.Log("\n");
        }
        #endregion Helper methods

        #region Event logging and exception throwing
        public static void OnListenerAdding(string eventName, Delegate callback)
        {
#if LOG_ALL_EVENTS || LOG_ADD_LISTENER
		Debug.Log("SEventBus OnListenerAdding \t\"" + eventName + "\"\t{" + listenerBeingAdded.Target + " -> " + listenerBeingAdded.Method + "}");
#endif

            if (!_callbacksCacheByPriority.ContainsKey(eventName))
            {
                _callbacksCacheByPriority.Add(eventName, new Dictionary<int, Delegate>());
            }

            if (callback != null && callback.GetType() != callback.GetType())
            {
                throw new ListenerException($"Attempting to add listener with inconsistent signature for event type {eventName}. Current listeners have type {callback.GetType().Name} and listener being added has type {callback.GetType().Name}");
            }
        }

        public static void OnListenerRemoving(string eventName, Delegate callback)
        {
            if (_callbacksCacheByPriority.ContainsKey(eventName))
            {
                Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];
                Delegate @delegate = null;

                foreach (Delegate d in @event.Values)
                {
                    if (d != null)
                    {
                        @delegate = d;
                        break;
                    }
                }

                if (@delegate == null)
                {
                    throw new ListenerException($"Attempting to remove listener with for event type {eventName}\n but current listener is null.");
                }
                else if (@delegate.GetType() != callback.GetType())
                {
                    throw new ListenerException($"Attempting to remove listener with inconsistent signature for event type {eventName}.\n Current listeners have type {@delegate.GetType().Name} and listener being removed has type {callback.GetType().Name}");
                }
            }
            else
            {
                throw new ListenerException($"Attempting to remove listener for type {eventName}\n but SEventBus doesn't know about this event type.");
            }
        }

        public static void OnSend(string eventName)
        {
            if (!_callbacksCacheByPriority.ContainsKey(eventName))
            {
#if REQUIRE_LISTENER
            throw new SendEventException(string.Format("Sending event \"{0}\" but no listener found. Try marking the event with SEventBus.MarkAsPermanent.", eventName));
#else
#if UNITY_EDITOR
                Debug.LogWarning($"Sending event {eventName} but listeners not found.\n Try marking the event with SEventBus.MarkAsPermanent.");
#endif
#endif
            }
        }

        public static SendEventException CreateSendSignatureException(string eventName)
        {
            return new SendEventException($"Sending event {eventName} but listeners have a different signature than the sender.");
        }      
        #endregion Event logging and exception throwing

        #region AddListener
        public static void AddListener(string eventName, Callback callback, int callPriority = 0)
        {
            OnListenerAdding(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            if (@event.ContainsKey(callPriority))
            {
                @event[callPriority] = (Callback)@event[callPriority] + callback;
            }
            else
            {
                @event[callPriority] = callback;
            }
        }

        public static void AddListener<T>(string eventName, Callback<T> callback, int callPriority = 0)
        {
            OnListenerAdding(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            if (@event.ContainsKey(callPriority))
            {
                @event[callPriority] = (Callback<T>)@event[callPriority] + callback;
            }
            else
            {
                @event[callPriority] = callback;
            }
        }

        public static void AddListener<T, TU>(string eventName, Callback<T, TU> callback, int callPriority = 0)
        {
            OnListenerAdding(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            if (@event.ContainsKey(callPriority))
            {
                @event[callPriority] = (Callback<T, TU>)@event[callPriority] + callback;
            }
            else
            {
                @event[callPriority] = callback;
            }
        }

        public static void AddListener<T, TU, TV>(string eventName, Callback<T, TU, TV> callback, int callPriority = 0)
        {
            OnListenerAdding(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            if (@event.ContainsKey(callPriority))
            {
                @event[callPriority] = (Callback<T, TU, TV>)@event[callPriority] + callback;
            }
            else
            {
                @event[callPriority] = callback;
            }
        }
        #endregion AddListener

        #region RemoveListener
        public static void RemoveListener(string eventName, Callback callback)
        {
            OnListenerRemoving(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            for (int i = 0; i < @event.Keys.Count; i++)
            {
                @event[i] = (Callback)@event[i] - callback;

                if (@event[i] == null)
                {
                    @event.Remove(i);

                    if (@event.Count == 0)
                    {
                        _callbacksCacheByPriority.Remove(eventName);
                    }
                }
            }
        }

        public static void RemoveListener<T>(string eventName, Callback<T> callback)
        {
            OnListenerRemoving(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            for (int i = 0; i < @event.Keys.Count; i++)
            {
                @event[i] = (Callback<T>)@event[i] - callback;

                if (@event[i] == null)
                {
                    @event.Remove(i);

                    if (@event.Count == 0)
                    {
                        _callbacksCacheByPriority.Remove(eventName);
                    }
                }
            }
        }

        public static void RemoveListener<T, TU>(string eventName, Callback<T, TU> callback)
        {
            OnListenerRemoving(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            for (int i = 0; i < @event.Keys.Count; i++)
            {
                @event[i] = (Callback<T, TU>)@event[i] - callback;

                if (@event[i] == null)
                {
                    @event.Remove(i);

                    if (@event.Count == 0)
                    {
                        _callbacksCacheByPriority.Remove(eventName);
                    }
                }
            }
        }

        public static void RemoveListener<T, TU, TV>(string eventName, Callback<T, TU, TV> callback)
        {
            OnListenerRemoving(eventName, callback);
            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            for (int i = 0; i < @event.Keys.Count; i++)
            {
                @event[i] = (Callback<T, TU, TV>)@event[i] - callback;

                if (@event[i] == null)
                {
                    @event.Remove(i);

                    if (@event.Count == 0)
                    {
                        _callbacksCacheByPriority.Remove(eventName);
                    }
                }
            }
        }
        #endregion RemoveListener

        #region Send
        public static void Send(string eventName)
        {
#if LOG_ALL_EVENTS || LOG_SEND_EVENT
		Debug.Log("SEventBus\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventName + "\"");
#endif
            OnSend(eventName);

            if (!_callbacksCacheByPriority.ContainsKey(eventName))
                return;

            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            int[] priorities = @event.Keys
             .OrderBy(p => p)
             .ToArray();

            foreach (int key in priorities)
            {
                Callback callback = (Callback)@event[key];

                if (callback != null)
                {
                    callback.Invoke();
                }
                else
                {
                    throw CreateSendSignatureException(eventName);
                }
            }
        }

        public static void Send<T>(string eventName, T arg1)
        {
#if LOG_ALL_EVENTS || LOG_SEND_EVENT
		Debug.Log("SEventBus\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventName + "\"");
#endif
            OnSend(eventName);

            if (!_callbacksCacheByPriority.ContainsKey(eventName))
                return;

            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            int[] priorities = @event.Keys
             .OrderBy(p => p)
             .ToArray();

            foreach (int key in priorities)
            {
                Callback<T> callback = @event[key] as Callback<T>;

                if (callback != null)
                {
                    callback.Invoke(arg1);
                }
                else
                {
                    throw CreateSendSignatureException(eventName);
                }
            }
        }

        public static void Send<T, TU>(string eventName, T arg1, TU arg2)
        {
#if LOG_ALL_EVENTS || LOG_SEND_EVENTS
		Debug.Log("SEventBus\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventName + "\"");
#endif
            OnSend(eventName);

            if (!_callbacksCacheByPriority.ContainsKey(eventName))
                return;

            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            int[] priorities = @event.Keys
             .OrderBy(p => p)
             .ToArray();

            foreach (int key in priorities)
            {
                Callback<T, TU> callback = @event[key] as Callback<T, TU>;

                if (callback != null)
                {
                    callback.Invoke(arg1, arg2);
                }
                else
                {
                    throw CreateSendSignatureException(eventName);
                }
            }
        }

        public static void Send<T, TU, TV>(string eventName, T arg1, TU arg2, TV arg3)
        {
#if LOG_ALL_EVENTS || LOG_SEND_EVENTS
		Debug.Log("SEventBus\t" + System.DateTime.Now.ToString("hh:mm:ss.fff") + "\t\t\tInvoking \t\"" + eventName + "\"");
#endif
            OnSend(eventName);

            if (!_callbacksCacheByPriority.ContainsKey(eventName))
                return;

            Dictionary<int, Delegate> @event = _callbacksCacheByPriority[eventName];

            int[] priorities = @event.Keys
             .OrderBy(p => p)
             .ToArray();

            foreach (int key in priorities)
            {
                Callback<T, TU, TV> callback = @event[key] as Callback<T, TU, TV>;

                if (callback != null)
                {
                    callback.Invoke(arg1, arg2, arg3);
                }
                else
                {
                    throw CreateSendSignatureException(eventName);
                }
            }
        }
        #endregion Send
    }
}