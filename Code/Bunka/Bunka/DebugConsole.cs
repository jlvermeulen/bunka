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
            string line = Console.ReadLine();
            if (line != null)
            {
                string[] parts = line.Split();

                switch (parts[0])
                {
                    case "build":
                        BuildingType type;
                        if (Enum.TryParse(parts[1], true, out type))
                            BunkaGame.ConstructionManager.ConstructBuilding(type);
                        else
                            Console.WriteLine("Cannot construct \'{0}\': unknown building type.", parts[1]);
                        break;
                    default:
                        Console.WriteLine("Unrecognised command '{0}'.", parts[0]);
                        break;
                }
            }
        }
    }
}