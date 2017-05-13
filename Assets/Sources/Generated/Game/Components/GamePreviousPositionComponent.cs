//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PreviousPositionComponent previousPosition { get { return (PreviousPositionComponent)GetComponent(GameComponentsLookup.PreviousPosition); } }
    public bool hasPreviousPosition { get { return HasComponent(GameComponentsLookup.PreviousPosition); } }

    public void AddPreviousPosition(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.PreviousPosition;
        var component = CreateComponent<PreviousPositionComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePreviousPosition(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.PreviousPosition;
        var component = CreateComponent<PreviousPositionComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePreviousPosition() {
        RemoveComponent(GameComponentsLookup.PreviousPosition);
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

    static Entitas.IMatcher<GameEntity> _matcherPreviousPosition;

    public static Entitas.IMatcher<GameEntity> PreviousPosition {
        get {
            if (_matcherPreviousPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PreviousPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPreviousPosition = matcher;
            }

            return _matcherPreviousPosition;
        }
    }
}
