using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// class for allowing the game to be controlled through the console
class DebugConsole
{
    BunkaGame game;

    public DebugConsole(BunkaGame game)
    {
        this.game = game;
    }

    public void Update(GameTime t)
    {
        if (game.InputManager.IsKeyPressed(Keys.OemTilde))
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
                            game.ConstructionManager.ConstructBuilding(type);
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