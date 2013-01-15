using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all resource types
public enum ResourceType { Wood, Stone, Coal, Fish, IronOre, IronBar, Planks, None }

// class for resource administration
public class ResourceManager
{
    List<Resource> resources;
    List<ResourceConverter> converters;
    List<ResourceProducer> producers;

    public ResourceManager()
    {
        this.resources = new List<Resource>();
        this.converters = new List<ResourceConverter>();
        this.producers = new List<ResourceProducer>();
        this.ResourceCounts = new Dictionary<ResourceType, uint>();
        this.FreeResources = new Dictionary<ResourceType, LinkedList<Resource>>();
    }

    public void AddInitialResources()
    {
        LinkedList<Resource> herp = new LinkedList<Resource>();
        herp.AddFirst(new Resource(ResourceType.Wood, 10));
        this.FreeResources.Add(ResourceType.Wood, herp);

        LinkedList<Resource> derp = new LinkedList<Resource>();
        derp.AddFirst(new Resource(ResourceType.Stone, 10));
        this.FreeResources.Add(ResourceType.Stone, derp);
    }

    public void Update(GameTime t)
    {
        // test
        Console.Clear();
        foreach (KeyValuePair<ResourceType, uint> pair in this.ResourceCounts)
            Console.WriteLine(pair.Key + "\t | \t" + pair.Value);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void RequestResource(ResourceType type, uint amount, Building destination)
    {
        BunkaGame.CarrierManager.RequestCarrier(new CarryRequest(type, amount, destination));
    }

    public uint ResourceCount(ResourceType type)
    {
        uint count = 0;
        this.ResourceCounts.TryGetValue(type, out count);
        return count;
    }

    public Resource CreateResource(ResourceType type, uint amount = 0)
    {
        Resource resource = new Resource(type, amount);
        this.resources.Add(resource);
        return resource;
    }

    public ResourceProducer CreateResourceProducer(ResourceType type, uint amount, float speed)
    {
        ResourceProducer temp = new ResourceProducer(type, amount, speed);
        this.producers.Add(temp);
        return temp;
    }

    // overload for single input and output
    public ResourceConverter CreateResourceConverter(ResourceType input, ResourceType output, byte inSize, byte outSize, float speed)
    {
        ResourceConverter temp = new ResourceConverter(new ResourceType[] { input }, new ResourceType[] { output }, new byte[] { inSize }, new byte[] { outSize }, speed);
        this.converters.Add(temp);
        return temp;
    }

    // overload for multiple inputs and outputs
    public ResourceConverter CreateResourceConverter(ResourceType[] input, ResourceType[] output, byte[] inSize, byte[] outSize, float speed)
    {
        ResourceConverter temp = new ResourceConverter(input, output, inSize, outSize, speed);
        this.converters.Add(temp);
        return temp;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Dictionary<ResourceType, uint> ResourceCounts { get; private set; }

    public Dictionary<ResourceType, LinkedList<Resource>> FreeResources { get; private set; }
}