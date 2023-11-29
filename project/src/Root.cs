using EventBusSourceGenerator;
using SCALE.Enums;
using SCALE.GameData;
using SCALE.Scripts.Managers;
using StringyEnums;
using AudioManager = SCALE.Scripts.Managers.AudioManager;
using Camera = SCALE.Scripts.Camera;
using EventBus = SCALE.Events.EventBus;
using GameStateManager = SCALE.Scripts.Managers.GameStateManager;
using PopupManager = SCALE.Scripts.Managers.PopupManager;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace SCALE;

public partial class Root : Node2D
{
	public static EventBus EventBus = null!;
	public static Viewport Tree = null!;
	public static SceneTree SceneTree = null!;
	public static GameStateManager GameManager = null!;
	public static AudioManager AudioManager = null!;
	public static PopupManager PopupManager = null!;
	public static Data Data = new Data()!;
	public static PersistedDataManager PersistedDataManager = null!;
	public static WindowManager WindowManager = null!;
	
	// Guarantee that it's only run once in the lifetime of the program
	// As calling this duplicate times will throw exceptions
	public static int _ = Data.RegisterMappings();

	public static bool IsGameInitialized = false;

	private Camera? _camera;

	
	public override void _EnterTree()
	{
		Tree = GetTree().Root;
		EnumCore.Init();
		SceneTree = GetTree();
		InitializeEventBus();
		InitializeWindowManager();
		InitializeGameManager();
		InitializeAudioManager();
		InitializeCamera();
		InitializePopupManager();
		InitializePersistedDataManager();
		
		InitializeGameData();

		ParseCmdArgs();
	}

	/// <summary>
	/// Supported cli arg now:
	/// --saveGameDebug=load
	/// --saveGameDebug=save
	/// --debugLoadLevelName=6aba16dca6f441619342394ad97f0ba6.v1.level.bin
	/// saveGameDebug opts will cause the game to load or save the game first thing when it boots up, allowing easy debugging using breakpoints.
	/// 
	/// </summary>
	public void ParseCmdArgs()
	{
		var args = OS.GetCmdlineArgs();

		foreach (var arg in args)
		{
			GD.Print($"Got custom command argument: {arg}");

			var parts = arg.Split('=');

			if (parts.Length == 2)
			{
				string label = parts[0].TrimStart('-');
				string value = parts[1];

				if (label.Equals("saveGameDebug", StringComparison.InvariantCultureIgnoreCase))
				{
					switch (value)
					{
						case "load":
							HandleSaveGameDebugLoad();
							break;
						case "save":
							HandleSaveGameDebugSave();
							break;
					}
				}
			}
		}
	}

	private static void HandleSaveGameDebugLoad()
	{
		var data = Data.Load();
#pragma warning disable CS0219
		// Put breakpoint here
		var _ = 0;
#pragma warning restore CS0219
	}

	private static void HandleSaveGameDebugSave()
	{
		Data.Save();
#pragma warning disable CS0219
		// Put breakpoint here
		var _ = 0;
#pragma warning restore CS0219
	}

	public override void _Ready()
	{
		if (Data is not null)
		{
			IsGameInitialized = true;
		}
	}

	private void InitializeWindowManager()
	{
		if (WindowManager is null)
		{
			WindowManager = new WindowManager();
			WindowManager.Name = nameof(WindowManager);
			AddChild(WindowManager, true);
		}
	}

	private static void InitializeGameData()
	{
		if (!IsGameInitialized)
		{
			Data = Data.Load();
		}
	}

	private void InitializePersistedDataManager()
	{
		if (PersistedDataManager is null)
		{
			PersistedDataManager = new PersistedDataManager();
			PersistedDataManager.Name = nameof(PersistedDataManager);
			AddChild(PersistedDataManager, true);
		}
	}

	private void InitializePopupManager()
	{
		if (PopupManager is null)
		{
			PopupManager = new PopupManager();
			PopupManager.Name = nameof(PopupManager);
			AddChild(PopupManager, true);
		}
	}

	private void InitializeEventBus()
	{
		if (EventBus is null)
		{
			EventBus = new EventBus();
			EventBus.Name = nameof(EventBus);
			AddChild(EventBus, true);
		}
	}

	private void InitializeGameManager()
	{
		if (GameManager is null)
		{
			GameManager = GameStateManager.Instance;
			GameManager.Name = nameof(GameManager);
			AddChild(GameManager, true);
		}
	}

	private void InitializeAudioManager()
	{
		if (AudioManager is null)
		{
			AudioManager = AudioManager.Instance;
			AudioManager.Name = nameof(AudioManager);
			AddChild(AudioManager, true);
		}
	}

	private void InitializeCamera()
	{
		if (_camera is null)
		{
			_camera = new Camera()
			{
				Enabled = true,
			};
			_camera.Name = "Main Camera";
			AddChild(_camera, true);
		}
	}

	public override void _Process(double delta)
	{
	}
}
