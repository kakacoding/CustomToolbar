#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CustomToolbar.Editor
{
	internal class CustomToolbarSettings
	{
		private const string SettingsPath = "ProjectSettings/CustomToolbar/CustomToolbarSettings.json";
		public List<BaseToolbarElement> Elements { get; set; } = new();

		private static readonly JsonSerializerSettings JsonSetting = new()
		{
			TypeNameHandling = TypeNameHandling.Auto,
			NullValueHandling = NullValueHandling.Ignore,
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
		};

		internal static CustomToolbarSettings Instance
		{
			get
			{
				if (_instance != null) return _instance;
				
				if (File.Exists(SettingsPath))
				{
					var json = File.ReadAllText(SettingsPath);
					_instance = JsonConvert.DeserializeObject<CustomToolbarSettings>(json, JsonSetting) ?? new CustomToolbarSettings();
				}
				else
				{
					_instance = new CustomToolbarSettings
					{
						Elements = new List<BaseToolbarElement>
						{
							new ToolbarMenuInvoke
							{
								InvokeMenuDisplay = "M",
								InvokeMenuPath="Assets/1"
							},
						}
					};
					_instance.Save();
				}
				return _instance;
			}
		}

		private static CustomToolbarSettings _instance;

		internal void Save()
		{
			var json = JsonConvert.SerializeObject(this, Formatting.None, JsonSetting);
			File.WriteAllText(SettingsPath, json);
		}
	}
}
#endif
