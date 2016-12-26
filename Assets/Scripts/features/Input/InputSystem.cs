using Entitas;
using UnityEngine;

public sealed class InputSystem:ISetPool,IExecuteSystem,ICleanupSystem
{
    Pool _pool;
    Group _group;

    private float _lastKeyPressTime;
    private float KeyPressDelay = 0.1f;
    public void SetPool(Pool pool)
    {
        _pool = pool;
    }

    public void Execute()
    {
        bool didPlayerAct = false;
        var x = 0;
        var y = 0;
        var player = _pool.controlableEntity;
        if (Input.anyKeyDown || Time.time - _lastKeyPressTime > KeyPressDelay)
        {
            _lastKeyPressTime = Time.time;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal > 0)
            {
                x = player.pos.X + 1;
                y = player.pos.Y;
                didPlayerAct = _pool.dungeonMap.Board[x, y].isWalkable;
            }
            else if (horizontal < 0)
            {
                x = player.pos.X - 1;
                y = player.pos.Y;
                didPlayerAct = _pool.dungeonMap.Board[x, y].isWalkable;
            }
            else if (vertical > 0)
            {
                x = player.pos.X;
                y = player.pos.Y - 1;
                didPlayerAct = _pool.dungeonMap.Board[x, y].isWalkable;
            }
            else if (vertical < 0)
            {
                x = player.pos.X;
                y = player.pos.Y + 1;
                didPlayerAct = _pool.dungeonMap.Board[x, y].isWalkable;
            }
        }

        if (didPlayerAct)
        {
            player.AddMove(x, y);
        }
    }

    public void Cleanup()
    {
        //
    }
}

