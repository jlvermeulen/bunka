using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for managing construction of buildings
public class ConstructionManager
{
    LinkedList<ConstructionRequest> constructionRequests;
    List<ConstructionHandler> collect, build;
    LinkedList<BuildRequest> buildRequests;
    LinkedList<Builder> moveToIdle;
    List<Builder> builders, idleBuilders, busyBuilders;

    public ConstructionManager()
    {
        constructionRequests = new LinkedList<ConstructionRequest>();
        collect = new List<ConstructionHandler>();
        build = new List<ConstructionHandler>();
        buildRequests = new LinkedList<BuildRequest>();
        moveToIdle = new LinkedList<Builder>();
        builders = new List<Builder>();
        idleBuilders = new List<Builder>();
        busyBuilders = new List<Builder>();

        CreateBuilder();
    }

    public void AddInitialConstruction()
    {
        ConstructBuilding(BuildingType.Lumberjack);
        ConstructBuilding(BuildingType.Quarry);
        ConstructBuilding(BuildingType.CoalMine);
        ConstructBuilding(BuildingType.IronMine);
        ConstructBuilding(BuildingType.IronSmelter);
    }

    public void Update(GameTime t)
    {
        foreach (Builder b in builders)
            b.Update(t);

        // move finished builders to the idle list
        if (moveToIdle.Count > 0)
        {
            LinkedListNode<Builder> node = moveToIdle.First;
            while (node != null)
            {
                busyBuilders.Remove(node.Value);
                idleBuilders.Add(node.Value);
                moveToIdle.Remove(node);
                node = moveToIdle.First;
            }
        }

        // process new construction requests
        if (constructionRequests.Count > 0)
        {
            LinkedListNode<ConstructionRequest> node = constructionRequests.First;
            while (node != null)
            {
                collect.Add(BunkaGame.BuildingManager.BuildingLoader.CreateConstructionRequest(node.Value.BuildingType));
                constructionRequests.Remove(node);
                node = constructionRequests.First;
            }
        }

        // move buildings that are done collecting to the build list and request a builder
        for (int i = collect.Count - 1; i >= 0; i--)
            if (!collect[i].Location.Collecting)
            {
                RequestBuilder(collect[i].Location);
                build.Add(collect[i]);
                collect.RemoveAt(i);
            }

        // process new building requests
        if (buildRequests.Count > 0)
        {
            // first request
            LinkedListNode<BuildRequest> node = buildRequests.First;

            // keep processing as long as there is another request and an idle builder
            while (node != null && idleBuilders.Count > 0)
            {
                // give order to builder and move it from idle to busy
                idleBuilders[0].CurrentConstruction = node.Value.Location;
                busyBuilders.Add(idleBuilders[0]);
                idleBuilders.RemoveAt(0);

                buildRequests.Remove(node);
                node = buildRequests.First;
            }
        }
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void ConstructBuilding(BuildingType type)
    {
        constructionRequests.AddLast(new ConstructionRequest(type));
    }

    public Builder CreateBuilder()
    {
        Builder temp = new Builder();
        builders.Add(temp);
        idleBuilders.Add(temp);
        return temp;
    }

    public void RequestBuilder(ConstructionBuilding location)
    {
        buildRequests.AddLast(new BuildRequest(location));
    }

    public void MoveToIdle(Builder builder)
    {
        moveToIdle.AddFirst(builder);
    }

    public void CompleteConstruction(ConstructionBuilding location)
    {
        build.Remove(location.ConstructionHandler);
        BunkaGame.BuildingManager.CreateBuilding(location.ConstructionHandler.BuildingType);
        System.Console.WriteLine("Built {0}", location.ConstructionHandler.BuildingType.ToString());
    }
}