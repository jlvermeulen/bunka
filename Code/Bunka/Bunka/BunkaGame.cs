using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for managing a single game
class BunkaGame
{
    InputManager inputManager;
    ResourceManager resourceManager;
    BuildingManager buildingManager;
    ConstructionManager constructionManager;
    CarrierManager carrierManager;

    DebugConsole debugConsole;

    public BunkaGame()
    {
        inputManager = new InputManager();
        resourceManager = new ResourceManager(this);
        buildingManager = new BuildingManager(this);
        constructionManager = new ConstructionManager(this);
        carrierManager = new CarrierManager(this);
        debugConsole = new DebugConsole(this);
    }

    public void Update(GameTime t)
    {
        inputManager.Update(t);
        //resourceManager.Update(t);
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