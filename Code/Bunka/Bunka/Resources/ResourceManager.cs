using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all resource types
enum ResourceType { Wood, Stone, None }

// class for resource administration
class ResourceManager
{
    List<Resource> resources;
    List<ResourceConverter> converters;
    List<ResourceProducer> producers;
    Dictionary<ResourceType, uint> resourceCounts;

    public ResourceManager()
    {
        resources = new List<Resource>();
        converters = new List<ResourceConverter>();
        producers = new List<ResourceProducer>();
        resourceCounts = new Dictionary<ResourceType, uint>();
    }

    public void Update(GameTime t)
    {
        foreach (ResourceProducer p in producers)
        {
            p.Update(t);
        }
        foreach (ResourceConverter c in converters)
        {
            c.Update(t);
        }
        // test
        Console.Clear();
        Console.WriteLine(resources[0].ResourceType + "\t | \t" + resources[0].Amount);
        Console.WriteLine(resources[1].ResourceType + "\t | \t" + resources[1].Amount);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public uint ResourceCount(ResourceType type)
    {
        uint count = 0;
        resourceCounts.TryGetValue(type, out count);
        return count;
    }

    public Resource CreateResource(ResourceType type, uint amount)
    {
        Resource resource = null;
        switch (type)
        {
            case ResourceType.None:
                resource = new Resource_None(this);
                resources.Add(resource);
                break;
            case ResourceType.Stone:
                resource = new Resource_Stone(this, amount);
                resources.Add(resource);
                break;
            case ResourceType.Wood:
                resource = new Resource_Wood(this, amount);
                resources.Add(resource);
                break;
            default:
                break;
        }
        return resource;
    }

    public ResourceProducer CreateResourceProducer(Resource resource, uint amount, float speed)
    {
        ResourceProducer temp = new ResourceProducer(this, resource, amount, speed);
        producers.Add(temp);
        return temp;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Dictionary<ResourceType, uint> ResourceCounts
    {
        get { return resourceCounts; }
    }
}