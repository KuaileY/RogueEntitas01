
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entitas;
using UnityEngine;

public static class FieldOfViewExtension
{
    private static readonly HashSet<int> _inFov = new HashSet<int>();
    public static ReadOnlyCollection<Vector2> ComputeFov(this Entity[,] grid,int xOrigin, int yOrigin, int radius, bool lightWalls)
    {
        grid.ClearFov();
        return grid.AppendFov(xOrigin, yOrigin, radius, lightWalls);
    }

    private static ReadOnlyCollection<Vector2> AppendFov(this Entity[,] grid, int xOrigin, int yOrigin, int radius, bool lightWalls)
    {
        foreach (var borderCell in DungeonMapExtension.GetBorderCellsInArea(xOrigin, yOrigin, radius))
        {
            foreach (var cell in DungeonMapExtension.GetCellsAlongLine(xOrigin,yOrigin, (int)borderCell.x,(int)borderCell.y))
            {
                var x = (int)cell.x;
                var y = (int)cell.y;
                if ((Math.Abs(x - xOrigin) + Math.Abs(y - yOrigin)) > radius)
                {
                    break;
                }

                if (grid[x, y].isTransparent)
                {
                    _inFov.Add(DungeonMapExtension.IndexFor(x, y));
                }
                else
                {
                    if (lightWalls)
                    {
                        _inFov.Add(DungeonMapExtension.IndexFor(x, y));
                    }
                    break;
                }
            }
        }
        if (lightWalls)
        {
            // Post processing step created based on the algorithm at this website:
            // https://sites.google.com/site/jicenospam/visibilitydetermination
            foreach (var cell in DungeonMapExtension.GetBorderCellsInArea(xOrigin, yOrigin, radius))
            {
                var x = (int) cell.x;
                var y = (int) cell.y;
                if (cell.x > xOrigin)
                {
                    if (cell.y > yOrigin)
                    {
                        grid.PostProcessFovQuadrant(x, y, Quadrant.SE);
                    }
                    else if (cell.y < yOrigin)
                    {
                        grid.PostProcessFovQuadrant(x, y, Quadrant.NE);
                    }
                }
                else if (cell.x < xOrigin)
                {
                    if (cell.y > yOrigin)
                    {
                        grid.PostProcessFovQuadrant(x, y, Quadrant.SW);
                    }
                    else if (cell.y < yOrigin)
                    {
                        grid.PostProcessFovQuadrant(x, y, Quadrant.NW);
                    }
                }
            }
        }

        return CellsInFov();
    }

    private static void PostProcessFovQuadrant(this Entity[,] grid, int x, int y, Quadrant quadrant)
    {
        int x1 = x;
        int y1 = y;
        int x2 = x;
        int y2 = y;
        switch (quadrant)
        {
            case Quadrant.NE:
                {
                    y1 = y + 1;
                    x2 = x - 1;
                    break;
                }
            case Quadrant.SE:
                {
                    y1 = y - 1;
                    x2 = x - 1;
                    break;
                }
            case Quadrant.SW:
                {
                    y1 = y - 1;
                    x2 = x + 1;
                    break;
                }
            case Quadrant.NW:
                {
                    y1 = y + 1;
                    x2 = x + 1;
                    break;
                }
        }
        if (!IsInFov(x, y) && !grid[x,y].isTransparent)
        {
            if((grid[x1,y1].isTransparent)&&IsInFov(x1,y1) || (grid[x2,y2].isTransparent&&IsInFov(x2,y2))
                    || (grid[x2, y1].isTransparent && IsInFov(x2, y1)))
            {
                _inFov.Add(DungeonMapExtension.IndexFor(x, y));
            }
        }
    }

    private static ReadOnlyCollection<Vector2> CellsInFov()
    {
        var cells = new List<Vector2>();
        foreach (var index in _inFov)
        {
            cells.Add(DungeonMapExtension.CellFor(index));
        }

        return new ReadOnlyCollection<Vector2>(cells);
    }

    private static void ClearFov(this Entity[,] grid)
    {
        _inFov.Clear();
    }

    public static bool IsInFov(int x, int y)
    {
        return _inFov.Contains(DungeonMapExtension.IndexFor(x, y));
    }
}

