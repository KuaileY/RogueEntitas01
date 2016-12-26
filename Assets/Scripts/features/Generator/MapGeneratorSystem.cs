using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class MapGeneratorSystem:ISetPool,IReactiveSystem
{
    Pool _pool;
    public TriggerOnEvent trigger { get { return Matcher.MapGenerator.OnEntityAdded(); } }
    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute(List<Entity> entities)
    {
        foreach (var entity in entities )
        {
            _pool.DestroyEntity(entity);
        }

        CreateMap();
    }

    void CreateMap()
    {
        var w = Res.Width;
        var h = Res.Height;
        var board=_pool.SetDungeonMap(new Entity[w,h]);
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Entity e;
                if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                    e = _pool.CreateEntity()
                        .AddSymbol('#')
                        .AddBgColor(Colors.WallBackground)
                        .AddColor(Colors.Wall)
                        ;
                else
                    e = _pool.CreateEntity()
                        .AddSymbol('.')
                        .AddBgColor(Colors.FloorBackground)
                        .AddColor(Colors.Floor)
                        .IsTransparent(true)
                        .IsWalkable(true)
                        ;
                e.AddPos(x, y).IsExplored(true).IsDraw(true);
                board.dungeonMap.Board[x, y] = e;
            }
        }

        PlacePlayer(10,10);

        
    }

    void PlacePlayer(int x,int y)
    {
        _pool.dungeonMap.Board[x, y]
            .AddName("player")
            .AddAwareness(15)
            .ReplaceSymbol('@')
            .ReplaceBgColor(Color.clear)
            .ReplaceColor(Colors.Player)
            .IsActor(true)
            .IsControlable(true)
            .IsUpdatePlayerFov(true)
            .IsWalkable(false)
            .IsExplored(true);
    }

}

