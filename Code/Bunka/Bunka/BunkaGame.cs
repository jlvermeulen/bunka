using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for managing a single game
class BunkaGameInstance
{
    InputManager inputManager;
    ResourceManager resourceManager;
    BuildingManager buildingManager;
    ConstructionManager constructionManager;
    CarrierManager carrierManager;

    DebugConsole debugConsole;

    public BunkaGameInstance()
    {
        inputManager = new InputManager();
        resourceManager = new ResourceManager();
        buildingManager = new BuildingManager();
        constructionManager = new ConstructionManager();
        carrierManager = new CarrierManager();
        debugConsole = new DebugConsole();
    }

    public void Update(GameTime t)
    {
        inputManager.Update(t);
        resourceManager.Update(t);
        buildingManager.Update(t);
        constructionManager.Update(t);
        carrierManager.Update(t);
        debugConsole.Update(t);
    }

    public void Draw(SpriteBatch s)
    {

    }

    public ResourceManager ResourceManager
    {
        get { return resourceManager; }
    }

    public BuildingManager BuildingManager
    {
        get { return buildingManager; }
    }

    public ConstructionManager ConstructionManager
    {
        get { return constructionManager; }
    }

    public CarrierManager CarrierManager
    {
        get { return carrierManager; }
    }

    public InputManager InputManager
    {
        get { return inputManager; }
    }
}

public class BunkaGame
{
    private static readonly BunkaGameInstance instance = new BunkaGameInstance();

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

    public static InputManager InputManager
    {
        get { return instance.InputManager; }
    }
}