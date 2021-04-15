using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundServices : MonoBehaviour
{
    public OSCSendReceive oscSender;

    private List<Carrier> carriers = new List<Carrier>();

    public SoundServices(OSCSendReceive oscsr){
        this.oscSender = oscsr;
    }

    private void Awake(){
        //der eventservice besitzt einen eventlistener, der auf das betätigen von buttons reagiert
        EventServices.Instance.onButtonClick.AddListener(onButtonClick);
    }

    public void onButtonClick(){
        PlaySound("Menu ", 1);
    }

    public void NewBuilding(float index)
    {
        PlaySound("Building ", index);
    }

    public void PlayProductionSound(){
        PlaySound("Work ", 1);
    }

    private void PlaySound(string msg, float index){
        oscSender.PlaySoundOSC("/" + msg + index);    
    }
}