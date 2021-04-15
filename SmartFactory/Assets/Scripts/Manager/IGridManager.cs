using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridManager
{
    GridCell GetCell(Coordinates coords);
}
