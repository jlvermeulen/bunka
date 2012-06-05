﻿// parent class for buildings that produce resources
abstract class Building_Production : Building
{
    ResourceProducer[] production;

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