using Microsoft.Xna.Framework;

public class ConstructionHandler
{
    ConstructionBuilding location;
    BuildingType type;

    public ConstructionHandler(BuildingType type, ResourceType[] costTypes, uint[] costAmounts, float constructionTime, Vector2 position)
    {
        this.location = new ConstructionBuilding(this, costTypes, costAmounts, constructionTime, position);
        this.type = type;

        // request required resources
        for (int i = 0; i < costTypes.Length; i++)
            BunkaGame.ResourceManager.RequestResource(costTypes[i], costAmounts[i], location);
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