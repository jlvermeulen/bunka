using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// class for allowing the game to be controlled through the console
public class DebugConsole
{
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

    public void Update(GameTime t)
    {
        if (BunkaGame.InputManager.IsKeyPressed(Keys.OemTilde))
        {
            string line;
            while ((line = Console.ReadLine()) != "")
            {
                string[] parts = line.Split();

                switch (parts[0])
                {
                    case "build":
                        BuildingType type;
                        if (parts.Length != 4)
                            Console.WriteLine("Invalid number of arguments.");
                        else if (Enum.TryParse(parts[1], true, out type))
                            BunkaGame.ConstructionManager.ConstructBuilding(type, BunkaGame.MapManager.IndexToCellCentre(int.Parse(parts[2]), int.Parse(parts[3])));
                        else
                            Console.WriteLine("Cannot construct \'{0}\': unknown building type.", parts[1]);
                        break;
                    case "draw":
                        CPoint dimensions = BunkaGame.MapManager.Dimensions;
                        for (int y = 0; y < dimensions.Y; y++)
                        {
                            for (int x = 0; x < dimensions.X; x++)
                            {
                                Building b = BunkaGame.MapManager[x, y];
                                if (b == null)
                                    Console.Write('.');
                                else
                                    Console.Write(buildingChars[b.BuildingType]);
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "path":
                        DateTime start = DateTime.Now;
                        List<CPoint> path = Pathfinder.GetPath(new CPoint(int.Parse(parts[1]), int.Parse(parts[2])), new CPoint(int.Parse(parts[3]), int.Parse(parts[4])));
                        DateTime end = DateTime.Now;
                        dimensions = BunkaGame.MapManager.Dimensions;
                        for (int y = 0; y < dimensions.Y; y++)
                        {
                            for (int x = 0; x < dimensions.X; x++)
                            {
                                Building b = BunkaGame.MapManager[x, y];
                                if (b != null)
                                    Console.Write(buildingChars[b.BuildingType]);
                                else if (path.Contains(new CPoint(x, y)))
                                    Console.Write('+');
                                else
                                    Console.Write('.');
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine("Found path in {0} ms.", (end - start).TotalMilliseconds);
                        break;
                    default:
                        Console.WriteLine("Unrecognised command '{0}'.", parts[0]);
                        break;
                }
            }
        }
    }
}