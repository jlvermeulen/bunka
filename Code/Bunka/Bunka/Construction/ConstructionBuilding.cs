using System.Collections.Generic;

class ConstructionBuilding : Building
{
    ConstructionHandler handler;
    Dictionary<ResourceType, uint> costs;
    float constructionTime;

    public ConstructionBuilding(ConstructionHandler handler, ResourceType[] costTypes, uint[] costAmounts, float constructionTime)
        : base(BuildingType.Construction)
    {
        this.handler = handler;
        this.constructionTime = constructionTime;
        costs = new Dictionary<ResourceType, uint>();

        for (int i = 0; i < costTypes.Length; i++)
            costs.Add(costTypes[i], costAmounts[i]);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void DeliverResource(ResourceType type, uint amount)
    {
        // update requested resources
        uint curr = costs[type];
        if (amount >= curr)
            costs.Remove(type);
        else
            costs[type] = curr - amount;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public bool Collecting
    {
        get { return costs.Count > 0; }
    }

    public float ConstructionTime
    {
        get { return constructionTime; }
        set { constructionTime = value; }
    }

    public ConstructionHandler ConstructionHandler
    {
        get { return handler; }
    }
}