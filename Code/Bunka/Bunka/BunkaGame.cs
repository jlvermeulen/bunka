using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for managing a single game
class BunkaGame
{
    ResourceManager resourceManager;
    BuildingManager buildingManager;
    ConstructionManager constructionManager;
    CarrierManager carrierManager;

    public BunkaGame()
    {
        resourceManager = new ResourceManager();
        buildingManager = new BuildingManager(resourceManager);
        constructionManager = new ConstructionManager(resourceManager, buildingManager);
        carrierManager = new CarrierManager(resourceManager, buildingManager);
        resourceManager.CarrierManager = carrierManager;
    }

    public void Update(GameTime t)
    {
        resourceManager.Update(t);
        buildingManager.Update(t);
        constructionManager.Update(t);
        carrierManager.Update(t);
    }

    public void Draw(SpriteBatch s)
    {

    }
}