using System;
using System.Linq;
using PlasticGui.Configuration.CloudEdition.Welcome;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
    internal class TextureTextCtrl : VisualElement
    {
        internal delegate CustomToolbarUtility.ETextTextureDisplay DisplayGetter();
        internal delegate void DisplaySetter(CustomToolbarUtility.ETextTextureDisplay value);
        internal delegate string TextGetter();
        internal delegate void TextSetter(string value);
        internal delegate string TextureGetter();
        internal delegate void TextureSetter(string value);
        
        internal static VisualElement Create(DisplayGetter displayGetter, DisplaySetter displaySetter, 
            TextGetter textGetter, TextSetter textSetter, TextureGetter textureGetter, TextureSetter textureSetter)
        {
            var container = new VisualElement();
            container.AddToClassList(CustomToolbarUtility.SETTING_TEXTURE_TEXT_CTRL);
            
            var enumField = new EnumField(CustomToolbarUtility.TTDisplayMap[CustomToolbarUtility.ETextTextureDisplay.TextureText], CustomToolbarUtility.ETextTextureDisplay.TextureText);
            enumField.RegisterValueChangedCallback(evt =>
            {
                var newValue = (CustomToolbarUtility.ETextTextureDisplay)evt.newValue;
                displaySetter(newValue);
                enumField.label = CustomToolbarUtility.TTDisplayMap[newValue];
                
                var ttContainer = container.Children().FirstOrDefault(child => child.name.Equals("ttContainer"));
                if (ttContainer != null)
                {
                    ttContainer.style.flexDirection = displayGetter() == CustomToolbarUtility.ETextTextureDisplay.TextureText ? FlexDirection.Row : FlexDirection.RowReverse;
                }
            });
            enumField.AddToClassList(CustomToolbarUtility.SETTING_TEXT_TEXTURE_ENUM);
            container.Add(enumField);

            var ttContainer = new VisualElement();
            ttContainer.name = "ttContainer";
            ttContainer.AddToClassList(CustomToolbarUtility.SETTING_TT_CONTAINER);
            
            var objField = new ObjectField
            {
                objectType = typeof(Texture2D),
                value = AssetDatabase.LoadAssetAtPath<Texture2D>(textureGetter()),
            };
            objField.AddToClassList(CustomToolbarUtility.SETTING_TEXTURE_MIN);
            objField.RegisterValueChangedCallback(evt =>
            {
                textureSetter(evt.newValue != null ? AssetDatabase.GetAssetPath(evt.newValue) : "");
            });
            ttContainer.Add(objField);
            
            var txtBtnText = new TextField
            {
                value = textGetter(),
            };
            txtBtnText.AddToClassList(CustomToolbarUtility.SETTING_TEXT_MIN);
            txtBtnText.RegisterValueChangedCallback(evt =>
            {
                textSetter(evt.newValue);
            });
            ttContainer.Add(txtBtnText);
            
            container.Add(ttContainer);
            return container;
        }
    }
}