#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
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
		
		private const string SETTING_TOGGLE = "setting-toggle";
		private const string SETTING_LABEL_DISPLAY = "setting-label-display";

		#region Styles

		protected const string TOOLBAR_BTN_MENU_INVOKE = "toolbar-btn-menu-invoke";
		protected const string SETTING_TEXT_SMALL = "setting-text-small";
		protected const string SETTING_TEXT_LARGE = "setting-text-large";

		#endregion
		
		[JsonIgnore]
		public virtual string CountingSubKey => "";

		protected BaseToolbarElement() 
		{
		}

		internal GUIStyle _style;
		

		internal virtual GUIStyle Style
		{
			get => _style ??= new GUIStyle(CustomToolbarUtility_Styles.CommandStyle);
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
				value = IsEnabled
			};
			toggleBtn.AddToClassList(SETTING_TOGGLE);
			toggleBtn.RegisterCallback<ChangeEvent<bool>>(evt =>
			{
				IsEnabled = evt.newValue;
			});
			container.Add(toggleBtn);
			
			var labelDisplay = new Label
			{
				text = ToolbarElementDisplay.GetDisplay(GetType())
			};
			labelDisplay.AddToClassList(SETTING_LABEL_DISPLAY);
			container.Add(labelDisplay);
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