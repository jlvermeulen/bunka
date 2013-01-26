using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// class for managing the map of a game
public class MapManager
{
    Building[,] mapArray;
    char[,] drawArray;
    LinkedList<CPoint> path;

    private static readonly Dictionary<BuildingType, char> buildingChars = new Dictionary<BuildingType, char>
                                                                            {
                                                                                {BuildingType.CoalMine, 'c'},
                                                                                {BuildingType.CokingPlant, 'C'},
                                                                                {BuildingType.Construction, 'x'},
                                                                                {BuildingType.Fishery, 'F'},
                                                                                {BuildingType.IronMine, 'i'},
                                                                                {BuildingType.IronSmelter, 'I'},
                                                                                {BuildingType.Lumberjack, 'L'},
                                                                                {BuildingType.Quarry, 'Q'},
                                                                                {BuildingType.Sawmill, 'S'}
                                                                            };

    public MapManager()
    {
        this.Dimensions = new CPoint(20, 20);
        this.CellSize = 100;
        this.mapArray = new Building[this.Dimensions.X, this.Dimensions.Y];
        this.drawArray = new char[this.Dimensions.X, this.Dimensions.Y];
    }

    public void Update(GameTime t)
    {
        if (BunkaGame.InputManager.IsKeyPressed(Keys.Back))
            this.path = null;
        this.FillMap();
    }

    public void Draw(SpriteBatch s)
    {
        Vector2 root = new Vector2(75, 50);
        for (int i = 0; i < this.Dimensions.X; i++)
            s.DrawString(BunkaGame.ContentManager.Load<SpriteFont>("Fonts/Buildings"), i.ToString(), root + new Vector2(i * 30, -25), Color.White);
        for (int j = 0; j < this.Dimensions.Y; j++)
            s.DrawString(BunkaGame.ContentManager.Load<SpriteFont>("Fonts/Buildings"), j.ToString(), root + new Vector2(-35, j * 20), Color.White);
        for(int i = 0; i < this.Dimensions.X; i++)
            for(int j = 0;j < this.Dimensions.Y;j++)
                s.DrawString(BunkaGame.ContentManager.Load<SpriteFont>("Fonts/Buildings"), this.drawArray[i, j].ToString(), root + new Vector2(i * 30, j * 20), Color.White);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    // checks if index falls within the bounds of the map
    public bool IsValidIndex(int x, int y)
    {
        return x < this.mapArray.GetLength(0) && y < this.mapArray.GetLength(1) && x >= 0 && y >= 0;
    }

    public bool IsValidIndex(CPoint p)
    {
        return IsValidIndex(p.X, p.Y);
    }

    // check if position falls within the bounds of the map
    public bool IsPositionOnMap(Vector2 pos)
    {
        return pos.X < this.mapArray.GetLength(0) * this.CellSize && pos.Y < this.mapArray.GetLength(1) * this.CellSize && pos.X >= 0 && pos.Y >= 0;
    }

    // returns the position of the centre of the cell at the specified index
    public Vector2 IndexToCellCentre(int x, int y)
    {
        if (!IsValidIndex(x, y))
            return new Vector2(-1, -1);
        return new Vector2(x * this.CellSize + this.CellSize / 2, y * this.CellSize + this.CellSize / 2);
    }

    public Vector2 IndexToCellCentre(CPoint p)
    {
        return IndexToCellCentre(p.X, p.Y);
    }

    // returns the index of the cell that contains the specified position
    public CPoint PositionToIndex(Vector2 pos)
    {
        if (!IsPositionOnMap(pos))
            return new CPoint(-1, -1);
        return new CPoint((int)(pos.X / this.CellSize), (int)(pos.Y / this.CellSize));
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
            if (this[p] == null)
                result.Add(p);
        return result;
    }

    public void FillMap()
    {
        for(int i = 0; i < this.Dimensions.X; i++)
            for (int j = 0; j < this.Dimensions.Y; j++)
            {
                if (this[i, j] == null)
                    this.drawArray[i, j] = '-';
                else
                    this.drawArray[i, j] = buildingChars[this[i, j].BuildingType];
            }

        if (this.path != null)
            foreach (CPoint p in path)
                if (this[p] == null)
                    this.drawArray[p.X, p.Y] = '#';
    }

    public void FindPath(CPoint start, CPoint end)
    {
        this.path = Pathfinder.GetPath(start, end);
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

    public Building this[CPoint index]
    {
        get { return this[index.X, index.Y]; }
        set { this[index.X, index.Y] = value; }
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

    public CPoint Dimensions { get; private set; }

    public float CellSize { get; private set; }
}