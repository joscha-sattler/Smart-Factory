using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class closeProgram : MonoBehaviour
{
    public void programmBeenden() {
        UnityEngine.Debug.Log("Meldung: Smart Factory wurde beendet!");
        Application.Quit();
    }
}
