using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// main game class
public class Bunka : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public Bunka()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        BunkaGame.ResourceManager.AddInitialResources();
    }

    protected override void Initialize()
    {
        base.Initialize();
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void LoadContent()
    {
        
    }

    protected override void Update(GameTime t)
    {
        BunkaGame.Update(t);
        base.Update(t);
    }

    protected override void Draw(GameTime t)
    {
        GraphicsDevice.Clear(Color.Black);
        BunkaGame.Draw(spriteBatch);
        base.Draw(t);
    }
}
