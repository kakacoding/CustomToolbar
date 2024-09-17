#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{

	[Serializable]
	[ToolbarElementDisplay("[按钮]P4Workspace", "显示对应的Perforce的Workspace，没有设置时点击可以打开设置界面")]
	internal class ToolbarWorkspace : BaseToolbarElement
	{
		public override void Init()
		{
		}

		protected override void OnDrawInSettings(VisualElement container)
		{
		}

		private GUIContent _unConfigBtn;
		private GUIContent UnConfigBtn => _unConfigBtn ??= new GUIContent("未配置P4", "点击开启配置窗口");
		private GUIContent ConfigBtn = new();

		protected override void OnDrawInToolbar(VisualElement container)
		{
			//EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
			var workspace = EditorUserSettings.GetConfigValue("vcPerforceWorkspace");
			if (!string.IsNullOrEmpty(workspace))
			{
				var bk = GUI.color;
				GUI.color = Provider.onlineState != OnlineState.Offline ? Color.green : Color.red;
				var tooltip = $@"点击本按钮 强制重连P4
红色表示P4 Disconnect
绿色表示P4 Connect
workspace路径:{Application.dataPath}";
				ConfigBtn.text = workspace;
				ConfigBtn.tooltip = tooltip;
				if (GUILayout.Button(ConfigBtn, Style))
				{
					Provider.UpdateSettings();
				}

				GUI.color = bk;
			}
			else
			{
				var bk = GUI.color;
				GUI.color = Color.red;
				if (GUILayout.Button(UnConfigBtn, Style))
				{
					Assembly assembly = Assembly.GetAssembly(typeof(EditorWindow));
					var t = assembly.GetType("UnityEditor.SettingsWindow");
					var obj = ScriptableObject.CreateInstance(t);
#if UNITY_2021_1_OR_NEWER
					t.GetMethod("Show", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(obj,
						new object[] { SettingsScope.Project, "Project/Version Control" });
#else
				t.GetMethod("Show", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(obj, new object[] { SettingsScope.Project, "Project/Editor" });
#endif
				}

				GUI.color = bk;
			}
			//EditorGUI.EndDisabledGroup();
		}
	}
}
#endif