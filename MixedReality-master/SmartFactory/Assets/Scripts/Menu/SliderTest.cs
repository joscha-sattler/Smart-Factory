using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour
{
    //Attributes

    public Slider meinSlider;

    Text zaehler;

    // Start is called before the first frame update
    void Start()
    {
        zaehler = GetComponent<Text>();
        zaehler.text = "Anzahl:" + " " + meinSlider.value;
        meinSlider.wholeNumbers = true;
    }


    public void updateText(float value)
    {
        value = meinSlider.value;
        zaehler.text = "Anzahl:" + " " + value;
        Debug.Log(meinSlider.value);
    }

}
