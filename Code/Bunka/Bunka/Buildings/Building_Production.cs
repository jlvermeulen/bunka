// parent class for buildings that produce resources
abstract class Building_Production : Building
{
    ResourceProducer[] production;

    // overload for single producer
    public Building_Production(BuildingType type, ResourceManager resourceManager, ResourceProducer production)
        : base(type)
    {
        this.production = new ResourceProducer[] { production };
    }

    // overload for multiple producers
    public Building_Production(BuildingType type, ResourceManager resourceManager, ResourceProducer[] production)
        : base(type)
    {
        this.production = production;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceProducer[] ResourceProducers
    {
        get { return production; }
        set { production = value; }
    }
}