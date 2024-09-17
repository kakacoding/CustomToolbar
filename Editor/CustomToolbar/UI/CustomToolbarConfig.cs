#if UNITY_EDITOR && CUSTOM_TOOLBAR
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CustomToolbar.Editor
{
	internal class CustomToolbarConfig
	{
		private const string ConfigPath = "ProjectSettings/CustomToolbar/CustomToolbarConfig.json";
		[JsonProperty]
		internal List<BaseToolbarElement> Elements { get; set; } = new();
		private static readonly JsonSerializerSettings JsonSetting = new()
		{
			TypeNameHandling = TypeNameHandling.Auto,
			NullValueHandling = NullValueHandling.Ignore,
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
		};

		internal static CustomToolbarConfig Instance
		{
			get
			{
				if (_instance != null) return _instance;
				
				if (File.Exists(ConfigPath))
				{
					var json = File.ReadAllText(ConfigPath);
					_instance = JsonConvert.DeserializeObject<CustomToolbarConfig>(json, JsonSetting);
					if (_instance != null) return _instance;
				}
				
				_instance = new CustomToolbarConfig();
				_instance.Save();
				return _instance;
			}
		}

		private static CustomToolbarConfig _instance;

		internal void Save()
		{
			var json = JsonConvert.SerializeObject(this, Formatting.Indented, JsonSetting);
			File.WriteAllText(ConfigPath, json);
			ToolbarExtender.Reload();
		}
	}
}
#endif
