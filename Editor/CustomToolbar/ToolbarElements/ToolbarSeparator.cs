#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using Newtonsoft.Json;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[分隔]插入间隔", "在功能控件之间插入间隔")]
	internal class ToolbarSeparator : BaseToolbarElement
	{
		[JsonProperty]
		internal int SeparatorPx = DefaultPx;
		internal const int DefaultPx = 5;
		
		private const string StrShow = "间隔像素值px";
		private const string SETTING_SEPARATOR_PX = "setting-text-separator-px";
		
		protected override void OnDrawInSettings(VisualElement container)
		{
			base.OnDrawInSettings(container);
			
			var txtShow = new TextField
			{
				label = StrShow,
				value = SeparatorPx.ToString(),
			};
			txtShow.AddToClassList(SETTING_SEPARATOR_PX);
			txtShow.RegisterValueChangedCallback(evt =>
			{
				if (!int.TryParse(evt.newValue, out SeparatorPx))
				{
					SeparatorPx = DefaultPx;
				}
			});
			container.Add(txtShow);
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
			var ve = new VisualElement
			{
				style =
				{
					width = SeparatorPx,
				}
			};
			container.Add(ve);
		}
	}
}
#endif
