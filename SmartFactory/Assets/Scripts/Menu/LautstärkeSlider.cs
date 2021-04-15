using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LautstärkeSlider : MonoBehaviour
{
    //Attribute Slieder

    public Slider einSlider;

    Text volume;




    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Text>();
        volume.text = " " + einSlider.value + "%";
        einSlider.wholeNumbers = true;
    }

    // Methoden Slider

    public void updateText(float value)
    {
        value = einSlider.value+80;
        volume.text = " " + value + "%";
        Debug.Log(einSlider.value);
    }

}

