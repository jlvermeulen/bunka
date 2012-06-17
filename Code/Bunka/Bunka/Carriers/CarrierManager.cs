using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for carrier administration
class CarrierManager
{
    ResourceManager resourceManager;
    BuildingManager buildingManager;
    List<Carrier> carriers, idleCarriers, busyCarriers;
    LinkedList<Carrier> moveToIdle;
    LinkedList<CarryRequest> requests;

    public CarrierManager(ResourceManager resourceManager, BuildingManager buildingManager)
    {
        this.resourceManager = resourceManager;
        this.buildingManager = buildingManager;
        this.carriers = new List<Carrier>();
        this.idleCarriers = new List<Carrier>();
        this.busyCarriers = new List<Carrier>();
        this.moveToIdle = new LinkedList<Carrier>();
        this.requests = new LinkedList<CarryRequest>();
        Test();
    }

    void Test()
    {
        Carrier c = new Carrier(this);
        idleCarriers.Add(c);
        carriers.Add(c);
    }

    public void Update(GameTime t)
    {
        // update busy carriers
        foreach (Carrier c in busyCarriers)
            c.Update(t);

        // move finished carriers to idle list
        if (moveToIdle.Count > 0)
        {
            LinkedListNode<Carrier> node = moveToIdle.First;
            while (node != null)
            {
                busyCarriers.Remove(node.Value);
                idleCarriers.Add(node.Value);
                moveToIdle.Remove(node);
                node = moveToIdle.First;
            }
        }

        // handle carrier requests
        if (requests.Count > 0)
        {
            // first request
            LinkedListNode<CarryRequest> node = requests.First;

            // check if carrier is available and if there is still a request
            while (idleCarriers.Count != 0 && node != null)
            {
                // retrieve list of free resources of the desired type
                LinkedList<Resource> list;
                if (resourceManager.FreeResources.TryGetValue(node.Value.ResourceType, out list))
                {
                    // skip if list is empty
                    if (list.Count == 0)
                        continue;

                    foreach (Resource r in list)
                    {
                        // check if amount meets request
                        if (r.Amount >= node.Value.Amount)
                        {
                            // give resource to carrier
                            idleCarriers[0].Carrying = node.Value.ResourceType;
                            idleCarriers[0].Destination = node.Value.Destination;
                            idleCarriers[0].Amount = node.Value.Amount;

                            // remove amount from resource
                            r.Amount -= node.Value.Amount;

                            // move carrier to busy carriers list
                            busyCarriers.Add(idleCarriers[0]);
                            idleCarriers.RemoveAt(0);

                            // move to next request
                            requests.Remove(node);
                            node = requests.First;
                        }
                        else
                            node = node.Next;
                    }
                }
                else
                    continue;
            }
        }
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public Carrier CreateCarrier()
    {
        Carrier temp = new Carrier(this);
        carriers.Add(temp);
        idleCarriers.Add(temp);
        return temp;
    }

    public void RequestCarrier(CarryRequest request)
    {
        // add request at the end of the linked list
        requests.AddLast(request);
    }

    public void MoveToIdle(Carrier carrier)
    {
        moveToIdle.AddLast(carrier);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public int CarrierCount
    {
        get { return carriers.Count; }
    }
}