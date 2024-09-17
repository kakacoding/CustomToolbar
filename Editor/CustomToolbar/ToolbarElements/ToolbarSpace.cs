#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[分隔]插入间隔", "在功能控件之间插入间隔")]
	internal class ToolbarSpace : BaseToolbarElement
	{
		public override void Init()
		{
		}

		protected override void OnDrawInSettings(VisualElement container)
		{
			// position.x += position.width + FieldSizeSpace * 3;
			// position.width = DefaultSectionWidth;
			// EditorGUI.LabelField(position, "-----------------");
			//WidthInToolbar = EditorGUI.IntField(position, $"插入间隔", WidthInToolbar);
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			EditorGUILayout.Separator();
		}
	}
}
#endif
