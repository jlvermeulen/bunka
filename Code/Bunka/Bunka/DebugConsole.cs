using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// class for allowing the game to be controlled through the console
public class DebugConsole
{
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
                        if (Enum.TryParse(parts[1], true, out type))
                            BunkaGame.ConstructionManager.ConstructBuilding(type, BunkaGame.MapManager.IndexToCellCentre(int.Parse(parts[2]), int.Parse(parts[3])));
                        else
                            Console.WriteLine("Cannot construct \'{0}\': unknown building type.", parts[1]);
                        break;
                    case "draw":
                        Tuple<int, int> dimensions = BunkaGame.MapManager.Dimensions;
                        for (int y = 0; y < dimensions.Item2; y++)
                        {
                            for (int x = 0; x < dimensions.Item1; x++)
                            {
                                if (BunkaGame.MapManager[x, y] == null)
                                    Console.Write('.');
                                else
                                    Console.Write('x');
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Unrecognised command '{0}'.", parts[0]);
                        break;
                }
            }
        }
    }
}