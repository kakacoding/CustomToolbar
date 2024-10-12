#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]打开场景", "在toolbar上增加一个按钮以打开指定的场景")]
	internal class ToolbarSceneOpen : BaseToolbarElement
	{
		[JsonProperty]
		internal string BtnText;
		[JsonProperty]
		internal string SceneName;
		
		private const string StrShow = "按钮显示文字";
		private const string StrSceneName = "场景名字";
		private const string StrSceneNull = "未设置场景名";
		
		private string Tooltip => string.IsNullOrEmpty(SceneName) ? StrSceneNull : $"打开场景 {SceneName}";

		protected override void OnDrawInSettings(VisualElement container)
		{
			base.OnDrawInSettings(container);
			
			var txtBtnText = new TextField
			{
				label = StrShow,
				value = BtnText,
			};
			txtBtnText.AddToClassList(CustomToolbarUtility.SETTING_TEXT_SMALL);
			txtBtnText.RegisterValueChangedCallback(evt =>
			{
				BtnText = evt.newValue;
			});
			container.Add(txtBtnText);

			var txtSceneName = new TextField
			{
				label = StrSceneName,
				value = SceneName,
			};
			txtSceneName.AddToClassList(CustomToolbarUtility.SETTING_TEXT_LARGE);
			txtSceneName.RegisterValueChangedCallback(evt =>
			{
				SceneName = evt.newValue;
			});			
			container.Add(txtSceneName);
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			var toolbarBtn = new ToolbarButton  
			{
				text = BtnText,
				tooltip = Tooltip,
			};
			toolbarBtn.AddToClassList(CustomToolbarUtility.TOOLBAR_BTN_MENU_INVOKE);
			toolbarBtn.style.width = toolbarBtn.text.Length * 15;
			toolbarBtn.clicked += () =>
			{
				if (string.IsNullOrEmpty(SceneName))
				{
					CustomToolbarUtility.LogError(StrSceneNull);
				}
				else
				{
					var sceneGuids = AssetDatabase.FindAssets($"t:scene {SceneName}", new[] { "Assets" });
					if (sceneGuids.Length > 0)
					{
						var path = AssetDatabase.GUIDToAssetPath(sceneGuids[0]);
						EditorSceneManager.OpenScene(path);
					}
					else
					{
						CustomToolbarUtility.LogError($"找不到需要打开的名为[{SceneName}]的场景");
					}
				}
			};
			
			container.Add(toolbarBtn);
		}
	}
}
#endif
