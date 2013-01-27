using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// class for resource carriers
public class Carrier : Worker
{
    CarryInfo carryInfo;

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
                // collect resource
                if (this.Carrying == ResourceType.None)
                {
                    this.CarryInfo.Origin.CollectResource(this.CarryInfo.ResourceType, this.CarryInfo.Amount);
                    this.Carrying = this.CarryInfo.ResourceType;
                    this.Amount = this.CarryInfo.Amount;
                    this.Destination = this.CarryInfo.Destination;
                }
                // deliver resource
                else
                {
                    this.Destination.DeliverResource(this.CarryInfo.ResourceType, this.CarryInfo.Amount);

                    // reset carrier members
                    this.Destination = null;
                    this.CarryInfo = null;
                    this.Carrying = ResourceType.None;
                    this.Amount = 0;

                    // request moving to idle carriers list
                    BunkaGame.CarrierManager.MoveToIdle(this);
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
        s.DrawString(BunkaGame.ContentManager.Load<SpriteFont>("Fonts/Buildings"), "$", new Vector2(this.Position.X / 100 * 30 + 60, this.Position.Y / 100 * 20 + 40), Color.Red);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public CarryInfo CarryInfo
    {
        get { return this.carryInfo; }
        set
        {
            this.carryInfo = value;
            if (value != null)
            {
                this.Destination = value.Origin;
                this.path = Pathfinder.GetPath(BunkaGame.MapManager.PositionToIndex(this.Position), value.Origin.Position);
            }
        }
    }

    public ResourceType Carrying { get; set; }

    public uint Amount { get; set; }
}