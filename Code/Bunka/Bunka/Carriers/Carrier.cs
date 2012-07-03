using Microsoft.Xna.Framework;

// class for resource carriers
class Carrier
{
    CarrierManager carrierManager;
    ResourceType resourceType;
    Building destination;
    uint amount;

    public Carrier(CarrierManager carrierManager)
    {
        this.carrierManager = carrierManager;
        resourceType = ResourceType.None;
    }

    public void Update(GameTime t)
    {
        if (destination != null)
        {
            // TODO: add check to see if carrier has arrived at destination
            if (true)
            {
                // check if building is a conversion building
                if (destination.BuildingType > BuildingType.CONVERSION)
                {
                    // add amount to appropriate resource
                    BuildingConversion b = (BuildingConversion)destination;
                    foreach (Resource r in b.ResourceConverter.Input)
                        if (r.ResourceType == resourceType)
                        {
                            b.DeliverResource(resourceType);
                            r.Amount += amount;
                            amount = 0;
                            destination = null;
                            resourceType = ResourceType.None;
                        }
                }
                // request moving to idle carriers list
                carrierManager.MoveToIdle(this);
            }
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType Carrying
    {
        get { return resourceType; }
        set { resourceType = value; }
    }

    public Building Destination
    {
        get { return destination; }
        set { destination = value; }
    }

    public uint Amount
    {
        get { return amount; }
        set { amount = value; }
    }
}