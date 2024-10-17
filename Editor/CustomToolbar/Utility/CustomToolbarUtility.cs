using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
    internal static partial class CustomToolbarUtility
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

        public enum ETextTextureDisplay
        {
            TextureText,
            TextTexture,
        }

        public static readonly Dictionary<ETextTextureDisplay, string> TTDisplayMap = new()
        {
            { ETextTextureDisplay.TextureText, "左图右字" },
            { ETextTextureDisplay.TextTexture, "左字右图" },
        };

        

        public const string LOST_ICON = "Packages/com.unity.collab-proxy/Editor/PlasticSCM/Assets/Images/d_secondarytabclosehover@2x.png";
    }
}