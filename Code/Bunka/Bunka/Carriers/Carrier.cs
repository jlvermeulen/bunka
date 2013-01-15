using Microsoft.Xna.Framework;

// class for resource carriers
public class Carrier
{
    public Carrier(Vector2 position)
    {
        this.Carrying = ResourceType.None;
    }

    public void Update(GameTime t)
    {
        if (this.Destination != null)
        {
            // TODO: add check to see if carrier has arrived at destination
            if (true)
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
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType Carrying { get; set; }

    public Building Destination { get; set; }

    public uint Amount { get; set; }

    public Vector2 Position { get; set; }
}