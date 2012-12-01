using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class InputManager
{
    KeyboardState keyState, prevKeyState;
    MouseState mouseState, prevMouseState;

    public void Update(GameTime t)
    {
        prevKeyState = keyState;
        prevMouseState = mouseState;

        keyState = Keyboard.GetState();
        mouseState = Mouse.GetState();
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public bool AnyKeyPressed()
    {
        return keyState.GetPressedKeys() != null;
    }

    public bool IsKeyDown(Keys key)
    {
        return keyState.IsKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return keyState.IsKeyDown(key) && !prevKeyState.IsKeyDown(key);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public KeyboardState KeyboardState
    {
        get { return keyState; }
    }

    public KeyboardState PreviousKeyboardState
    {
        get { return prevKeyState; }
    }

    public MouseState MouseState
    {
        get { return mouseState; }
    }

    public MouseState PreviousMouseState
    {
        get { return prevMouseState; }
    }
}