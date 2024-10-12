﻿#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine.Windows;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]调用菜单", "在toolbar上增加一个按钮以打开指定的菜单，格式为【Help/About Unity】")]
	internal class ToolbarMenuInvoke : BaseToolbarElement
	{
		[JsonProperty]
		internal CustomToolbarUtility.ETextTextureDisplay SettingDisplayType;
		[JsonProperty]
		internal string BtnText;
		[JsonProperty]
		internal string TexturePath;
		[JsonProperty]
		internal string MenuInvokePath;
		
		private const string StrMenuPath = "调用的菜单路径";
		private const string StrMenuNull = "菜单命令为空";
		private string Tooltip => $"调用菜单 {MenuInvokePath}";

		public override string CountingSubKey
		{
			get 
			{
				if (!string.IsNullOrEmpty(MenuInvokePath))
				{
					var arr = MenuInvokePath.Split('/');
					if (arr.Length > 1)
					{
						return arr[^1].Replace(" ", "_");
					}
				}

				return "";
			}
		}
		
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

			var txtMenuPath = new TextField(StrMenuPath)
			{
				value = MenuInvokePath
			};
			txtMenuPath.AddToClassList(CustomToolbarUtility.SETTING_TEXT_LARGE);
			txtMenuPath.RegisterValueChangedCallback(evt =>
			{
				MenuInvokePath = evt.newValue;
			});			
			container.Add(txtMenuPath);
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
					if (string.IsNullOrEmpty(MenuInvokePath))
					{
						CustomToolbarUtility.LogError(StrMenuNull);
					}
					else
					{
						EditorApplication.ExecuteMenuItem(MenuInvokePath);
						Counting();
					}
				}));
		}
	}
}
#endif
