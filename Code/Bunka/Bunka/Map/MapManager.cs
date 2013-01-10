using System;
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

    public bool IsValidIndex(int x, int y)
    {
        return x < mapArray.GetLength(0) && y < mapArray.GetLength(1) && x >= 0 && y >= 0;
    }

    public bool IsPositionOnMap(Vector2 pos)
    {
        return pos.X < mapArray.GetLength(0) * cellSize && pos.Y < mapArray.GetLength(1) * cellSize && pos.X >= 0 && pos.Y >= 0;
    }

    public Vector2 IndexToCellCentre(int x, int y)
    {
        return new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2);
    }

    public Tuple<int, int> PositionToIndex(Vector2 pos)
    {
        return new Tuple<int, int>((int)(pos.X / cellSize), (int)(pos.Y / cellSize));
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Building this[Vector2 pos]
    {
        get
        {
            Tuple<int, int> index = PositionToIndex(pos);
            return this[index.Item1, index.Item2];
        }
        set
        {
            Tuple<int, int> index = PositionToIndex(pos);
            this[index.Item1, index.Item2] = value;
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

    public Tuple<int, int> Dimensions
    {
        get { return new Tuple<int, int>(mapArray.GetLength(0), mapArray.GetLength(1)); }
    }
}