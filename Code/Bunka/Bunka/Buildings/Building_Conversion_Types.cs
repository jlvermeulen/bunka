// building types of buildings that convert resources
class Building_Conversion_CokingPlant : Building_Conversion
{
    public Building_Conversion_CokingPlant(ResourceManager resourceManager)
        : base(BuildingType.CokingPlant, resourceManager, new ResourceConverter[] { resourceManager.CreateResourceConverter(resourceManager.CreateResource(ResourceType.Wood), resourceManager.CreateResource(ResourceType.Coal), 3, 1, 5) })
    {

    }
}