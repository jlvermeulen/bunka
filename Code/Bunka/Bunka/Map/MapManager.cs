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
        mapArray = new Building[10, 10];
        cellSize = 10;
    }

    //////////////////
    //   METHODS    //
    //////////////////

    // checks if index falls within the bounds of the map
    public bool IsValidIndex(int x, int y)
    {
        return x < mapArray.GetLength(0) && y < mapArray.GetLength(1) && x >= 0 && y >= 0;
    }

    public bool IsValidIndex(Point p)
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
    public Point PositionToIndex(Vector2 pos)
    {
        if (!IsPositionOnMap(pos))
            return new Point(-1, -1);
        return new Point((int)(pos.X / cellSize), (int)(pos.Y / cellSize));
    }

    public int ManhattanDistance(Point from, Point to)
    {
        return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
    }

    public int ManhattanDistance(Vector2 from, Vector2 to)
    {
        Point fromIndex, toIndex;
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

    public List<Point> Neighbours(Point cell)
    {
        List<Point> result = new List<Point>();
        Point[] possibles = { new Point(cell.X - 1, cell.Y), new Point(cell.X + 1, cell.Y), new Point(cell.X, cell.Y - 1), new Point(cell.X, cell.Y + 1) };
        foreach (Point p in possibles)
            if (IsValidIndex(p))
                result.Add(p);
        return result;
    }

    public List<Point> FreeNeighbours(Point cell)
    {
        List<Point> possibles = Neighbours(cell);
        List<Point> result = new List<Point>();
        foreach (Point p in possibles)
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
            Point index = PositionToIndex(pos);
            return this[index.X, index.Y];
        }
        set
        {
            Point index = PositionToIndex(pos);
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

    public Point Dimensions
    {
        get { return new Point(mapArray.GetLength(0), mapArray.GetLength(1)); }
    }
}