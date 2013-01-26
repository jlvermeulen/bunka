using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        this.constructionRequests = new LinkedList<ConstructionRequest>();
        this.collect = new List<ConstructionHandler>();
        this.build = new List<ConstructionHandler>();
        this.buildRequests = new LinkedList<BuildRequest>();
        this.moveToIdle = new LinkedList<Builder>();
        this.builders = new List<Builder>();
        this.idleBuilders = new List<Builder>();
        this.busyBuilders = new List<Builder>();

        CreateBuilder();
    }
    /*
    public void AddInitialConstruction()
    {
        ConstructBuilding(BuildingType.Lumberjack);
        ConstructBuilding(BuildingType.Quarry);
        ConstructBuilding(BuildingType.CoalMine);
        ConstructBuilding(BuildingType.IronMine);
        ConstructBuilding(BuildingType.IronSmelter);
    }
    */
    public void Update(GameTime t)
    {
        foreach (Builder b in this.builders)
            b.Update(t);

        // move finished builders to the idle list
        if (this.moveToIdle.Count > 0)
        {
            LinkedListNode<Builder> node = this.moveToIdle.First;
            while (node != null)
            {
                this.busyBuilders.Remove(node.Value);
                this.idleBuilders.Add(node.Value);
                this.moveToIdle.Remove(node);
                node = this.moveToIdle.First;
            }
        }

        // move buildings that are done collecting to the build list and request a builder
        for (int i = this.collect.Count - 1; i >= 0; i--)
            if (!this.collect[i].Location.Collecting)
            {
                this.RequestBuilder(collect[i].Location);
                this.build.Add(collect[i]);
                this.collect.RemoveAt(i);
            }

        // process new building requests
        if (this.buildRequests.Count > 0)
        {
            // first request
            LinkedListNode<BuildRequest> node = this.buildRequests.First;

            // keep processing as long as there is another request and an idle builder
            while (node != null && this.idleBuilders.Count > 0)
            {
                // give order to builder and move it from idle to busy
                this.idleBuilders[0].Destination = node.Value.Location;
                this.busyBuilders.Add(idleBuilders[0]);
                this.idleBuilders.RemoveAt(0);

                this.buildRequests.Remove(node);
                node = this.buildRequests.First;
            }
        }
    }

    public void Draw(SpriteBatch s)
    {
        foreach (Builder b in this.builders)
            b.Draw(s);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void ConstructBuilding(BuildingType type, CPoint position)
    {
        if (BunkaGame.MapManager.IsValidIndex(position) && BunkaGame.MapManager[position] == null)
            this.collect.Add(BunkaGame.BuildingManager.BuildingLoader.CreateConstructionRequest(type, position));
    }

    public Builder CreateBuilder()
    {
        Builder temp = new Builder(new Vector2(50, 50), 3);
        this.builders.Add(temp);
        this.idleBuilders.Add(temp);
        return temp;
    }

    public void RequestBuilder(ConstructionBuilding location)
    {
        this.buildRequests.AddLast(new BuildRequest(location));
    }

    public void MoveToIdle(Builder builder)
    {
        this.moveToIdle.AddFirst(builder);
    }

    public void CompleteConstruction(ConstructionBuilding location)
    {
        this.build.Remove(location.ConstructionHandler);
        BunkaGame.BuildingManager.CreateBuilding(location.ConstructionHandler.BuildingType, location.Position);
    }
}