using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for managing a single game
class BunkaGame
{
    ResourceManager resourceManager;
    BuildingManager buildingManager;

    public BunkaGame()
    {
        resourceManager = new ResourceManager();
        buildingManager = new BuildingManager(resourceManager);
    }

    public void Update(GameTime t)
    {
        resourceManager.Update(t);
    }

    public void Draw(SpriteBatch s)
    {

    }
}