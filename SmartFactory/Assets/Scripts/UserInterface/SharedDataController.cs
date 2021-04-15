using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SharedDataController : MonoBehaviour
{
    [SerializeField]
    private SharedData sharedData = null;

    protected ISmartFactory factory;

    protected Building selected;
    protected Building parented;

    protected virtual void Start()
    {
        factory = SmartFactory.Instance;
    }

    protected virtual void Update()
    {
        UpdateParented();
        UpdateSelected();
    }

    protected void UpdateSelected()
    {
        selected = sharedData.Selected;
    }

    protected void UpdateParented()
    {
        parented = sharedData.Parented;
    }

    protected void SetSelected(Building building)
    {
        selected = building;
        sharedData.Selected = building;
    }

    protected void Unselect()
    {
        selected = null;
        sharedData.Selected = null;
    }

    protected void SetParented(Building building)
    {
        parented = building;
        sharedData.Parented = building;
    }

    protected void Unparent()
    {
        parented = null;
        sharedData.Parented = null;
    }

    protected void DeleteParented()
    {
        if (parented != null)
        {
            factory.DeleteBuilding(parented);
            Unparent();
        }
    }
}
