using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PriorityCoords
{
    public float priority;
    public Coordinates coords;

    public PriorityCoords(Coordinates _coords, float _priority)
    {
        coords = _coords;
        priority = _priority;
    }
}

public class PriorityQueue
{
    List<PriorityCoords> list = new List<PriorityCoords>();
    
    public void Add(PriorityCoords entry)
    {
        int i;

        for (i=0; i < list.Count; i++)
        {
            if (list[i].priority > entry.priority)
            {
                break;
            }
        }

        list.Insert(i, entry);
    }

    public Coordinates Pop()
    {
        Coordinates coords = list[0].coords;
        list.RemoveAt(0);
        return coords;
    }

    public bool IsEmpty()
    {
        return (list.Count == 0);
    }
}
