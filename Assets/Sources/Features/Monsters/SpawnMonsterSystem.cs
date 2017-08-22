﻿namespace Assets.Sources.Features.Monsters
{
	using System.Collections.Generic;
	using Actions;
	using Entitas;
	using Helpers;
	using Helpers.Map;
	using Helpers.SystemDependencies.Attributes;
	using Helpers.SystemDependencies.Phases;
	using MapTracker;

	[ExecutePhase(ExecutePhase.ReactToActions)]
	[DependsOn(typeof(ActionsFeature), typeof(MapTrackerSystem))]
	public class SpawnMonsterSystem : ReactiveSystem<ActionsEntity>
	{
		private readonly GameContext gameContext;

		public SpawnMonsterSystem(Contexts contexts) : base(contexts.actions)
		{
			gameContext = contexts.game;
		}

		protected override ICollector<ActionsEntity> GetTrigger(IContext<ActionsEntity> context)
		{
			return context.CreateCollector(ActionsMatcher.Action.Added());
		}

		protected override bool Filter(ActionsEntity entity)
		{
			return entity.hasAction && entity.action.Action is SpawnMonsterAction;
		}

		protected override void Execute(List<ActionsEntity> entities)
		{
			foreach (var entity in entities)
			{
				var action = entity.action.Action as SpawnMonsterAction;

				if (!EntityMap.Instance.IsWalkable(action.Position))
				{
					entity.Destroy(); // TODO: same problem as destroying movement actions
					continue;
				}

				gameContext.CreateMonster(action.Position, action.Type, action.Entity);
			}
		}
	}
}