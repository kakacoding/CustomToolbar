#if UNITY_EDITOR && CUSTOM_TOOLBAR
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace CustomToolbar.Editor
{
	internal class CustomToolbarSettingsProvider : SettingsProvider
	{
		private const string UXML = "Packages/com.kakacoding.customtoolbar/Editor/CustomToolbar/UI/ProviderSettings.uxml";
		private const string SETTING_PROVIDER_PATH = "Project/CustomToolbar";
		private const string SETTING_HORIZONTAL_PANEL = "setting-horizontal-panel";
		private const string SETTING_LISTVIEW = "setting-listview";
		private SerializedObject _toolbarSettings;
		private static ListView _toolbarListView;
		
		private static readonly BaseToolbarElement[] ToolbarElements =
		{
			new ToolbarMenuInvoke(),
			new ToolbarSeparator(),
			new ToolbarSides(),
			new ToolbarSceneOpen(),
		};
		
		private CustomToolbarSettingsProvider(string path, SettingsScope scopes = SettingsScope.User) : base(path, scopes){}
		
		[SettingsProvider]
		public static SettingsProvider CreateProvider()
		{
			return new SettingsProvider(SETTING_PROVIDER_PATH, SettingsScope.Project)
			{
				activateHandler = ActivateHandler
			};
		}

		private static void ActivateHandler(string _, VisualElement rootElement)
		{
			var window = EditorGUIUtility.Load(UXML) as VisualTreeAsset;
			if (window == null) return;
			window.CloneTree(rootElement);
			var sv = rootElement.Q<ScrollView>("toolbarScrollView");
			_toolbarListView = new ListView(CustomToolbarConfig.Instance.Elements, 20, () =>
			{
				var container = new VisualElement();
				container.AddToClassList(SETTING_HORIZONTAL_PANEL);
				return container;
			}, (container, i) =>
			{
				if (i < CustomToolbarConfig.Instance.Elements.Count)
				{
					CustomToolbarConfig.Instance.Elements[i].DrawInSettings(container);
				}
			}) { name = "lv", showAddRemoveFooter = true, reorderMode = ListViewReorderMode.Animated, reorderable = true, };
			
			_toolbarListView.AddToClassList(SETTING_LISTVIEW);
			_toolbarListView.itemsRemoved += _ => { _toolbarListView.Rebuild(); };
			_toolbarListView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(() =>
			{
				var menu = new GenericMenu();
				foreach (var toolbarElement in ToolbarElements)
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

							var idx = _toolbarListView.selectedIndex == -1 ? 0 : _toolbarListView.selectedIndex + 1;
							CustomToolbarConfig.Instance.Elements.Insert(idx, target as BaseToolbarElement);
							_toolbarListView.Rebuild();
						}, toolbarElement);
					}
				}

				menu.ShowAsContext();
			});

			sv.Add(_toolbarListView);
			var btnApply = rootElement.Q<Button>("btnApply");
			btnApply.clicked += () => { CustomToolbarConfig.Instance.Save(); };
		}
	}
}
#endif