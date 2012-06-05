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
    Dictionary<ResourceType, uint> resourceCounts;

    public ResourceManager()
    {
        resources = new List<Resource>();
        converters = new List<ResourceConverter>();
        resourceCounts = new Dictionary<ResourceType, uint>();
        Test();
    }

    void Test()
    {
        Resource r1 = new Resource_Wood(this, 10);
        resources.Add(r1);
        Resource r2 = new Resource_Stone(this, 0);
        resources.Add(r2);
        converters.Add(new ResourceConverter(this, new Resource[1] { r1 }, new Resource[1] { r2 }, new byte[1] { 3 }, new byte[1] { 2 }, 5));
    }

    public void Update(GameTime t)
    {
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

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Dictionary<ResourceType, uint> ResourceCounts
    {
        get { return resourceCounts; }
    }
}