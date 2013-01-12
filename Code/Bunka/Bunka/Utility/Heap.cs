using System;
using System.Collections.Generic;

public class MaxHeap<T> : Heap<T>
    where T : IComparable<T>
{
    public MaxHeap() { }

    public MaxHeap(ICollection<T> data) : base(data) { }

    protected override void Heapify(int root)
    {
        int p = Parent(root);

        if (p != -1 && heap[p].CompareTo(heap[root]) < 0)
        {
            Switch(p, root);
            Heapify(p);
        }
        else
        {
            int l = Left(root);
            int r = Right(root);

            if (l != -1 && r != -1)
            {
                int max = heap[l].CompareTo(heap[r]) > 0 ? l : r;
                if(heap[max].CompareTo(heap[root]) > 0)
                {
                    Switch(max, root);
                    Heapify(max);
                }
            }
            else if (l != -1 && heap[l].CompareTo(heap[root]) > 0)
            {
                Switch(l, root);
                Heapify(l);
            }
            else if (r != -1 && heap[r].CompareTo(heap[root]) > 0)
            {
                Switch(r, root);
                Heapify(r);
            }
        }
    }
}

public class MinHeap<T> : Heap<T>
    where T : IComparable<T>
{
    public MinHeap() { }

    public MinHeap(ICollection<T> data) : base(data) { }

    protected override void Heapify(int root)
    {
        int p = Parent(root);

        if (p != -1 && heap[p].CompareTo(heap[root]) > 0)
        {
            Switch(p, root);
            Heapify(p);
        }
        else
        {
            int l = Left(root);
            int r = Right(root);

            if (l != -1 && r != -1)
            {
                int max = heap[l].CompareTo(heap[r]) < 0 ? l : r;
                if(heap[max].CompareTo(heap[root]) < 0)
                {
                    Switch(max, root);
                    Heapify(max);
                }
            }
            else if (l != -1 && heap[l].CompareTo(heap[root]) < 0)
            {
                Switch(l, root);
                Heapify(l);
            }
            else if (r != -1 && heap[r].CompareTo(heap[root]) < 0)
            {
                Switch(r, root);
                Heapify(r);
            }
        }
    }
}

public abstract class Heap<T>
    where T : IComparable<T>
{
    protected List<T> heap = new List<T>();

    public Heap() { }

    public Heap(ICollection<T> data)
    {
        foreach (T t in data)
            Add(t);
    }

    public void Add(T item)
    {
        heap.Add(item);
        Heapify(heap.Count - 1);
    }

    public T Top()
    {
        T top = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        Heapify(0);
        return top;
    }

    public T Peek()
    {
        return heap[0];
    }

    public int Count { get { return heap.Count; } }

    protected abstract void Heapify(int root);

    protected int Parent(int node)
    {
        int p = (node + 1) / 2 - 1;
        if (p >= 0 && p < heap.Count)
            return p;
        return -1;
    }

    protected int Left(int node)
    {
        int l = (node + 1) * 2 - 1;
        if (l < heap.Count)
            return l;
        return -1;
    }

    protected int Right(int node)
    {
        int r = (node + 1) * 2;
        if (r < heap.Count)
            return r;
        return -1;
    }

    protected void Switch(int i1, int i2)
    {
        T temp = heap[i1];
        heap[i1] = heap[i2];
        heap[i2] = temp;
    }
}