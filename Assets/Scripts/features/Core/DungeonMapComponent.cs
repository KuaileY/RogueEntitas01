using System.Collections.Generic;
using Entitas;
using Entitas.CodeGenerator;

[SingleEntity]
public sealed class DungeonMapComponent:IComponent
{
    public Entity[,] Board;
}

