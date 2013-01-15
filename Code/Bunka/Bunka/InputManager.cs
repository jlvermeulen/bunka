using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    public void Update(GameTime t)
    {
        this.PreviousKeyboardState = this.KeyboardState;
        this.PreviousMouseState = this.MouseState;

        this.KeyboardState = Keyboard.GetState();
        this.MouseState = Mouse.GetState();
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public bool AnyKeyPressed()
    {
        return this.KeyboardState.GetPressedKeys() != null;
    }

    public bool IsKeyDown(Keys key)
    {
        return this.KeyboardState.IsKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return this.KeyboardState.IsKeyDown(key) && !this.PreviousKeyboardState.IsKeyDown(key);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public KeyboardState KeyboardState { get; private set; }

    public KeyboardState PreviousKeyboardState { get; private set; }

    public MouseState MouseState { get; private set; }

    public MouseState PreviousMouseState { get; private set; }
}