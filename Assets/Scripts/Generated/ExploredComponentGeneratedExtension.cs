//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {

    public partial class Entity {

        static readonly ExploredComponent exploredComponent = new ExploredComponent();

        public bool isExplored {
            get { return HasComponent(ComponentIds.Explored); }
            set {
                if(value != isExplored) {
                    if(value) {
                        AddComponent(ComponentIds.Explored, exploredComponent);
                    } else {
                        RemoveComponent(ComponentIds.Explored);
                    }
                }
            }
        }

        public Entity IsExplored(bool value) {
            isExplored = value;
            return this;
        }
    }

    public partial class Matcher {

        static IMatcher _matcherExplored;

        public static IMatcher Explored {
            get {
                if(_matcherExplored == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Explored);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherExplored = matcher;
                }

                return _matcherExplored;
            }
        }
    }
}