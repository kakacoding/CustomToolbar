#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]第三方程序调用", "在toolbar上增加一个按钮以打开第三方程序，格式为Application.dataPath的相对路径")]
	internal class ToolbarThirdPartyInvoke : BaseToolbarElement
	{
		public override void Init()
		{
		}

		public override string CountingSubKey => Path.GetFileName(relativePath);
		[SerializeField] public string displayText = "第三方程序";
		[SerializeField] public string relativePath;

		private GUIContent content = new();

		protected override void OnDrawInSettings(VisualElement container)
		{
			// position.x += position.width + FieldSizeSpace * 3;
			// position.width = DefaultSectionWidth;
			// displayText = EditorGUI.TextField(position, "按钮显示文字", displayText);
			//
			// position.x += position.width + FieldSizeSpace * 3;
			// position.width = 600;
			// relativePath = EditorGUI.TextField(position, "相对路径", relativePath);
			// if (GUI.changed)
			// {
			// 	//ToolbarCallback.RefreshToolbar();
			// }
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			content.text = displayText;
			content.tooltip = $"调用第三方程序 {relativePath}";
			if (GUILayout.Button(content, Style))
			{
				var path = Path.GetFullPath(Path.Combine(Application.dataPath, relativePath));
				if (string.IsNullOrEmpty(relativePath))
				{
					Debug.LogError($"第三方程序路径为空");
				}
				else if (!File.Exists(path))
				{
					Debug.LogError($"第三方程序路径不存在:{path}");
				}
				else
				{
#pragma warning disable 4014
					//EditorTools.RunExeAsync(path, "",false);
#pragma warning restore 4014
					Counting();
				}
			}
		}
	}
}
#endif
