using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class ConstructionBuilding : Building
{
    Dictionary<ResourceType, uint> costs;

    public ConstructionBuilding(ConstructionHandler handler, ResourceType[] costTypes, uint[] costAmounts, float constructionTime, CPoint position)
        : base(BuildingType.Construction, position)
    {
        BunkaGame.MapManager[position] = this;
        this.ConstructionHandler = handler;
        this.ConstructionTime = constructionTime;
        this.costs = new Dictionary<ResourceType, uint>();

        for (int i = 0; i < costTypes.Length; i++)
            this.costs.Add(costTypes[i], costAmounts[i]);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public override void DeliverResource(ResourceType type, uint amount)
    {
        // update requested resources
        uint curr = this.costs[type];
        if (amount >= curr)
            this.costs.Remove(type);
        else
            this.costs[type] = curr - amount;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public bool Collecting
    {
        get { return this.costs.Count > 0; }
    }

    public float ConstructionTime { get; set; }

    public ConstructionHandler ConstructionHandler { get; private set; }
}