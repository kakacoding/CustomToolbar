#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[按钮]调用菜单", "在toolbar上增加一个按钮以打开指定的菜单，格式为【Help/About Unity】")]
	internal class ToolbarMenuInvoke : BaseToolbarElement
	{
		[JsonProperty]
		internal string InvokeMenuDisplay;
		[JsonProperty]
		internal string InvokeMenuPath;
	
		private const string StrShow = "按钮显示文字";
		private const string StrMenuPath = "调用的菜单路径";
		private const string StrMenuNull = "菜单命令为空";
		private string Tooltip => $"调用菜单 {InvokeMenuPath}";

		private const string TOOLBAR_BTN_INVOKE_MENU = "toolbar-btn-invoke-menu";
		private const string SETTING_INVOKE_MENU_PATH = "setting-text-invoke-menu-path";
		private const string SETTING_INVOKE_MENU_DISPLAY = "setting-text-invoke-menu-display";
		public override void Init()
		{
		}

		public override string CountingSubKey
		{
			get 
			{
				if (!string.IsNullOrEmpty(InvokeMenuPath))
				{
					var arr = InvokeMenuPath.Split('/');
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
			TextField txt;
			container.Add(txt = new TextField
			{
				label = StrShow,
				value = InvokeMenuDisplay,
			});
			txt.AddToClassList(SETTING_INVOKE_MENU_DISPLAY);
			txt.RegisterValueChangedCallback(evt =>
			{
				InvokeMenuDisplay = evt.newValue;
			});
			
			container.Add(txt = new TextField
			{
				label = StrMenuPath,
				value = InvokeMenuPath
			});
			txt.AddToClassList(SETTING_INVOKE_MENU_PATH);
			txt.RegisterValueChangedCallback(evt =>
			{
				InvokeMenuPath = evt.newValue;
			});
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			var toolbarBtn = new ToolbarButton  
			{
				text = InvokeMenuDisplay,
				tooltip = Tooltip,
			};
			toolbarBtn.AddToClassList(TOOLBAR_BTN_INVOKE_MENU);
			toolbarBtn.style.width = toolbarBtn.text.Length * 10;
			toolbarBtn.clicked += () =>
			{
				if (string.IsNullOrEmpty(InvokeMenuPath))
				{
					CustomToolbarUtility.LogError(StrMenuNull);
				}
				else
				{
					EditorApplication.ExecuteMenuItem(InvokeMenuPath);
					Counting();
				}
			};
			
			container.Add(toolbarBtn);
		}
	}
}
#endif
