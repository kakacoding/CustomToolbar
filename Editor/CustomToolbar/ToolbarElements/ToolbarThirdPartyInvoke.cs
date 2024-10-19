﻿#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]第三方程序调用", "在toolbar上增加一个按钮以打开第三方程序，格式为Application.dataPath的相对路径")]
	internal class ToolbarThirdPartyInvoke : BaseToolbarElement
	{
		[JsonProperty]
		internal CustomToolbarUtility.ETextTextureDisplay SettingDisplayType;
		[JsonProperty]
		internal string BtnText;
		[JsonProperty]
		internal string TexturePath;
		[JsonProperty]
		internal string ExecutePath;
		[JsonProperty]
		internal string Params;
		
		private const string StrExecutePath = "调用的程序路径";
		private const string StrExecuteNull = "程序路径命令为空";
		private const string StrParams = "调用参数";
		private string Tooltip => $"调用程序 {StrExecutePath}";
		
		public override string CountingSubKey => Path.GetFileName(ExecutePath);

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

			container.Add(PathCtrl.Create(
				()=>StrExecutePath,
				()=>ExecutePath,
				v=>ExecutePath=v
			));
			
			container.Add(ParamsCtrl.Create(
				()=>StrParams,
				()=>Params,
				v=>Params=v
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
					if (string.IsNullOrEmpty(ExecutePath))
					{
						CustomToolbarUtility.LogError(StrExecuteNull);
					}
					else
					{
						//EditorTools.RunExeAsync(path, "",false);
						//EditorApplication.ExecuteMenuItem(ExecutePath);
						Counting();
					}
				}));
		}
	}
}
#endif
