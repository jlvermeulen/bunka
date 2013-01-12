using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for managing the map of a game
public class MapManager
{
    Building[,] mapArray;
    float cellSize;

    public MapManager()
    {
        mapArray = new Building[200, 200];
        cellSize = 10;
        Console.SetBufferSize(201, 201);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    // checks if index falls within the bounds of the map
    public bool IsValidIndex(int x, int y)
    {
        return x < mapArray.GetLength(0) && y < mapArray.GetLength(1) && x >= 0 && y >= 0;
    }

    public bool IsValidIndex(CPoint p)
    {
        return IsValidIndex(p.X, p.Y);
    }

    // check if position falls within the bounds of the map
    public bool IsPositionOnMap(Vector2 pos)
    {
        return pos.X < mapArray.GetLength(0) * cellSize && pos.Y < mapArray.GetLength(1) * cellSize && pos.X >= 0 && pos.Y >= 0;
    }

    // returns the position of the centre of the cell at the specified index
    public Vector2 IndexToCellCentre(int x, int y)
    {
        if (!IsValidIndex(x, y))
            return new Vector2(-1, -1);
        return new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
    }

    // returns the index of the cell that contains the specified position
    public CPoint PositionToIndex(Vector2 pos)
    {
        if (!IsPositionOnMap(pos))
            return new CPoint(-1, -1);
        return new CPoint((int)(pos.X / cellSize), (int)(pos.Y / cellSize));
    }

    public int ManhattanDistance(CPoint from, CPoint to)
    {
        return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
    }

    public int ManhattanDistance(Vector2 from, Vector2 to)
    {
        CPoint fromIndex, toIndex;
        fromIndex = PositionToIndex(from);
        toIndex = PositionToIndex(to);
        return Math.Abs(fromIndex.X - toIndex.X) + Math.Abs(fromIndex.Y - toIndex.Y);
    }

    public float EuclidianDistance(Vector2 from, Vector2 to)
    {
        return Vector2.Distance(from, to);
    }

    public int PathDistance(Vector2 from, Vector2 to)
    {
        return ManhattanDistance(from, to);
    }

    public List<CPoint> Neighbours(CPoint cell)
    {
        List<CPoint> result = new List<CPoint>();
        CPoint[] possibles = { new CPoint(cell.X - 1, cell.Y), new CPoint(cell.X + 1, cell.Y), new CPoint(cell.X, cell.Y - 1), new CPoint(cell.X, cell.Y + 1) };
        foreach (CPoint p in possibles)
            if (IsValidIndex(p))
                result.Add(p);
        return result;
    }

    public List<CPoint> FreeNeighbours(CPoint cell)
    {
        List<CPoint> possibles = Neighbours(cell);
        List<CPoint> result = new List<CPoint>();
        foreach (CPoint p in possibles)
            if (mapArray[p.X, p.Y] == null)
                result.Add(p);
        return result;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Building this[Vector2 pos]
    {
        get
        {
            CPoint index = PositionToIndex(pos);
            return this[index.X, index.Y];
        }
        set
        {
            CPoint index = PositionToIndex(pos);
            this[index.X, index.Y] = value;
        }
    }

    public Building this[int x, int y]
    {
        get
        {
            if (!IsValidIndex(x, y))
                return null;
            return mapArray[x, y];
        }
        set
        {
            if (IsValidIndex(x, y))
                mapArray[x, y] = value;
        }
    }

    public CPoint Dimensions
    {
        get { return new CPoint(mapArray.GetLength(0), mapArray.GetLength(1)); }
    }
}