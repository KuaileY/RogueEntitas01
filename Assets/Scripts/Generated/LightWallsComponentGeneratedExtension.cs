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

        static readonly LightWallsComponent lightWallsComponent = new LightWallsComponent();

        public bool isLightWalls {
            get { return HasComponent(ComponentIds.LightWalls); }
            set {
                if(value != isLightWalls) {
                    if(value) {
                        AddComponent(ComponentIds.LightWalls, lightWallsComponent);
                    } else {
                        RemoveComponent(ComponentIds.LightWalls);
                    }
                }
            }
        }

        public Entity IsLightWalls(bool value) {
            isLightWalls = value;
            return this;
        }
    }

    public partial class Matcher {

        static IMatcher _matcherLightWalls;

        public static IMatcher LightWalls {
            get {
                if(_matcherLightWalls == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.LightWalls);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherLightWalls = matcher;
                }

                return _matcherLightWalls;
            }
        }
    }
}
