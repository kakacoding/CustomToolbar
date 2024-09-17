#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	internal abstract class BaseToolbarElement
	{
		protected static string GetPackageRootPath => "ProjectSettings/CustomToolbar";

		protected bool IsEnabled = true;
		protected int WidthInToolbar;

		protected const float FieldSizeSpace = 10.0f;
		protected const float FieldSizeSingleChar = 13.0f;
		protected const float FieldSizeWidth = 50.0f;
		protected const float DefaultSectionWidth = 250;
		
		private const string SETTING_TOGGLE = "setting-toggle";
		private const string SETTING_LABEL_DISPLAY = "setting-label-display";
		internal GUIStyle _style;
		

		internal virtual GUIStyle Style
		{
			get => _style ??= new GUIStyle(CustomToolbarUtility_Styles.CommandStyle);
			set => _style = value;
		}

		[JsonIgnore]
		public virtual string CountingSubKey => DisplayNameInToolbar;
		[JsonIgnore]
		public virtual string DisplayNameInToolbar => "";

		protected BaseToolbarElement() : this(150)
		{
			//Init();
		}

		protected BaseToolbarElement(int widthInToolbar)
		{
			WidthInToolbar = widthInToolbar;
		}

		// public void DrawInList(Rect position)
		// {
		// 	position.y += 2;
		// 	position.height -= 4;
		//
		// 	position.x += FieldSizeSpace;
		// 	position.width = 10;
		// 	IsEnabled = EditorGUI.ToggleLeft(position, "", IsEnabled);
		//
		// 	position.x += position.width + FieldSizeSpace;
		// 	position.width = 200;
		// 	EditorGUI.LabelField(position, ToolbarElementDisplay.GetDisplay(this.GetType()));
		//
		// 	EditorGUI.BeginDisabledGroup(!IsEnabled);
		// 	OnDrawInSettings(position);
		// 	EditorGUI.EndDisabledGroup();
		// }

		public void DrawInToolbar(VisualElement container)
		{
			if (IsEnabled) OnDrawInToolbar(container);
		}

		public void DrawInSettings(VisualElement container)
		{
			OnDrawInSettings(container);
		}

		public virtual void Init()
		{
		}

		protected virtual void OnDrawInSettings(VisualElement container)
		{
			var toggleBtn = new Toggle();
			toggleBtn.AddToClassList(SETTING_TOGGLE);
			container.Add(toggleBtn);
			
			var labelDisplay = new Label
			{
				text = ToolbarElementDisplay.GetDisplay(GetType())
			};
			labelDisplay.AddToClassList(SETTING_LABEL_DISPLAY);
			container.Add(labelDisplay);
		}
		protected abstract void OnDrawInToolbar(VisualElement container);

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