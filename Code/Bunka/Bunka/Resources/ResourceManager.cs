using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all resource types
enum ResourceType { Wood, Stone, Coal, Fish, IronOre, IronBar, Planks, None }

// class for resource administration
class ResourceManager
{
    BunkaGame game;

    List<Resource> resources;
    List<ResourceConverter> converters;
    List<ResourceProducer> producers;
    Dictionary<ResourceType, uint> resourceCounts;
    Dictionary<ResourceType, LinkedList<Resource>> freeResources;

    public ResourceManager(BunkaGame game)
    {
        this.game = game;

        resources = new List<Resource>();
        converters = new List<ResourceConverter>();
        producers = new List<ResourceProducer>();
        resourceCounts = new Dictionary<ResourceType, uint>();
        freeResources = new Dictionary<ResourceType, LinkedList<Resource>>();

        LinkedList<Resource> herp = new LinkedList<Resource>();
        herp.AddFirst(new Resource(this, ResourceType.Wood, 10));
        freeResources.Add(ResourceType.Wood, herp);

        LinkedList<Resource> derp = new LinkedList<Resource>();
        derp.AddFirst(new Resource(this, ResourceType.Stone, 10));
        freeResources.Add(ResourceType.Stone, derp);
    }

    public void Update(GameTime t)
    {
        // test
        Console.Clear();
        foreach (KeyValuePair<ResourceType, uint> pair in resourceCounts)
            Console.WriteLine(pair.Key + "\t | \t" + pair.Value);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void RequestResource(ResourceType type, uint amount, Building destination)
    {
        game.CarrierManager.RequestCarrier(new CarryRequest(type, amount, destination));
    }

    public uint ResourceCount(ResourceType type)
    {
        uint count = 0;
        resourceCounts.TryGetValue(type, out count);
        return count;
    }

    public Resource CreateResource(ResourceType type, uint amount = 0)
    {
        Resource resource = new Resource(this, type, amount);
        resources.Add(resource);
        return resource;
    }

    public ResourceProducer CreateResourceProducer(ResourceType type, uint amount, float speed)
    {
        ResourceProducer temp = new ResourceProducer(this, type, amount, speed);
        producers.Add(temp);
        return temp;
    }

    // overload for single input and output
    public ResourceConverter CreateResourceConverter(ResourceType input, ResourceType output, byte inSize, byte outSize, float speed)
    {
        ResourceConverter temp = new ResourceConverter(this, new ResourceType[] { input }, new ResourceType[] { output }, new byte[] { inSize }, new byte[] { outSize }, speed);
        converters.Add(temp);
        return temp;
    }

    // overload for multiple inputs and outputs
    public ResourceConverter CreateResourceConverter(ResourceType[] input, ResourceType[] output, byte[] inSize, byte[] outSize, float speed)
    {
        ResourceConverter temp = new ResourceConverter(this, input, output, inSize, outSize, speed);
        converters.Add(temp);
        return temp;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Dictionary<ResourceType, uint> ResourceCounts
    {
        get { return resourceCounts; }
    }

    public Dictionary<ResourceType, LinkedList<Resource>> FreeResources
    {
        get { return freeResources; }
    }
}