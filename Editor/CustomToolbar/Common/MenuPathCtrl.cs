﻿using UnityEditor;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
    internal class MenuPathCtrl : VisualElement
    {
        internal delegate string LabelGetter();
        internal delegate string TextGetter();
        internal delegate void TextSetter(string value);
        
        internal static VisualElement Create(LabelGetter labelGetter, TextGetter textGetter, TextSetter textSetter)
        {
            var textField = new TextField(labelGetter.Invoke())
            {
                name = nameof(MenuPathCtrl),
                value = textGetter(),
            };
            textField.RegisterValueChangedCallback(evt =>
            {
                textSetter.Invoke(evt.newValue);
                ToolbarExtender.Reload();
            });
            
            return textField;
        }
    }
}