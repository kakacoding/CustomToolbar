#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
	[Serializable]
	[ToolbarElementDisplay("[分隔]左右区域分割", "在本项目以上的条目在运行按钮的左边区域，以下的项目在右边")]
	internal class ToolbarSides : BaseToolbarElement
	{
		public override void Init()
		{
		}

		protected override void OnDrawInSettings(VisualElement container)
		{
		}

		protected override void OnDrawInToolbar(VisualElement container)
		{
		}
	}
}
#endif
