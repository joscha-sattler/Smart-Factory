using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Manual
{
    string workstation;
    List<string> items;
    string parameters;

    public string Workstation { get { return workstation; } }
    public List<string> Items { get { return items; } }
    public string Parameters { get { return parameters; } }

    public Manual(string _workstation, List<string> _items, string _parameters)
    {
        workstation = _workstation;
        items = _items;
        parameters = _parameters;
    }
}
