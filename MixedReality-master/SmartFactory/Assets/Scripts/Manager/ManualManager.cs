using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualManager : IManualManager
{
    private static readonly ManualManager instance = new ManualManager();
    private static Dictionary<string, Manual> manuals = new Dictionary<string, Manual>();

    public static ManualManager Instance { get { return instance; } }

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static ManualManager()
    {
        // manual for testing
        List<string> items = new List<string>();
        items.Add("Sphere");
        items.Add("Sphere");
        manuals.Add("Snowman", new Manual("Assembler", items, ""));
    }

    public Manual GetManual(string product)
    {
        return manuals[product];
    }
}
