#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]Apply All Override并保存场景", "|Apply All Override并保存场景")]
	internal class ToolbarSaveScene : BaseToolbarElement
	{
		public override void Init()
		{
		}

		protected override void OnDrawInSettings(VisualElement container)
		{
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			//GUI.enabled = EditorHook.IsBattleScene(EditorSceneManager.GetActiveScene());
			//GUI.enabled = true;

			//EditorGUI.BeginDisabledGroup(!EditorHook.IsBattleScene(EditorSceneManager.GetActiveScene()));
			if (GUILayout.Button(EditorGUIUtility.IconContent("SaveAs", ToolbarElementDisplay.GetDetail(GetType())),
				    Style))
			{
				//EditorHook.ForceApply = true;
				EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
				Counting();
			}

			EditorGUI.EndDisabledGroup();
		}
	}
}
#endif
