#if UNITY_EDITOR
#if CUSTOM_TOOLBAR
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]打开场景", "在toolbar上增加一个按钮以打开指定的场景")]
	internal class ToolbarSceneOpen : BaseToolbarElement
	{
		public override string CountingSubKey => openSceneName;

		public override void Init()
		{
		}

		[SerializeField] private string displayText = "BTN";
		[SerializeField] private string openSceneName;

		protected override void OnDrawInSettings(VisualElement container)
		{
			// position.x += position.width + FieldSizeSpace * 3;
			// position.width = DefaultSectionWidth;
			// displayText = EditorGUI.TextField(position, "按钮显示文字", displayText);
			//
			// position.x += position.width + FieldSizeSpace * 3;
			// position.width = 300;
			// openSceneName = EditorGUI.TextField(position, "打开的场景名", openSceneName);
			// if (GUI.changed)
			// {
			// 	//ToolbarCallback.RefreshToolbar();
			// }
		}

		private GUIContent content = new();

		protected override void OnDrawInToolbar(VisualElement container)
		{
			EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
			content.text = displayText;
			content.tooltip = $"打开场景 {openSceneName}";
			if (GUILayout.Button(content, Style))
			{
				if (string.IsNullOrEmpty(openSceneName))
				{
					Debug.LogError($"找不到需要打开的名为[{openSceneName}]的场景");
				}
				else
				{
					var sceneGuids = AssetDatabase.FindAssets($"t:scene {openSceneName}", new[] { "Assets" });
					if (sceneGuids.Length > 0)
					{
						var path = AssetDatabase.GUIDToAssetPath(sceneGuids[0]);
						EditorSceneManager.OpenScene(path);
						Counting();
					}
					else
					{
						Debug.LogError($"找不到需要打开的名为[{openSceneName}]的场景");
					}
				}
			}

			EditorGUI.EndDisabledGroup();
		}
	}
}
#endif
#endif
