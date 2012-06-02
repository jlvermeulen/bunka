using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

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
        Resource r1 = new ResourceWood(this, 10);
        resources.Add(r1);
        Resource r2 = new ResourceStone(this, 0);
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

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public List<Resource> ResourceList
    {
        get { return resources; }
    }

    public List<ResourceConverter> ConverterList
    {
        get { return converters; }
    }

    public Dictionary<ResourceType, uint> ResourceCounts
    {
        get { return resourceCounts; }
    }
}