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
		internal string BtnText;
		[JsonProperty]
		internal string MenuInvokePath;
	
		private const string StrShow = "按钮显示文字";
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
			
			var txtBtnText = new TextField
			{
				label = StrShow,
				value = BtnText,
			};
			txtBtnText.AddToClassList(SETTING_TEXT_SMALL);
			txtBtnText.RegisterCallback<ChangeEvent<string>>(evt =>
			{
				BtnText = evt.newValue;
			});
			container.Add(txtBtnText);

			var txtMenuPath = new TextField
			{
				label = StrMenuPath,
				value = MenuInvokePath
			};
			txtMenuPath.AddToClassList(SETTING_TEXT_LARGE);
			txtMenuPath.RegisterCallback<ChangeEvent<string>>(evt =>
			{
				MenuInvokePath = evt.newValue;
			});			
			container.Add(txtMenuPath);
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			var toolbarBtn = new ToolbarButton  
			{
				text = BtnText,
				tooltip = Tooltip,
			};
			toolbarBtn.AddToClassList(TOOLBAR_BTN_MENU_INVOKE);
			toolbarBtn.style.width = toolbarBtn.text.Length * 15;
			toolbarBtn.clicked += () =>
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
			};
			
			container.Add(toolbarBtn);
		}
	}
}
#endif
