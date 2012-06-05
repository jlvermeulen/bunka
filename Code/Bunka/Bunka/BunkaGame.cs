using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for managing a single game
class BunkaGame
{
    ResourceManager resourceManager;

    public BunkaGame()
    {
        resourceManager = new ResourceManager();
    }

    public void Update(GameTime t)
    {
        resourceManager.Update(t);
    }

    public void Draw(SpriteBatch s)
    {

    }
}