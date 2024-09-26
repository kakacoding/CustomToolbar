using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
    internal static class CustomToolbarUtility
    {
        internal static void LogError(string log)
        {
            Debug.LogError(log);
        }
        private static readonly Dictionary<VisualElement, Dictionary<Type, Delegate>> VisualElementEventCache = new ();

        internal static void RegisterCallback<T>(this VisualElement visualElement, EventCallback<T> callback) where T : EventBase<T>, new()
        {
            if (!VisualElementEventCache.TryGetValue(visualElement, out var eventDictionary))
            {
                eventDictionary = new Dictionary<Type, Delegate>();
                VisualElementEventCache[visualElement] = eventDictionary;
            }

            if (eventDictionary.TryGetValue(typeof(T), out var existingCallback))
            {
                visualElement.UnregisterCallback((EventCallback<T>)existingCallback);
            }

            visualElement.RegisterCallback(callback);
            eventDictionary[typeof(T)] = callback;
        }
    }
}