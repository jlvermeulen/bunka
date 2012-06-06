// parent class for buildings that convert resources
abstract class Building_Conversion : Building
{
    ResourceConverter[] converters;

    // overload for single converter
    public Building_Conversion(BuildingType type, ResourceManager resourceManager, ResourceConverter converter)
        : base(type)
    {
        this.converters = new ResourceConverter[] { converter };
    }

    // overload for multiple converters
    public Building_Conversion(BuildingType type, ResourceManager resourceManager, ResourceConverter[] converters)
        : base(type)
    {
        this.converters = converters;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceConverter[] ResourceConverters
    {
        get { return converters; }
        set { converters = value; }
    }
}