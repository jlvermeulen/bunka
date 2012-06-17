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
            if (true)
            {
                // check if building is a conversion building
                if (destination.BuildingType > BuildingType.Conversion)
                {
                    // add amount to appropriate resource
                    Building_Conversion b = (Building_Conversion)destination;
                    foreach (ResourceConverter c in b.ResourceConverters)
                        foreach (Resource r in c.Input)
                            if (r.ResourceType == resourceType)
                            {
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