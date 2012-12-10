using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for carrier administration
public class CarrierManager
{
    List<Carrier> carriers, idleCarriers, busyCarriers;
    LinkedList<Carrier> moveToIdle;
    LinkedList<CarryRequest> requests;

    public CarrierManager()
    {
        this.carriers = new List<Carrier>();
        this.idleCarriers = new List<Carrier>();
        this.busyCarriers = new List<Carrier>();
        this.moveToIdle = new LinkedList<Carrier>();
        this.requests = new LinkedList<CarryRequest>();

        CreateCarrier();
    }

    public void Update(GameTime t)
    {
        // update busy carriers
        foreach (Carrier c in carriers)
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
            while (idleCarriers.Count > 0 && node != null)
            {
                // retrieve list of free resources of the desired type
                // TODO: determine which resource is the closest to the destination
                LinkedList<Resource> list;
                if (BunkaGame.ResourceManager.FreeResources.TryGetValue(node.Value.ResourceType, out list))
                {
                    // skip if list is empty
                    if (list.Count == 0)
                        continue;

                    bool broken = false;
                    foreach (Resource r in list)
                    {
                        // check if resource is not empty
                        if (r.Amount > 0)
                        {
                            // give resource to carrier
                            // TODO: determine closest carrier
                            idleCarriers[0].Carrying = node.Value.ResourceType;
                            idleCarriers[0].Destination = node.Value.Destination;

                            // resource amount is large enough
                            if (r.Amount >= node.Value.Amount)
                            {
                                idleCarriers[0].Amount = node.Value.Amount;
                                r.Amount -= node.Value.Amount;
                            }
                            // resource amount is insufficient
                            else
                            {
                                // create new request for remainder of requested resource
                                RequestCarrier(new CarryRequest(node.Value.ResourceType, node.Value.Amount - r.Amount, node.Value.Destination));

                                idleCarriers[0].Amount = r.Amount;
                                r.Amount = 0;
                            }                            

                            // move carrier to busy carriers list
                            busyCarriers.Add(idleCarriers[0]);
                            idleCarriers.RemoveAt(0);

                            // move to next request
                            requests.Remove(node);
                            node = requests.First;
                            broken = true;
                            break;
                        }
                    }

                    // move to next request if current request could not be fulfilled
                    if (!broken)
                        node = node.Next;
                }
                else
                    node = node.Next;
            }
        }
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

    public void RequestCarrier(CarryRequest request)
    {
        requests.AddLast(request);
    }

    public void MoveToIdle(Carrier carrier)
    {
        moveToIdle.AddLast(carrier);
    }
}