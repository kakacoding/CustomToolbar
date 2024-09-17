// #if UNITY_EDITOR && CUSTOM_TOOLBAR
// using UnityEngine;
// using UnityEditor;
// using UnityEditorInternal;
//
// namespace CustomToolbar.Editor
// {
// 	internal static class CustomToolbarReordableList
// 	{
// 		private const string HelpMsg = "选中任一项目查看其相应说明";
// 		private static string _toolMsg = "";
//
// 		internal static ReorderableList Create(SerializedObject settings)
// 		{
// 			return null;
// 			CustomToolbarSettings  targetObject;// = (CustomToolbarSettings)settings.targetObject;
// 			var curToolbarElements = targetObject?.Elements;
// 			var reorderableList =
// 				new ReorderableList(curToolbarElements, typeof(BaseToolbarElement), true, true, true, true);
//
// 			reorderableList.elementHeight = EditorGUIUtility.singleLineHeight + 4;
// 			reorderableList.drawElementCallback = (position, index, _, _) =>
// 			{
// 				curToolbarElements?[index].DrawInList(position);
// 			};
//
// 			reorderableList.drawHeaderCallback = rect =>
// 			{
// 				//GUI.Label(rect,  string.IsNullOrEmpty(_toolMsg) ? HelpMsg : _toolMsg);
// 				GUI.Label(rect, "编辑工具项 - 按ctrl+s保存");
// 			};
//
// 			reorderableList.onSelectCallback = list =>
// 			{
// 				_toolMsg = "";
// 				if (0 <= list.index && list.index < list.count)
// 				{
// 					var item = list.list[list.index];
// 					if (item != null)
// 					{
// 						_toolMsg = ToolbarElementDisplay.GetHelp(item.GetType());
// 					}
// 				}
// 			};
//
// 			reorderableList.onReorderCallback = _ => { /*ToolbarCallback.RefreshToolbar();*/ };
//
// 			reorderableList.onAddDropdownCallback = (_, _) =>
// 			{
// 				BaseToolbarElement[] toolbarElements =
// 				{
// 					new ToolbarSetting(),
// 					new ToolbarSceneSelection(),
// 					new ToolbarSceneOpen(),
// 					new ToolbarMenuInvoke(),
// 					new ToolbarThirdPartyInvoke(),
// 					null,
// 					new ToolbarWorkspace(),
// 					new ToolbarMute(),
// 					new ToolbarStopSound(),
// 					new ToolbarExportContentData(),
// 					new ToolbarSaveScene(),
// 					null,
// 					null,
// 					new ToolbarSides(),
// 					new ToolbarSpace(),
// 				};
//
// 				var menu = new GenericMenu();
// 				foreach (var toolbarElement in toolbarElements)
// 				{
// 					if (toolbarElement == null)
// 					{
// 						menu.AddSeparator("");
// 					}
// 					else
// 					{
// 						var display = ToolbarElementDisplay.GetDisplay(toolbarElement.GetType());
// 						menu.AddItem(new GUIContent(display), false, target =>
// 						{
// 							curToolbarElements?.Add(target as BaseToolbarElement);
// 							if (target is ToolbarSceneSelection selection)
// 							{
// 								selection.Init();
// 							}
// 						}, toolbarElement);
// 					}
// 				}
//
// 				menu.ShowAsContext();
// 			};
//
// 			reorderableList.onChangedCallback = _ =>
// 			{
// 				//ToolbarCallback.RefreshToolbar();
// 			};
// 			return reorderableList;
// 		}
// 	}
// }
// #endif