#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]导表", "通过菜单选择，从Excel导出ContentData和Json")]
	internal class ToolbarExportContentData : BaseToolbarElement
	{
		public override string DisplayNameInToolbar => "导表";
		public override string CountingSubKey => "";

		public override void Init()
		{
		}

		protected override void OnDrawInSettings(VisualElement container)
		{
		}

		private GUIContent content = new();

		protected override void OnDrawInToolbar(VisualElement container)
		{
			content.text = DisplayNameInToolbar;
			content.tooltip = ToolbarElementDisplay.GetDetail(GetType());
			if (GUILayout.Button(content, Style))
			{
				Counting();
				var mousePosition = Event.current.mousePosition;
				EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "Tools/导出工具/从Excel导出",
					null);
				// Event.current.type == EventType.Used && Event.current.button == 0 or Event.current.type == EventType.Used && Event.current.button == 1
			}
		}
	}
}
#endif
