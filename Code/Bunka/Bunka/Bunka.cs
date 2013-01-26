using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// main game class
public class Bunka : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public Bunka()
    {
        this.graphics = new GraphicsDeviceManager(this);
        this.Content.RootDirectory = "Content";
        this.graphics.PreferredBackBufferWidth = 1280;
        this.graphics.PreferredBackBufferHeight = 720;
        BunkaGame.Instantiate(this.Content);
        BunkaGame.ResourceManager.AddInitialResources();
    }

    protected override void Initialize()
    {
        base.Initialize();
        this.spriteBatch = new SpriteBatch(GraphicsDevice);
        this.IsMouseVisible = true;
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
        spriteBatch.Begin();
        BunkaGame.Draw(spriteBatch);
        spriteBatch.End();
        base.Draw(t);
    }
}
