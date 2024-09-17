#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditorInternal;

namespace CustomToolbar.Editor
{
	internal class CustomToolbarSettingsProvider : SettingsProvider
	{
		private const string SettingProviderPath = "Project/CustomToolbar";
		private const string UXml = "Packages/com.kakacoding.customtoolbar/Editor/CustomToolbar/UI/ProviderSettings.uxml";
		
		private SerializedObject _toolbarSettings;
		private Vector2 _scrollPos;
		private ReorderableList _elementsList;

		private static ListView _toolbarListView;

		private const string SETTING_HORIZONTAL_PANEL = "setting-horizontal-panel";
		private const string SETTING_LISTVIEW = "setting-listview";
		
		[SettingsProvider]
		public static SettingsProvider CreateProvider()
		{
			return new SettingsProvider(SettingProviderPath, SettingsScope.Project)
			{
				activateHandler = (_, rootElement) =>
				{
					var window = EditorGUIUtility.Load(UXml) as VisualTreeAsset;
					if (window == null) return;
					window.CloneTree(rootElement);
					var sv = rootElement.Q<ScrollView>("toolbarScrollView");
					_toolbarListView = new ListView(CustomToolbarSettings.Instance.Elements, 20, () =>
					{
						var container = new VisualElement();
						container.AddToClassList(SETTING_HORIZONTAL_PANEL);
						return container;
					}, (container, i) =>
					{
						if (i < CustomToolbarSettings.Instance.Elements.Count)
						{
							CustomToolbarSettings.Instance.Elements[i].DrawInSettings(container);
						}
					});
					_toolbarListView.AddToClassList(SETTING_LISTVIEW);
					
					_toolbarListView.showAddRemoveFooter = true;
					_toolbarListView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(() =>
					{
						BaseToolbarElement[] toolbarElements =
						{
							new ToolbarMenuInvoke(),
						};
						
						var menu = new GenericMenu();
						foreach (var toolbarElement in toolbarElements)
						{
							if (toolbarElement == null)
							{
								menu.AddSeparator("");
							}
							else
							{
								var display = ToolbarElementDisplay.GetDisplay(toolbarElement.GetType());
								menu.AddItem(new GUIContent(display), false, target =>
								{
									if (target is ToolbarSceneSelection selection)
									{
										selection.Init();
									}
									var idx = _toolbarListView.selectedIndex == -1 ? 0 : _toolbarListView.selectedIndex;
									CustomToolbarSettings.Instance.Elements.Insert(idx, target as BaseToolbarElement);
									//_toolbarListView.itemsSource.Insert(idx, target);
									_toolbarListView.Rebuild();
								}, toolbarElement);
							}
						}
						
						menu.ShowAsContext();
					});
				
					_toolbarListView.itemsRemoved += ints =>
					{
						_toolbarListView.Rebuild();
					};
					
					sv.Add(_toolbarListView);
					var btnApply = rootElement.Q<Button>("btnApply");
					btnApply.clicked += () =>
					{
						CustomToolbarSettings.Instance.Save();
					};
				}
			};
		}

		private CustomToolbarSettingsProvider(string path, SettingsScope scopes = SettingsScope.User) : base(path, scopes){}

		public override void OnActivate(string searchContext, VisualElement rootElement) 
		{
			//_toolbarSettings = CustomToolbarSettings.GetSerializedSettings();
			//_elementsList ??= CustomToolbarReordableList.Create(_toolbarSettings);
		}
		
		public override void OnGUI(string searchContext)
		{
			// base.OnGUI(searchContext);
			// _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
			// _elementsList.DoLayoutList();
			// EditorGUILayout.EndScrollView();
			//
			// if (GUI.changed) 
			// {
			// 	CustomToolbarSettings.Save(_toolbarSettings);
			// }
		}
	}
}
#endif