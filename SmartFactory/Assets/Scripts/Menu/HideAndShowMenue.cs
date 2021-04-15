using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideAndShowMenue : MonoBehaviour
{
    // Attribute / Instanzen

    public GameObject Panel_oder_Canvas;

    // Methoden

    public void ToggleMenue()
    {
        if (Panel_oder_Canvas != null)
        {
            bool menueShown = Panel_oder_Canvas.activeSelf;

            Panel_oder_Canvas.SetActive(!menueShown);
        }
    }


}
