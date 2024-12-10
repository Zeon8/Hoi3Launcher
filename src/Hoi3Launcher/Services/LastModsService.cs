using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Hoi3Launcher.Models;

namespace Hoi3Launcher.Services;

public class LastModsService
{
	public void Load(IEnumerable<Dlc> dlcs)
	{
		string path = GetPath();
		if (!File.Exists(path))
			return;

		JsonNode? json = JsonNode.Parse(File.ReadAllText(path));
		if (json is null)
			return;

		foreach (Dlc dlc in dlcs)
		{
			JsonNode? modsNode = json[dlc.Name];
			if (modsNode is null)
				continue;

			HashSet<string> mods = modsNode.AsArray()
				.Select(m => m!.AsValue().GetValue<string>())
				.ToHashSet();

			foreach (Mod mod in dlc.Mods)
			{
				if (mods.Contains(mod.Name))
					mod.IsEnabled = true;
			}
		}
	}

	private static string GetPath()
	{
		return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Hoi3Launcher/mods.json");
	}

	public Task Save(IEnumerable<Dlc> dlcs)
	{
		JsonNode node = new JsonObject();
		foreach (Dlc dlc in dlcs)
		{
			JsonValue[] jsonMods = (from m in dlc.Mods
				where m.IsEnabled
				select JsonValue.Create(m.Name)).ToArray();
			string name = dlc.Name;
			JsonNode[] items = jsonMods;
			node[name] = new JsonArray(items);
		}
		string path = GetPath();
		Directory.CreateDirectory(Path.GetDirectoryName(path));
		return File.WriteAllTextAsync(path, node.ToJsonString());
	}
}
