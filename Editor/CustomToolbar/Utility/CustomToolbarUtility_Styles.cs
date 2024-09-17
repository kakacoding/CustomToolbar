﻿#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using UnityEngine;

namespace CustomToolbar.Editor
{
    static class CustomToolbarUtility_Styles
    {
        public static readonly GUIStyle DropDownStyle = new("DropDown")
        {
            fontSize = 12,
            stretchWidth = true,
            fixedWidth = 0,
            fixedHeight = 26,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageLeft,
        };
        public static readonly GUIStyle CommandStyle = new("AppCommand")
        {
            fontSize = 12,
            stretchWidth = true,
            fixedWidth = 0,
            fixedHeight = 22,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageLeft,
        };
        public static readonly GUIStyle CommandLeftOnStyle = new("AppCommandLeftOn")
        {
            fontSize = 12,
            stretchWidth = true,
            fixedWidth = 0,
            fixedHeight = 22,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageLeft,
        };
        public static readonly GUIStyle CommandLeftStyle = new("AppCommandLeft")
        {
            fontSize = 12,
            stretchWidth = true,
            fixedWidth = 0,
            fixedHeight = 22,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageLeft,
        };
        public static readonly GUIStyle CommandMidStyle = new("AppCommandMid")
        {
            fontSize = 12,
            stretchWidth = true,
            fixedWidth = 0,
            fixedHeight = 22,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageLeft
        };
        public static readonly GUIStyle commandRightStyle = new("AppCommandRight")
        {
            fontSize = 12,
            stretchWidth = true,
            fixedWidth = 0,
            fixedHeight = 22,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter,
            imagePosition = ImagePosition.ImageLeft
        };
    }

    internal class ToolbarElementDisplay : Attribute 
    {
        public string Display;
        public string Detail;

        public ToolbarElementDisplay(string display, string detail)
        {
            Display = display;
            Detail = detail;
        }

        public static string GetDisplay(Type t)
        {
            var attr = GetCustomAttribute(t, typeof(ToolbarElementDisplay)) as ToolbarElementDisplay;
            return attr?.Display ?? "";
        }
        
        public static string GetDetail(Type t)
        {
            var attr = GetCustomAttribute(t, typeof(ToolbarElementDisplay)) as ToolbarElementDisplay;
            return attr?.Detail ?? "";
        }
        
        public static string GetHelp(Type t)
        {
            return GetCustomAttribute(t, typeof(ToolbarElementDisplay)) is ToolbarElementDisplay attr ? $"{attr.Display}\n{attr.Detail}" : "";
        }
    }
}
#endif
