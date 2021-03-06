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

        static readonly DrawComponent drawComponent = new DrawComponent();

        public bool isDraw {
            get { return HasComponent(ComponentIds.Draw); }
            set {
                if(value != isDraw) {
                    if(value) {
                        AddComponent(ComponentIds.Draw, drawComponent);
                    } else {
                        RemoveComponent(ComponentIds.Draw);
                    }
                }
            }
        }

        public Entity IsDraw(bool value) {
            isDraw = value;
            return this;
        }
    }

    public partial class Matcher {

        static IMatcher _matcherDraw;

        public static IMatcher Draw {
            get {
                if(_matcherDraw == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Draw);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDraw = matcher;
                }

                return _matcherDraw;
            }
        }
    }
}
