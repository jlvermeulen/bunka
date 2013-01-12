using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public static class Pathfinder
{
    // find shortest path using A*
    public static List<Point> GetPath(Point from, Point dest)
    {
        HashSet<Point> closed = new HashSet<Point>();
        Dictionary<Point, PathNode> open = new Dictionary<Point, PathNode>();
        open.Add(from, new PathNode(from, BunkaGame.MapManager.ManhattanDistance(from, dest), null));

        PathNode last = null;
        List<Point> reachable;
        while (open.Count > 0)
        {
            PathNode best = new PathNode(new Point(-1, -1), int.MaxValue, null);
            foreach (PathNode p in open.Values)
                if (p.F < best.F)
                    best = p;

            open.Remove(best.Position);
            closed.Add(best.Position);

            if (best.Position == dest)
            {
                last = best;
                break;
            }

            reachable = BunkaGame.MapManager.FreeNeighbours(best.Position);
            PathNode node;
            foreach (Point p in reachable)
            {
                if (closed.Contains(p))
                    continue;

                if (open.TryGetValue(p, out node))
                {
                    if (best.G + 1 < node.G)
                    {
                        node.Parent = best;
                        node.G = best.G + 1;
                    }
                }
                else
                    open.Add(p, new PathNode(p, BunkaGame.MapManager.ManhattanDistance(p, dest), best));
            }
        }

        if (last == null)
            return null;

        List<Point> path = new List<Point>();
        while (last != null)
        {
            path.Add(last.Position);
            last = last.Parent;
        }
        path.Reverse();

        return path;
    }

    private class PathNode
    {
        public PathNode(Point position, int h, PathNode parent)
        {
            this.Position = position;
            if (parent != null)
                this.G = parent.G + 1;
            else
                this.G = 0;
            this.H = h;
            this.Parent = parent;
        }

        public Point Position
        {
            get;
            set;
        }

        public int F
        {
            get { return G + H; }
        }

        public int H
        {
            get;
            set;
        }

        public int G
        {
            get;
            set;
        }

        public PathNode Parent
        {
            get;
            set;
        }
    }
}