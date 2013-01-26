using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

// class for managing a single game
class BunkaGameInstance
{
    public BunkaGameInstance(ContentManager content)
    {
        this.InputManager = new InputManager();
        this.ResourceManager = new ResourceManager();
        this.BuildingManager = new BuildingManager();
        this.ConstructionManager = new ConstructionManager();
        this.CarrierManager = new CarrierManager();
        this.MapManager = new MapManager();
        this.ContentManager = content;
        this.DebugConsole = new DebugConsole();
    }

    public void Update(GameTime t)
    {
        this.InputManager.Update(t);
        this.ResourceManager.Update(t);
        this.BuildingManager.Update(t);
        this.ConstructionManager.Update(t);
        this.CarrierManager.Update(t);
        this.MapManager.Update(t);
        this.DebugConsole.Update(t);
    }

    public void Draw(SpriteBatch s)
    {
        this.MapManager.Draw(s);
        this.ResourceManager.Draw(s);
        this.ConstructionManager.Draw(s);
        this.CarrierManager.Draw(s);
    }

    public ResourceManager ResourceManager { get; private set; }

    public BuildingManager BuildingManager { get; private set; }

    public ConstructionManager ConstructionManager { get; private set; }

    public CarrierManager CarrierManager { get; private set; }

    public MapManager MapManager { get; private set; }

    public InputManager InputManager { get; private set; }

    public ContentManager ContentManager { get; private set; }

    public DebugConsole DebugConsole { get; private set; }
}

public class BunkaGame
{
    private static BunkaGameInstance instance;

    public static void Instantiate(ContentManager content)
    {
        instance = new BunkaGameInstance(content);
    }

    public static void Update(GameTime t)
    {
        instance.Update(t);
    }

    public static void Draw(SpriteBatch s)
    {
        instance.Draw(s);
    }

    public static ResourceManager ResourceManager
    {
        get { return instance.ResourceManager; }
    }

    public static BuildingManager BuildingManager
    {
        get { return instance.BuildingManager; }
    }

    public static ConstructionManager ConstructionManager
    {
        get { return instance.ConstructionManager; }
    }

    public static CarrierManager CarrierManager
    {
        get { return instance.CarrierManager; }
    }

    public static MapManager MapManager
    {
        get { return instance.MapManager; }
    }

    public static InputManager InputManager
    {
        get { return instance.InputManager; }
    }

    public static DebugConsole DebugConsole
    {
        get { return instance.DebugConsole; }
    }

    public static ContentManager ContentManager
    {
        get { return instance.ContentManager; }
    }
}