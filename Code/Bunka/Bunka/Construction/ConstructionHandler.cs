using Microsoft.Xna.Framework;

class ConstructionHandler
{
    ConstructionBuilding location;
    BuildingType type;

    public ConstructionHandler(BuildingType type, ResourceType[] costTypes, uint[] costAmounts, float constructionTime, ResourceManager resourceManager)
    {
        this.location = new ConstructionBuilding(this, costTypes, costAmounts, constructionTime);
        this.type = type;

        // request required resources
        for (int i = 0; i < costTypes.Length; i++)
            resourceManager.RequestResource(costTypes[i], costAmounts[i], location);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ConstructionBuilding Location
    {
        get { return location; }
    }

    public BuildingType BuildingType
    {
        get { return type; }
    }
}