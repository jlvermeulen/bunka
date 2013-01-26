using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        this.CreateCarrier();
    }

    public void Update(GameTime t)
    {
        // update busy carriers
        foreach (Carrier c in this.carriers)
            c.Update(t);

        // move finished carriers to idle list
        if (this.moveToIdle.Count > 0)
        {
            LinkedListNode<Carrier> node = this.moveToIdle.First;
            while (node != null)
            {
                this.busyCarriers.Remove(node.Value);
                this.idleCarriers.Add(node.Value);
                this.moveToIdle.Remove(node);
                node = this.moveToIdle.First;
            }
        }

        // handle carrier requests
        if (this.requests.Count > 0)
        {
            // first request
            LinkedListNode<CarryRequest> node = this.requests.First;

            // check if carrier is available and if there is still a request
            while (this.idleCarriers.Count > 0 && node != null)
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
                            this.idleCarriers[0].Carrying = node.Value.ResourceType;
                            this.idleCarriers[0].Destination = node.Value.Destination;

                            // resource amount is large enough
                            if (r.Amount >= node.Value.Amount)
                            {
                                this.idleCarriers[0].Amount = node.Value.Amount;
                                r.Amount -= node.Value.Amount;
                            }
                            // resource amount is insufficient
                            else
                            {
                                // create new request for remainder of requested resource
                                this.RequestCarrier(new CarryRequest(node.Value.ResourceType, node.Value.Amount - r.Amount, node.Value.Destination));

                                this.idleCarriers[0].Amount = r.Amount;
                                r.Amount = 0;
                            }                            

                            // move carrier to busy carriers list
                            this.busyCarriers.Add(this.idleCarriers[0]);
                            this.idleCarriers.RemoveAt(0);

                            // move to next request
                            this.requests.Remove(node);
                            node = this.requests.First;
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

    public void Draw(SpriteBatch s)
    {
        foreach (Carrier c in carriers)
            c.Draw(s);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public Carrier CreateCarrier()
    {
        Carrier temp = new Carrier(new Vector2(50, 50), 3);
        this.carriers.Add(temp);
        this.idleCarriers.Add(temp);
        return temp;
    }

    public void RequestCarrier(CarryRequest request)
    {
        this.requests.AddLast(request);
    }

    public void MoveToIdle(Carrier carrier)
    {
        this.moveToIdle.AddLast(carrier);
    }
}