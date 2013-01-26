using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public abstract class Worker
{
    Building target;
    protected LinkedList<CPoint> path;

    protected Worker(Vector2 position, float maxVelocity)
    {
        this.Position = position;
        this.MaxVelocity = maxVelocity;
    }

    protected virtual void UpdateAimAndVelocity()
    {
        Vector2 waypoint = BunkaGame.MapManager.IndexToCellCentre(path.First.Value);
        if (Vector2.Distance(this.Position, waypoint) <= this.MaxVelocity)
        {
            path.RemoveFirst();
            if (path.Count > 0)
                waypoint = BunkaGame.MapManager.IndexToCellCentre(path.First.Value);
        }
        this.Aim = Vector2.Normalize(waypoint - this.Position);
        this.Velocity = Math.Min(this.MaxVelocity, Vector2.Distance(this.Position, waypoint));
    }

    public Vector2 Position { get; set; }

    public float Velocity { get; set; }

    public float MaxVelocity { get; set; }

    protected Vector2 Aim { get; set; }

    public Building Destination
    {
        get { return this.target; }
        set
        {
            this.target = value;
            if (this.target != null)
                this.path = Pathfinder.GetPath(BunkaGame.MapManager.PositionToIndex(this.Position), this.target.Position);
        }
    }
}