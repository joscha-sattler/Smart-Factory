using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : IGridManager
{
    private static readonly GridManager instance = new GridManager();

    private List<GridCell> gridCells = new List<GridCell>();

    public List<GridCell> GridCells { get { return gridCells; } }
    public static GridManager Instance { get { return instance; } }   

    static GridManager() {}

    public GridCell GetCell(Coordinates coords)
    {
        GridCell cell = gridCells.Find(x => x.Coords == coords);

        if (cell == null)
        {
            cell = new GridCell(coords);
            gridCells.Add(cell);
        }

        return cell;
    }
}
