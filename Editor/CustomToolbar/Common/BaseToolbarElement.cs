#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	internal abstract class BaseToolbarElement
	{
		[JsonProperty]
		protected bool IsEnabled = true;
		[JsonIgnore]
		public virtual string CountingSubKey => "";

		protected BaseToolbarElement() 
		{
		}

		internal GUIStyle _style;
		

		internal virtual GUIStyle Style
		{
			get => _style ??= new GUIStyle(CustomToolbarUtility.CommandStyle);
			set => _style = value;
		}
		
		public void DrawInToolbar(VisualElement container)
		{
			if (IsEnabled) OnDrawInToolbar(container);
		}

		public void DrawInSettings(VisualElement container) => OnDrawInSettings(container);

		public virtual void Init()
		{
		}

		protected virtual void OnDrawInSettings(VisualElement container)
		{
			container.Clear();
			var toggleBtn = new Toggle
			{
				value = IsEnabled,
				label = ToolbarElementDisplay.GetDisplay(GetType())
			};
			toggleBtn.AddToClassList(CustomToolbarUtility.SETTING_TOGGLE);
			toggleBtn.RegisterValueChangedCallback(evt =>
			{
				IsEnabled = evt.newValue;
			});
			container.Add(toggleBtn);
		}

		protected virtual void OnDrawInToolbar(VisualElement container)
		{
		}

		protected void Counting()
		{
			// var subKey = CountingSubKey;
			// subKey = string.IsNullOrEmpty(subKey) ? "" : $"_{subKey}";
			// var url = $"http://teamcity.t3.xd.com:3000/?button={GetType()}{subKey}";
			// UnityWebRequest.Get(new Uri(url)).SendWebRequest();
		}
	}
}
#endif