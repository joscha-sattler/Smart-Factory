using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyboardController : SharedDataController
{
    private Coordinates cellCoords = new Coordinates();
    private EventSystem system;

    protected override void Start()
    {
        base.Start();

        system = EventSystem.current;
    }

    protected override void Update()
    {
        base.Update();

        // ignores keyboard inputs when InputField is selected
        if (!CheckInputFieldSelected())
        {
            HandleKeyInputs();
        }
    }

    private void HandleKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (selected != null)
            {
                factory.PlaceCarrier(factory.CreateCarrier(0), selected);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (selected != null && selected is Storage)
            {
                factory.AddItem(factory.CreateItem(0), (Storage)selected);
                return;
            }
            DeleteParented();            
            SetParented(factory.CreateBuilding(0));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(2));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(3));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(4));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DeleteParented();
            SetParented(factory.CreateBuilding(5));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            factory.CreateAssignment("Snowman", 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteParented();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            factory.LoadConfiguration("default.sav");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (selected == null) { return; }

            DeleteParented();
            SetParented(factory.PickUpBuilding(selected));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            factory.RotateBuilding(parented);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            factory.SaveConfiguration("default.sav");
        }
    }

    private void PlaceBuilding()
    {
        factory.PlaceBuilding(parented, cellCoords);
        parented = null;
    }

    private bool CheckInputFieldSelected()
    {
        if (system == null)
        {
            system = EventSystem.current;
            if (system == null) return false;
        }

        GameObject currentObject = system.currentSelectedGameObject;
        if (currentObject != null)
        {
            InputField inputField = currentObject.GetComponent<InputField>();
            if (inputField != null)
            {
                return true;
            }
        }return false;
    }
}
