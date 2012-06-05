// building types of buildings that produce resources
class Building_Production_Quarry : Building_Production
{
    public Building_Production_Quarry(ResourceManager resourceManager)
        : base(BuildingType.Quarry, resourceManager, new ResourceProducer[1] { resourceManager.CreateResourceProducer(resourceManager.CreateResource(ResourceType.Stone, 0), 1, 5) })
    {

    }
}

class Building_Production_Lumberjack : Building_Production
{
    public Building_Production_Lumberjack(ResourceManager resourceManager)
        : base(BuildingType.Lumberjack, resourceManager, new ResourceProducer[1] { resourceManager.CreateResourceProducer(resourceManager.CreateResource(ResourceType.Wood, 0), 1, 5) })
    {

    }
}