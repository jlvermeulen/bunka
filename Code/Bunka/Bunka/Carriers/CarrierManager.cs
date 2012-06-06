using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for carrier administration
class CarrierManager
{
    List<Carrier> carriers, idleCarriers, busyCarriers;
    ResourceManager resourceManager;
    BuildingManager buildingManager;

    public CarrierManager(ResourceManager resourceManager, BuildingManager buildingManager)
    {
        carriers = new List<Carrier>();
        idleCarriers = new List<Carrier>();
        busyCarriers = new List<Carrier>();
        this.resourceManager = resourceManager;
        this.buildingManager = buildingManager;
    }

    public void Update(GameTime t)
    {
        foreach (Carrier c in carriers)
            c.Update(t);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public Carrier CreateCarrier()
    {
        Carrier temp = new Carrier();
        carriers.Add(temp);
        idleCarriers.Add(temp);
        return temp;
    }

    public bool RequestCarrier(ResourceType type, uint amount, Building destination)
    {
        if (idleCarriers.Count > 0)
        {
            // find free resource of type and send carrier
        }
        return false;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public int CarrierCount
    {
        get { return carriers.Count; }
    }
}