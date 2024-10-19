﻿using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
    internal class ParamsCtrl : VisualElement
    {
        internal delegate string LabelGetter();
        internal delegate string TextGetter();
        internal delegate void TextSetter(string value);
        
        internal static VisualElement Create(LabelGetter labelGetter, TextGetter textGetter, TextSetter textSetter)
        {
            var textField = new TextField(labelGetter.Invoke())
            {
                name = nameof(ParamsCtrl),
                value = textGetter(),
            };
            textField.RegisterValueChangedCallback(evt =>
            {
                textSetter(evt.newValue);
            });
            
            return textField;
        }
    }
}