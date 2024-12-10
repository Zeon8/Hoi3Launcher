using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Hoi3Launcher.Models;

namespace Hoi3Launcher.Services;

public class Launcher
{
	public void LaunchGame(Dlc dlc)
	{
		Process.Start(dlc.ExecutableName, dlc.Mods.Select(m => $"-mod={m.Path}"));
	}

	public void CloseApplication()
	{
        Application application = Application.Current!;
        if (application.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime)
		{
			lifetime.Shutdown(0);
		}
	}
}
