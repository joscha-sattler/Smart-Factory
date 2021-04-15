using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : SharedDataController
{
    [SerializeField]
    private GameObject cursorPrefab = null;
    [SerializeField]
    private GameObject selectorPrefab = null;
    private GameObject cursor;
    private GameObject selector;

    private Coordinates cellCoords;

    protected override void Start()
    {
        base.Start();

        cursor = Instantiate(cursorPrefab);
        selector = Instantiate(selectorPrefab);
        selector.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (!mouseOverGUI())
        {
            UpdateCursor();
            HandleMouseInputs();
        }
    }

    private void UpdateCursor()
    {
        cellCoords = new Coordinates(GetFloorPosition());
        cursor.transform.position = cellCoords.ToWorldPosition() + new Vector3(0, 0.01f, 0);

        if (parented == null) { return; }
        parented.transform.position = cellCoords.ToWorldPosition();
    }

    private void HandleMouseInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (parented != null)
            {
                PlaceBuilding();
            }
            else
            {
                SetSelected(factory.SelectBuilding(cellCoords));
                if (selected != null)
                {
                    selector.transform.position = cellCoords.ToWorldPosition();
                    selector.SetActive(true);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DeleteParented();
            Unselect();
            selector.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            factory.RotateBuilding(parented);
        }
    }

    private void PlaceBuilding()
    {
        try
        {
            factory.PlaceBuilding(parented, cellCoords);
            Unparent();
        }
        catch (CellOccupiedException e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private Vector3 GetFloorPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return new Vector3();
    }

    private bool mouseOverGUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
