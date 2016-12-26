
using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public static class DungeonMapExtension
{
    //     public static bool IsInFov(this InFovComponent inFov, int x, int y)
    //     {
    //         return inFov.ids.Contains(y*Res.Width + x);
    //     }

    public static IEnumerable<Vector2> GetBorderCellsInArea(int xOrigin, int yOrigin, int distance)
    {
        int xMin = Math.Max(0, xOrigin - distance);
        int xMax = Math.Min(Res.Width - 1, xOrigin + distance);
        int yMin = Math.Max(0, yOrigin - distance);
        int yMax = Math.Min(Res.Height - 1, yOrigin + distance);

        for (int x = xMin; x <= xMax; x++)
        {
            yield return new Vector2(x, yMin);
            yield return new Vector2(x, yMax);
        }
        for (int y = yMin; y <= yMax; y++)
        {
            yield return new Vector2(xMin, y);
            yield return new Vector2(xMax, y);
        }
    }

    public static IEnumerable<Vector2> GetCellsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
    {
        int dx = Math.Abs(xDestination - xOrigin);
        int dy = Math.Abs(yDestination - yOrigin);

        int sx = xOrigin < xDestination ? 1 : -1;
        int sy = yOrigin < yDestination ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            yield return new Vector2(xOrigin, yOrigin);
            if (xOrigin == xDestination && yOrigin == yDestination)
            {
                break;
            }
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err = err - dy;
                xOrigin = xOrigin + sx;
            }
            if (e2 < dx)
            {
                err = err + dx;
                yOrigin = yOrigin + sy;
            }
        }
    }


    public static int IndexFor(int x, int y)
    {
        return (y * Res.Width) + x;
    }

    public static Vector2 CellFor(int index)
    {
        int x = index % Res.Width;
        int y = index / Res.Width;

        return new Vector2(x, y);
    }

    public static IEnumerable<Vector2> GetAllCells()
    {
        for (int y = 0; y < Res.Height; y++)
        {
            for (int x = 0; x < Res.Width; x++)
            {
                yield return new Vector2(x, y);
            }
        }
    }

}

