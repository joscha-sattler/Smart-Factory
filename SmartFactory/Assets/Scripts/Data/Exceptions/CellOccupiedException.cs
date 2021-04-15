using System;
using System.Collections;
using System.Collections.Generic;

public class CellOccupiedException : Exception
{
    public CellOccupiedException(GridCell cell, string addMessage) : 
        base("Cell at coordinates: " + cell.Coords + " is occupied already! Cannot place here!" + addMessage)
    {
        
    }
}
