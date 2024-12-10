using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hoi3Launcher.Models;
using Hoi3Launcher.Services;

namespace Hoi3Launcher.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
	[ObservableProperty]
	private Dlc? _selectedDlc;

	private readonly ModScanner _modScanner = new();
    private readonly LastModsService _lastModsService = new();
    private readonly Launcher _launcher = new();

	public ObservableCollection<Dlc> Dlcs { get; } = new ObservableCollection<Dlc>();

	public void Load()
	{
		Dlcs.Add(new Dlc("Their Finest Hour", "hoi3_tfh.exe", _modScanner.LoadMods("tfh/mod/")));
		Dlcs.Add(new Dlc("For the Motherland", "hoi3game.exe", _modScanner.LoadMods("mod/")));
		_lastModsService.Load(Dlcs);
		SelectedDlc = Dlcs.First();
	}

	[RelayCommand]
	public async Task Launch()
	{
		if (SelectedDlc is null)
			return;

		await _lastModsService.Save(Dlcs);
		_launcher.LaunchGame(SelectedDlc);
		_launcher.CloseApplication();
	}
}
