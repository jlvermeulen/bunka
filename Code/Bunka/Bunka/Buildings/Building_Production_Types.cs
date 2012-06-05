// building types of buildings that produce resources
class Building_Production_Quarry : Building_Production
{
    public Building_Production_Quarry(ResourceManager resourceManager)
        : base(BuildingType.Quarry, resourceManager)
    {

    }
}

class Building_Production_Lumberjack : Building_Production
{
    public Building_Production_Lumberjack(ResourceManager resourceManager)
        : base(BuildingType.Lumberjack, resourceManager)
    {

    }
}