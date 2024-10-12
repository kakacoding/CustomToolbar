using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomToolbar.Editor
{
    public class ImageButtonToolbarCtrl : VisualElement
    {
	    internal delegate void Click();
        internal static VisualElement Create(Click clickCallback)
        {
            var toolbarBtn = new ToolbarButton  
            {
            	tooltip = Tooltip,
            };
            toolbarBtn.AddToClassList(CustomToolbarUtility.TOOLBAR_BTN_MENU_INVOKE);

            var bHasTex = false;
            Texture2D tex = null;
            if (!string.IsNullOrEmpty(TexturePath))
            {
            	tex = AssetDatabase.LoadAssetAtPath<Texture2D>(TexturePath);
            	if (tex == null)
            	{
            		tex = AssetDatabase.LoadAssetAtPath<Texture2D>(CustomToolbarUtility.LOST_ICON);
            	}
            	bHasTex = tex != null;
            }
            var helpBox = new HelpBox(BtnText, HelpBoxMessageType.Info)
            {
            	style =
            	{
            		flexDirection = SettingDisplayType == CustomToolbarUtility.ETextTextureDisplay.TextTexture ? FlexDirection.Row : FlexDirection.RowReverse
            	}
            };
            toolbarBtn.Add(helpBox);
            var texFieldInHelpBox = helpBox.Children().FirstOrDefault(child => child is not Label);
            if (texFieldInHelpBox != null)
            {
            	texFieldInHelpBox.style.backgroundImage = bHasTex ? tex : null;
            	texFieldInHelpBox.style.minWidth = bHasTex ? texFieldInHelpBox.style.minHeight : 0;
            	texFieldInHelpBox.style.maxWidth = bHasTex ? texFieldInHelpBox.style.maxHeight : 0;
            }

            toolbarBtn.style.width = string.IsNullOrEmpty(BtnText) ? 18 : BtnText.Length * 20 + (bHasTex ? 20 : 0);
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
        }
    }
}