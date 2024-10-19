﻿#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]打开场景", "在toolbar上增加一个按钮以打开指定的场景")]
	internal class ToolbarSceneOpen : BaseToolbarElement
	{
		[JsonProperty]
		internal CustomToolbarUtility.ETextTextureDisplay SettingDisplayType;
		[JsonProperty]
		internal string BtnText;
		[JsonProperty]
		internal string TexturePath;
		[JsonProperty]
		internal string SceneName;
		
		private const string StrSceneName = "场景名字";
		private const string StrSceneNull = "未设置场景名";
		
		private string Tooltip => string.IsNullOrEmpty(SceneName) ? StrSceneNull : $"打开场景 {SceneName}";

		protected override void OnDrawInSettings(VisualElement container)
		{
			base.OnDrawInSettings(container);
			
			container.Add(TextureTextCtrl.Create(
				()=>SettingDisplayType,
				v=>SettingDisplayType=v,
				()=>BtnText,
				v=>BtnText=v,
				()=>TexturePath,
				v=>TexturePath=v
			));
			
			container.Add(ParamsCtrl.Create(
				()=>StrSceneName,
				()=>SceneName,
				v=>SceneName=v
			));
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			container.Add(ImageButtonCtrl.Create(
				()=>SettingDisplayType,
				()=>BtnText,
				()=>TexturePath,
				()=>Tooltip,
				() =>
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
				}));
		}
	}
}
#endif
