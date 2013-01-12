using Microsoft.Xna.Framework;

public class Builder
{
    ConstructionBuilding target;
    Vector2 position;

    public Builder(Vector2 position)
    {
        this.position = position;
    }

    public void Update(GameTime t)
    {
        if (target != null)
        {
            // TODO: add check to see if builder has arrived at target
            if (true)
            {
                // work on current target building
                target.ConstructionTime -= (float)t.ElapsedGameTime.TotalSeconds;
                if (target.ConstructionTime <= 0)
                {
                    // request moving to idle list
                    BunkaGame.ConstructionManager.MoveToIdle(this);

                    // signal that construction is finished
                    BunkaGame.ConstructionManager.CompleteConstruction(target);

                    // reset target
                    target = null;
                }
            }
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ConstructionBuilding CurrentConstruction
    {
        get { return target; }
        set { target = value; }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
}