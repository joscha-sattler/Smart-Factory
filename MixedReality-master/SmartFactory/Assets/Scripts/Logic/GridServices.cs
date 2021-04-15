using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridServices
{
    private IGridManager gridManager = GridManager.Instance;

    public void PlaceBuilding(Building building, Coordinates coords)
    {
        building.transform.position = new Vector3(coords.x, 0, coords.z);
        GridCell cell = gridManager.GetCell(coords);

        if (cell.Building != null)
        {
            throw new CellOccupiedException(cell, "");
        }

        List<GridCell> neighborCells = new List<GridCell>();

        foreach (Vector3 routeDelta in building.RouteDeltas)
        {
            Vector3 delta = Quaternion.Euler(0, building.transform.eulerAngles.y, 0) * routeDelta;
            neighborCells.Add(gridManager.GetCell(coords + delta));
        }

        cell.Building = building;
        building.Cell = cell;
        building.NeighborCells = neighborCells;

        //Benachrichtigung an den EventService, der das Signal an den SoundService weitergibt
        EventServices.Instance.OnBuildingPlaced(building.PrefabId);
    }

    public Building PickUpBuilding(Building building)
    {
        building.Cell.Building = null;
        return building;
    }

    public GridCell GetCell(Coordinates coords)
    {
        return gridManager.GetCell(coords);
    }
    
    public Building GetBuildingAt(Vector3 pos)
    {
        return GetCell(new Coordinates(pos)).Building;
    }

    public List<Coordinates> GetConnectedNeighborCoords(Coordinates coords)
    {
        return GetConnectedNeighborCoords(gridManager.GetCell(coords));
    }

    public List<Coordinates> GetConnectedNeighborCoords(GridCell cell)
    {
        List<Coordinates> connected = new List<Coordinates>();

        foreach (GridCell neighbor in cell.Building.NeighborCells)
        {
            if (neighbor.Building != null && neighbor.Building.NeighborCells.Contains(cell))
            {
                connected.Add(neighbor.Coords);
            }
        }

        return connected;
    }
}
