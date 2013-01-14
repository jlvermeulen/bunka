using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class Builder
{
    ConstructionBuilding target;
    LinkedList<CPoint> path;

    public Builder(Vector2 position)
    {
        this.Position = position;
    }

    public void Update(GameTime t)
    {
        if (target != null)
        {
            // TODO: add check to see if builder has arrived at target
            if (true)
            {
                // work on current target building
                this.CurrentConstruction.ConstructionTime -= (float)t.ElapsedGameTime.TotalSeconds;
                if (target.ConstructionTime <= 0)
                {
                    // request moving to idle list
                    BunkaGame.ConstructionManager.MoveToIdle(this);

                    // signal that construction is finished
                    BunkaGame.ConstructionManager.CompleteConstruction(target);

                    // reset target
                    target = null;
                    path = null;
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
        set
        {
            target = value;
            path = Pathfinder.GetPath(BunkaGame.MapManager.PositionToIndex(this.Position), target.Position);
        }
    }

    public Vector2 Position { get; set; }
}