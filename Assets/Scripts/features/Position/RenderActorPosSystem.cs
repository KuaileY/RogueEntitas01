using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RenderActorPosSystem:IReactiveSystem,ISetPool
{
    Pool _pool;
    public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.Move,Matcher.Actor).OnEntityAdded(); } }
    public void SetPool(Pool pool)
    {
        _pool = pool;
    }
    public void Execute(List<Entity> entities)
    {
        var grid = _pool.dungeonMap.Board;
        foreach (var entity in entities )
        {
            if (grid[entity.move.X, entity.move.Y].isWalkable)
            {
                var x = entity.pos.X;
                var y = entity.pos.Y;
                var newX = entity.move.X;
                var newY = entity.move.Y;
                grid[newX, newY]
                    .AddName(entity.name.value)
                    .AddAwareness(entity.awareness.value)
                    .ReplaceSymbol(entity.symbol.value)
                    .ReplaceBgColor(entity.bgColor.value)
                    .ReplaceColor(entity.color.value)
                    .ReplacePos(newX, newY)
                    .IsActor(true)
                    .IsControlable(true)
                    .IsWalkable(false)
                    .IsExplored(true);
                grid[x,y].RemoveAllComponents();
                grid[x,y]
                    .IsWalkable(true)
                    .IsTransparent(true)
                    .IsExplored(true)
                    .AddPos(x,y)
                    .ReplaceBgColor(Colors.FloorBackgroundFov)
                    .ReplaceColor(Colors.FloorFov)
                    .ReplaceSymbol('.');
                if (grid[newX, newY].isControlable)
                {
                    grid[newX, newY].IsUpdatePlayerFov(true);
                }

            }
        }
    }

}

