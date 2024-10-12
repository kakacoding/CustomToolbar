#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[InitializeOnLoad]
	internal static class ToolbarExtender
	{
		private static readonly Type ToolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
		private const string STYLES = "Packages/com.kakacoding.customtoolbar/Editor/CustomToolbar/UI/Styles.uss";
		private static ScriptableObject CurrentToolbar { get; set; }
		private static VisualElement LeftZoneContainer { get; set; }
		private static VisualElement RightZoneContainer { get; set; }
		
		//private static ToolbarSetting settingBtn = new ToolbarSetting();
		private static CustomToolbarConfig _config;

		static ToolbarExtender()
		{
			EditorApplication.update -= OnUpdate;
			EditorApplication.update += OnUpdate;
			EditorApplication.playModeStateChanged += OnChangePlayMode;
		}
		private static void OnUpdate()
		{
			if (CurrentToolbar != null) return;
			
			var toolbars = Resources.FindObjectsOfTypeAll(ToolbarType);
			CurrentToolbar = toolbars.Length > 0 ? (ScriptableObject) toolbars[0] : null;
			if (CurrentToolbar == null) return;
			
			var root = (VisualElement)CurrentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(CurrentToolbar);
			if (root != null && File.Exists(STYLES))
			{
				root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(STYLES));	
			}
			
			LeftZoneContainer = GetOrCreateToolbarZoneContainer(root, "ToolbarZoneLeftAlign", "toolbar-left-zone"); 
			RightZoneContainer = GetOrCreateToolbarZoneContainer(root, "ToolbarZoneRightAlign", "toolbar-right-zone");
			Reload();
		}

		internal static void Reload()
		{
			InitElements();
			GUILeft(LeftZoneContainer);
			GUIRight(RightZoneContainer);
		}

		private static VisualElement GetOrCreateToolbarZoneContainer(VisualElement root, string zoneName, string containerName)
		{
			var zone = root.Q(zoneName);
			var container = zone.Q(containerName);
			if (container == null)
			{
				container = new VisualElement
				{
					name = containerName
				};
				container.AddToClassList(containerName);
			}
			zone.Add(container);
			return container;
		}

		private static void OnChangePlayMode(PlayModeStateChange state) 
		{
			if (state == PlayModeStateChange.EnteredPlayMode)
			{
				//InitElements();
				Reload();
			}
		}

		private static void InitElements() 
		{
			_config = CustomToolbarConfig.Instance; 
			_config.Elements.ForEach(element => element.Init());
		}

		private static void GUILeft(VisualElement container) 
		{
			container.Clear();
			var idx = _config.Elements.FindIndex(element => element is ToolbarSides);
			DrawInToolbar(container, 0, idx != -1 ? idx : _config.Elements.Count);
		}
		
		private static void GUIRight(VisualElement container)
		{
			container.Clear();
			var idx = _config.Elements.FindIndex(element => element is ToolbarSides);
			if (idx < _config.Elements.Count && idx != -1) 
			{
				DrawInToolbar(container, idx, _config.Elements.Count);	
			}
		}

		[MenuItem("Tools/ToolbarRefresh _F12")]
		static void ff()
		{
			Reload();
		}

		private static void DrawInToolbar(VisualElement container, int from, int to)
		{
			for (var i = from; i < to; ++i)
			{
				_config.Elements[i].DrawInToolbar(container);
			}
		}
	}
}
#endif
