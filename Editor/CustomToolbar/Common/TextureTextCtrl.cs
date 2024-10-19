using System.Linq;
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
            var container = new VisualElement
            {
                name = nameof(TextureTextCtrl)
            };
            
            var enumField = new EnumField(CustomToolbarUtility.TTDisplayMap[CustomToolbarUtility.ETextTextureDisplay.TextureText], CustomToolbarUtility.ETextTextureDisplay.TextureText);
            enumField.RegisterValueChangedCallback(evt =>
            {
                var newValue = (CustomToolbarUtility.ETextTextureDisplay)evt.newValue;
                displaySetter(newValue);
                enumField.label = CustomToolbarUtility.TTDisplayMap[newValue];
                
                var textureTextContainer = container.Children().FirstOrDefault(child => child.name.Equals("TextureTextContainer"));
                if (textureTextContainer != null)
                {
                    textureTextContainer.style.flexDirection = displayGetter() == CustomToolbarUtility.ETextTextureDisplay.TextureText ? FlexDirection.Row : FlexDirection.RowReverse;
                }
            });
            container.Add(enumField);

            var textureTextContainer = new VisualElement
            {
                name = "TextureTextContainer"
            };
            
            var objField = new ObjectField
            {
                objectType = typeof(Texture2D),
                value = AssetDatabase.LoadAssetAtPath<Texture2D>(textureGetter()),
            };
            objField.RegisterValueChangedCallback(evt =>
            {
                textureSetter(evt.newValue != null ? AssetDatabase.GetAssetPath(evt.newValue) : "");
            });
            textureTextContainer.Add(objField);
            
            var txtBtnText = new TextField
            {
                value = textGetter(),
            };
            txtBtnText.RegisterValueChangedCallback(evt =>
            {
                textSetter(evt.newValue);
            });
            textureTextContainer.Add(txtBtnText);
            
            container.Add(textureTextContainer);
            return container;
        }
    }
}