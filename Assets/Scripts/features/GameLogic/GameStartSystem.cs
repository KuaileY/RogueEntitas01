using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class GameStartSystem:IInitializeSystem,ISetPool
{
    Pool _pool;
    public void SetPool(Pool pool)
    {
        _pool = pool;
    }
    public void Initialize()
    {
        /*
        _pool.CreateEntity().IsGameStarting(true);
        _pool.CreateEntity().AddInFov(new HashSet<int>());
        */
        _pool.CreateEntity().IsMapGenerator(true);
    }
}


