using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Bunka : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    BunkaGame currentGame;

    public Bunka()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        currentGame = new BunkaGame();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime t)
    {
        currentGame.Update(t);
        base.Update(t);
    }

    protected override void Draw(GameTime t)
    {
        GraphicsDevice.Clear(Color.Black);
        currentGame.Draw(spriteBatch);
        base.Draw(t);
    }
}
