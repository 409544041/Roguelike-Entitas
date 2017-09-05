﻿namespace Assets.Scripts
{
	using Sources.Features.Actions;
	using Sources.Features.Camera;
	using Sources.Features.Coroutines;
	using Sources.Features.FogOfWar;
	using Sources.Features.Items;
	using Sources.Features.Lights;
	using Sources.Features.MapTracker;
	using Sources.Features.Monsters;
	using Sources.Features.Movement;
	using Sources.Features.Networking;
	using Sources.Features.View;
	using Sources.Helpers;
	using Sources.Helpers.SystemDependencies;
	using Entitas;
	using Sources.Features.AI;
	using Sources.Features.Combat;
	using Sources.Features.Config;
	using Sources.Features.Loot;
	using Sources.Features.ProcGen;
	using Sources.Features.Stats;
	using Sources.Helpers.Networking;
	using Sources.Helpers.Networking.ControlMessages;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using Feature = Feature;

	public class GameController : MonoBehaviour
	{
		public GameState GameState { get; private set; }

		Systems systems;
		public GameObject CameraObject;
		public GameObject StartGameOverlay;
		public GameObject GameOverCanvas;

		public GameController()
		{
			GameState = GameState.NotStarted;
		}

		public void InitGame()
		{
			GameState = GameState.WaitingForPlayers;
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = -1;

			// Get a reference to the contexts
			var contexts = Contexts.sharedInstance;

			contexts.game.SetEventQueue(new EventQueue<GameEntity>());
			contexts.game.SetCamera(CameraObject.GetComponent<Camera>());
			contexts.game.SetDatabases(new DatabasesHandler());
			contexts.game.isGameBoard = true;
			contexts.game.gameBoardEntity.AddRectangularMap(100, 100);
			contexts.game.AddService(GetComponent<InventoryController>());
			contexts.game.AddService(this); // TODO: this may be dangerous.. Use wisely!

			var config = new Config();
			contexts.game.SetConfig(config);
			contexts.game.AddService(config);

			systems = new Feature("Systems");
			var systemsRoot = new SystemsRoot(!NetworkController.Instance.IsMultiplayer || NetworkController.Instance.IsServer);
			systemsRoot
				.Add(new LootFeature(contexts))
				.Add(new AiFeature(contexts))
				.Add(new StatsFeature(contexts))
				.Add(new CombatFeature(contexts))
				.Add(new ItemsFeature(contexts))
				.Add(new MonstersFeature(contexts))
				// .Add(new FogOfWarFeature(contexts))
				.Add(new MapTrackerSystem(contexts))
				.Add(new LightsFeature(contexts))
				.Add(new NetworkingFeature(contexts))
				.Add(new CoroutinesFeature(contexts))
				.Add(new MovementFeature(contexts))
				.Add(new PlayerCentricCameraSystem(contexts))
				.Add(new ActionsFeature(contexts))
				.Add(new ProcGenFeature(contexts))
				.Add(new ViewFeature(contexts));

			systemsRoot.SetupOrder();
			systems.Add(systemsRoot);

			
			// call Initialize() on all of the IInitializeSystems*/
			systems.Initialize();

			if (NetworkController.Instance.IsMultiplayer)
			{
				// TODO: possible memory leak with events and registered handlers
				NetworkController.Instance.OnGameStarted += StartGame;
				NetworkController.Instance.SendWaitingForPlayers();
				NetworkController.Instance.OnGameEnded += OnHostDisconnected;
			}
			else
			{
				StartGame();
			}
		}

		void Start()
		{
			if (GameState == GameState.NotStarted)
			{
				InitGame();
			}
		}

		private void OnDestroy()
		{
			NetworkController.Instance.OnGameStarted -= StartGame;
			NetworkController.Instance.OnGameEnded -= OnHostDisconnected;
		}

		void Update()
		{
			if (GameState == GameState.Running || (GameState == GameState.WaitingForPlayers && !NetworkController.Instance.IsServer))
			{
				// call Execute() on all the IExecuteSystems and 
				// ReactiveSystems that were triggered last frame
				systems.Execute();
				// call cleanup() on all the ICleanupSystems
				systems.Cleanup();
			}
		}

		public void PauseGame()
		{
			GameState = GameState.Paused;
		}

		public void UnpauseGame()
		{
			GameState = GameState.Running;
		}

		public void StopGame()
		{
			GameState = GameState.NotStarted;

			if (systems != null)
			{
				systems.TearDown();
				systems.ClearReactiveSystems();
				systems.DeactivateReactiveSystems();
			}
			Contexts.sharedInstance.Reset();
		}

		public void StartGame()
		{
			StartGameOverlay.SetActive(false);
			GameState = GameState.Running;
		}

		private void OnHostDisconnected()
		{
			StopGame();
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		}

		public void GameOver()
		{
			PauseGame();
			GameOverCanvas.SetActive(true);
		}
	}
}