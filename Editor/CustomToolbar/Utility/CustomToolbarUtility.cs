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

        #region Styles
        internal const string SETTING_TOGGLE = "setting-toggle";
        internal const string TOOLBAR_BTN_MENU_INVOKE = "toolbar-btn-menu-invoke";
        internal const string SETTING_TEXT_MIN = "setting-text-min";
        internal const string SETTING_TEXT_SMALL = "setting-text-small";
        internal const string SETTING_TEXT_LARGE = "setting-text-large";
        internal const string SETTING_TEXT_TEXTURE_ENUM = "setting-text-texture-enum";
        internal const string SETTING_TEXTURE_MIN = "setting-texture-min";
        #endregion

        public const string LOST_ICON = "Packages/com.unity.collab-proxy/Editor/PlasticSCM/Assets/Images/d_secondarytabclosehover@2x.png";
    }
}