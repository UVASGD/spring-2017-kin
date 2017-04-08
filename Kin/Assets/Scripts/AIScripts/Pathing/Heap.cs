using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>{

    T[] items;
    int numItems;

    public Heap(int maxSize)
    {
        items = new T[maxSize];
    }

    public void Add(T item)
    {
        item.HeapIndex = numItems;
        items[numItems] = item;
        SiftUp(item);
        numItems++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        numItems--;
        items[0] = items[numItems];
        items[0].HeapIndex = 0;
        SiftDown(items[0]);
        return firstItem;
    }

    void SiftUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parent = items[parentIndex];
            if (item.CompareTo(parent) > 0)
            {
                Swap(item,parent);
            }
            else
                break;
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void SiftDown(T item)
    {
        while (true) {
            int leftChildIndex = item.HeapIndex * 2 + 1;
            int rightChildIndex = item.HeapIndex * 2 + 2;
            int temp = 0;

            if (leftChildIndex < numItems)
            {
                temp = leftChildIndex;
                if (rightChildIndex < numItems)
                {
                    if (items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0)
                    {
                        temp = rightChildIndex;
                    }
                }
                if (item.CompareTo(items[temp]) < 0)
                {
                    Swap(item, items[temp]);
                }
                else
                    return;
            }
            else
                return;
        }
    }

    void Swap(T a, T b)
    {
        items[a.HeapIndex] = b;
        items[b.HeapIndex] = a;
        int temp = a.HeapIndex;
        a.HeapIndex = b.HeapIndex;
        b.HeapIndex = temp;
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    public int Count
    {
        get {
            return numItems;
        }
    }
    public void UpdateItem(T item)
    {
        SiftUp(item);
    }
}


public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
