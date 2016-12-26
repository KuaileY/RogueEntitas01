using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DrawSystem : IReactiveSystem
{
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Draw).OnEntityAdded(); } }

    public void Execute(List<Entity> entities)
    {
        var i = 0;
        foreach (var entity in entities )
        {
            i++;
            entity.IsDraw(false);
            if (!entity.isExplored)
                continue;
            var x = entity.pos.X;
            var y = entity.pos.Y;
            var symbol = entity.symbol.value.ToString();
            var bgColor = entity.bgColor.value;
            var color = entity.color.value;
            Display.CellAt(0, x, y).SetContent(symbol, bgColor, color);
        }
    }

}

