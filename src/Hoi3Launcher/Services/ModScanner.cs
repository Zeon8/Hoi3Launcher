using System;
using System.Collections.Generic;
using System.IO;
using Hoi3Launcher.Models;
using Hoi3Launcher.Utils;

namespace Hoi3Launcher.Services;

public class ModScanner
{
	public IEnumerable<Mod> LoadMods(string path)
	{
		if (!Directory.Exists(path))
			return [];

		var mods = new List<Mod>();
        var folder = new DirectoryInfo(path);
        foreach (FileInfo file in folder.EnumerateFiles())
		{
			if (!(file.Extension != ".mod"))
			{
				string name = ScriptUtils.ParseValue(File.ReadAllText(file.FullName), "name");
				mods.Add(new Mod(name, "mod/" + file.Name));
			}
		}
		return mods;
	}
}
