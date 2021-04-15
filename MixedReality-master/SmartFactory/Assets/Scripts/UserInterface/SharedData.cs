using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedData : MonoBehaviour
{
    private Building selected;
    private Building parented;

    public Building Selected { get { return selected; } set { selected = value; } }
    public Building Parented { get { return parented; } set { parented = value; } }

}
