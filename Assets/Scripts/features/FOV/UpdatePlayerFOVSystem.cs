using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entitas;
using UnityEngine;

public sealed class UpdatePlayerFOVSystem:ISetPool,IReactiveSystem
{
    Pool _pool;
    public TriggerOnEvent trigger { get
    {
        return Matcher.AllOf(Matcher.UpdatePlayerFov).OnEntityAdded();
    } }

    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute(List<Entity> entities)
    {
        var grid = _pool.dungeonMap.Board;
        foreach (var entity in entities )
        {
            entity.IsUpdatePlayerFov(false);
            FieldOfViewExtension.ComputeFov(grid,entity.pos.X, entity.pos.Y, entity.awareness.value, true);
            foreach (var cell in DungeonMapExtension.GetAllCells())
            {
                var x = (int)cell.x;
                var y = (int)cell.y;
                if (FieldOfViewExtension.IsInFov(x, y))
                {
                    if (grid[x, y].isWalkable)
                    {
                        grid[x, y].ReplaceBgColor(Colors.FloorBackgroundFov)
                            .ReplaceColor(Colors.FloorFov);
                    }
                    else
                    {
                        grid[x, y].ReplaceBgColor(Colors.WallBackgroundFov)
                            .ReplaceColor(Colors.WallFov);
                    }
                }
                else
                {
                    if (grid[x, y].isWalkable)
                    {
                        grid[x, y].ReplaceBgColor(Colors.FloorBackground)
                            .ReplaceColor(Colors.Floor);
                    }
                    else
                    {
                        grid[x, y].ReplaceBgColor(Colors.WallBackground)
                            .ReplaceColor(Colors.Wall);
                    }
                }
                grid[x, y].IsDraw(true);
            }
        }
    }


}

