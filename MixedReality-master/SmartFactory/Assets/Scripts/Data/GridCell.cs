using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Coordinates
{
    public int x;
    public int z;

    public Coordinates(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public Coordinates(Vector3 position)
    {
        x = Mathf.FloorToInt(position.x + 0.5f);
        z = Mathf.FloorToInt(position.z + 0.5f);
    }

    public Vector3 ToWorldPosition()
    {
        return new Vector3(x, 0, z);
    }

    public override bool Equals(object o)
    {
        return base.Equals(o);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return x + "," + z;
    }

    public static Coordinates operator +(Coordinates coords, Vector3 delta)
    {
        int newX = coords.x + Mathf.FloorToInt(delta.x + 0.5f);
        int newZ = coords.z + Mathf.FloorToInt(delta.z + 0.5f);
        return new Coordinates(newX, newZ);
    }

    public static bool operator ==(Coordinates lh, Coordinates rh)
    {
        return (lh.x == rh.x && lh.z == rh.z);
    }

    public static bool operator !=(Coordinates lh, Coordinates rh)
    {
        return !(lh == rh);
    }
}

public class GridCell
{
    private Coordinates coords;
    private Building building;

    public Coordinates Coords { get { return coords; } }
    public Building Building { get { return building; } set { building = value; } }

    public GridCell(Coordinates _coords)
    {
        coords = _coords;
    }

    public GridCell(Vector3 position)
    {
        coords = new Coordinates(position);
    }    
}
