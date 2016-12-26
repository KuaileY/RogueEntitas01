using System.Collections;
using Entitas;
using UnityEngine;

public class Game : MonoBehaviour
{
    Systems _systems;
    bool _gameStarting;
    // Use this for initialization
    IEnumerator Start () {

        while (!Display.IsInitialized())
        {
            yield return null;
        }

        

        var pools = Pools.sharedInstance;
	    pools.SetAllPools();

	    _systems = CreateSystems(pools);
	    _systems.Initialize();
        _gameStarting = true;
	}

    void Update()
    {
        if (_gameStarting)
        {
            _systems.Execute();
            _systems.Cleanup();
        }
    }

    void OnDestroy()
    {
        _systems.TearDown();
    }

    Systems CreateSystems(Pools pools)
    {
        return new Feature("Systems")
            //input
            .Add(pools.pool.CreateSystem(new InputSystem()))
            //gameLogic
            .Add(pools.pool.CreateSystem(new GameStartSystem()))
            //generator
            .Add(pools.pool.CreateSystem(new MapGeneratorSystem()))
            //behaviour
            .Add(pools.pool.CreateSystem(new RenderActorPosSystem()))
            //view
            .Add(pools.pool.CreateSystem(new UpdatePlayerFOVSystem()))
            //
            .Add(pools.pool.CreateSystem(new DrawSystem()))
            ;
    }
}
