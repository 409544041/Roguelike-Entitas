//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ActionInProgressComponent actionInProgressComponent = new ActionInProgressComponent();

    public bool isActionInProgress {
        get { return HasComponent(GameComponentsLookup.ActionInProgress); }
        set {
            if (value != isActionInProgress) {
                if (value) {
                    AddComponent(GameComponentsLookup.ActionInProgress, actionInProgressComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.ActionInProgress);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherActionInProgress;

    public static Entitas.IMatcher<GameEntity> ActionInProgress {
        get {
            if (_matcherActionInProgress == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActionInProgress);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActionInProgress = matcher;
            }

            return _matcherActionInProgress;
        }
    }
}
