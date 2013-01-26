using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// class for allowing the game to be controlled through the console
public class DebugConsole
{
    [DllImport("Kernel32")]
    public static extern void AllocConsole();

    [DllImport("Kernel32")]
    public static extern void FreeConsole();

    public void Update(GameTime t)
    {
        if (BunkaGame.InputManager.IsKeyPressed(Keys.OemTilde))
        {
            AllocConsole();
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
                        {
                            if (parts[3] != "_")
                                BunkaGame.ConstructionManager.ConstructBuilding(type, new CPoint(int.Parse(parts[2]), int.Parse(parts[3])));
                            else
                                for(int i = 0; i < 20; i++)
                                    BunkaGame.ConstructionManager.ConstructBuilding(type, new CPoint(int.Parse(parts[2]), i));
                        }
                        else
                            Console.WriteLine("Cannot construct \'{0}\': unknown building type.", parts[1]);
                        break;
                    case "create":
                        if (parts.Length != 2)
                            Console.WriteLine("Invalid number of arguments.");
                        else if (parts[1] == "builder")
                            BunkaGame.ConstructionManager.CreateBuilder();
                        else if (parts[1] == "carrier")
                            BunkaGame.CarrierManager.CreateCarrier();
                        else
                            Console.WriteLine("Cannot create \'{0}\': unknown worker type.", parts[1]);
                        break;
                    case "path":
                        BunkaGame.MapManager.FindPath(new CPoint(int.Parse(parts[1]), int.Parse(parts[2])), new CPoint(int.Parse(parts[3]), int.Parse(parts[4])));
                        break;
                    default:
                        Console.WriteLine("Unrecognised command '{0}'.", parts[0]);
                        break;
                }
            }
            FreeConsole();
        }
    }
}