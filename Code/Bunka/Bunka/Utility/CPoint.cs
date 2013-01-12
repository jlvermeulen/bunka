using System;
using Microsoft.Xna.Framework;

public struct CPoint : IComparable<CPoint>, IEquatable<CPoint>
{
    public CPoint(int x, int y) : this()
    {
        this.X = x;
        this.Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public int CompareTo(CPoint other)
    {
        int y = this.Y - other.Y;
        if (y != 0)
            return y;
        else
            return this.X - other.X;
    }

    public bool Equals(CPoint other)
    {
        return this.X == other.X && this.Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        return this.Equals((CPoint)obj);
    }

    public override int GetHashCode()
    {
        return X ^ Y;
    }

    public static bool operator ==(CPoint left, CPoint right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CPoint left, CPoint right)
    {
        return !left.Equals(right);
    }
}