using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Windows.Speech;

public class VoiceController : SharedDataController
{
    private EventSystem system;
    public string[] keywords; /* = new string[]{"Null", "Eins", "Zwei", "Drei", "Vier", "Fünf", "Sechs", "Auftrag erteilen", "Gebäude entfernen", "Gebäude bewegen", "Konfiguration laden", "Konfiguration speichern"}; */
    public ConfidenceLevel confidence = ConfidenceLevel.Low;

    public Text results;
    protected PhraseRecognizer recognizer;
    protected string word = "Null";

    // Start is called before the first frame update
    protected override void Start()
    {
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
            recognizer.Start();
        }

        base.Start();

        system = EventSystem.current;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        HandleVoiceInputs();
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;    
        results.text = word;
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }

    private void HandleVoiceInputs()
    {
        if (String.Equals(word, keywords[0]))
        {
            if (selected != null)
            {
                factory.PlaceCarrier(factory.CreateCarrier(0), selected);
            }
        }
        else if (String.Equals(word, keywords[1]) == true)
        {
            if (selected != null && selected is Storage)
            {
                factory.AddItem(factory.CreateItem(0), (Storage)selected);
                return;
            }
            DeleteParented();            
            SetParented(factory.CreateBuilding(0));
        }
        else if (String.Equals(word, keywords[2]) == true)
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(1));
        }
        else if (String.Equals(word, keywords[3]) == true)
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(2));
        }
        else if (String.Equals(word, keywords[4]) == true)
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(3));
        }
        else if (String.Equals(word, keywords[5]) == true)
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(4));
        }
        else if (String.Equals(word, keywords[6]) == true)
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(5));
        }
        else if (String.Equals(word, keywords[7]) == true)
        {
            factory.CreateAssignment("Snowman", 1);
        }
        else if (String.Equals(word, keywords[8]) == true)
        {
            DeleteParented();
        }
        else if (String.Equals(word, keywords[9]) == true)
        {
            factory.LoadConfiguration("default.sav");
        }
        else if (String.Equals(word, keywords[10]) == true)
        {
            if (selected == null) { return; }

            DeleteParented();
            SetParented(factory.PickUpBuilding(selected));
        }
        else if (String.Equals(word, keywords[11]) == true)
        {
            factory.SaveConfiguration("default.sav");
        }
    }    
}
