using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public static class Pathfinder
{
    // find shortest path using A*
    public static LinkedList<CPoint> GetPath(CPoint from, CPoint dest)
    {
        HashSet<CPoint> closed = new HashSet<CPoint>();
        Dictionary<CPoint, PathNode> openDict = new Dictionary<CPoint, PathNode>();
        MinHeap<PathNode> open = new MinHeap<PathNode>();
        PathNode node = new PathNode(from, BunkaGame.MapManager.ManhattanDistance(from, dest), null);
        open.Add(node);
        openDict.Add(from, node);

        PathNode last = null;
        List<CPoint> reachable;
        while (open.Count > 0)
        {
            PathNode best = open.Top();

            openDict.Remove(best.Position);
            closed.Add(best.Position);

            if (best.Position == dest)
            {
                last = best;
                break;
            }

            reachable = BunkaGame.MapManager.FreeNeighbours(best.Position);
            foreach (CPoint p in reachable)
            {
                if (closed.Contains(p))
                    continue;

                if (openDict.TryGetValue(p, out node))
                {
                    if (best.G + 1 < node.G)
                    {
                        node.Parent = best;
                        node.G = best.G + 1;
                    }
                }
                else
                {
                    node = new PathNode(p, BunkaGame.MapManager.ManhattanDistance(p, dest), best);
                    open.Add(node);
                    openDict.Add(p, node);
                }
            }
        }

        if (last == null)
            return null;

        LinkedList<CPoint> path = new LinkedList<CPoint>();
        while (last != null)
        {
            path.AddFirst(last.Position);
            last = last.Parent;
        }

        return path;
    }

    private class PathNode : IComparable<PathNode>
    {
        public PathNode(CPoint position, int h, PathNode parent)
        {
            this.Position = position;
            if (parent != null)
                this.G = parent.G + 1;
            else
                this.G = 0;
            this.H = h;
            this.Parent = parent;
        }

        public int CompareTo(PathNode other)
        {
            return this.F - other.F;
        }

        public CPoint Position
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