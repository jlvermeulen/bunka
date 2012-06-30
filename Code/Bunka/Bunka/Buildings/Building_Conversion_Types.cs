// building types of buildings that convert resources
class Building_Conversion_CokingPlant : Building_Conversion
{
    public Building_Conversion_CokingPlant(ResourceManager resourceManager)
        : base(BuildingType.CokingPlant, resourceManager, resourceManager.CreateResourceConverter(ResourceType.Wood, ResourceType.Coal, 3, 1, 5))
    {

    }
}