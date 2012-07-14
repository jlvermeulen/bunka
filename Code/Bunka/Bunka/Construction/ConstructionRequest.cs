struct ConstructionRequest
{
    BuildingType type;

    public ConstructionRequest(BuildingType type)
    {
        this.type = type;
    }

    public BuildingType BuildingType
    {
        get { return type; }
    }
}