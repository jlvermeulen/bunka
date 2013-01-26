using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Builder : Worker
{
    ConstructionBuilding target;

    public Builder(Vector2 position, float maxVelocity)
        : base(position, maxVelocity) { }

    public void Update(GameTime t)
    {
        ConstructionBuilding target = this.Destination as ConstructionBuilding;
        if (target != null)
        {
            // arrived at destination
            if (path.Count == 0)
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
                    this.Destination = null;
                    this.path = null;
                }
            }
            else
            {
                UpdateAimAndVelocity();
                this.Position += this.Aim * this.Velocity;
            }
        }
    }

    public void Draw(SpriteBatch s)
    {
        s.DrawString(BunkaGame.ContentManager.Load<SpriteFont>("Fonts/Buildings"), "#", new Vector2(this.Position.X / 100 * 30 + 60, this.Position.Y / 100 * 20 + 40), Color.Red);
    }
}