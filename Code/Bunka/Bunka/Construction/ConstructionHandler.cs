using Microsoft.Xna.Framework;

public class ConstructionHandler
{
    public ConstructionHandler(BuildingType type, ResourceType[] costTypes, uint[] costAmounts, float constructionTime, CPoint position)
    {
        this.Location = new ConstructionBuilding(this, costTypes, costAmounts, constructionTime, position);
        this.BuildingType = type;

        // request required resources
        for (int i = 0; i < costTypes.Length; i++)
            BunkaGame.ResourceManager.RequestResource(costTypes[i], costAmounts[i], this.Location);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ConstructionBuilding Location { get; private set; }

    public BuildingType BuildingType { get; private set; }
}