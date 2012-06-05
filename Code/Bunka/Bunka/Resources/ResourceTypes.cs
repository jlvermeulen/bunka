// classes for all resource types
class Resource_Stone : Resource
{
    public Resource_Stone(ResourceManager manager, uint amount)
        : base(manager, ResourceType.Stone, amount)
    {

    }
}

class Resource_Wood : Resource
{
    public Resource_Wood(ResourceManager manager, uint amount)
        : base(manager, ResourceType.Wood, amount)
    {

    }
}

class Resource_None : Resource
{
    public Resource_None(ResourceManager manager)
        : base(manager, ResourceType.None, 0)
    {

    }
}