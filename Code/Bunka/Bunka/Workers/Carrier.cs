using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for resource carriers
public class Carrier : Worker
{
    public Carrier(Vector2 position, float maxVelocity)
        : base(position, maxVelocity)
    {
        this.Carrying = ResourceType.None;
    }

    public void Update(GameTime t)
    {
        if (this.Destination != null)
        {
            // arrived at destination
            if (path.Count == 0)
            {
                // check if building is a conversion building
                if (this.Destination.BuildingType > BuildingType.CONVERSION)
                    ((BuildingConversion)this.Destination).DeliverResource(this.Carrying, this.Amount);
                // otherwise it is a construction building
                else
                    ((ConstructionBuilding)this.Destination).DeliverResource(this.Carrying, this.Amount);

                // reset carrier members
                this.Amount = 0;
                this.Destination = null;
                this.Carrying = ResourceType.None;

                // request moving to idle carriers list
                BunkaGame.CarrierManager.MoveToIdle(this);
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
        s.DrawString(BunkaGame.ContentManager.Load<SpriteFont>("Fonts/Buildings"), "$", new Vector2(this.Position.X / 100 * 30 + 60, this.Position.Y / 100 * 20 + 40), Color.Red);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType Carrying { get; set; }

    public uint Amount { get; set; }
}