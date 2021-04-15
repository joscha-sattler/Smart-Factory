using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISmartFactory
{
    Building CreateBuilding(int index);
    void RotateBuilding(Building building);
    void PlaceBuilding(Building building, Coordinates coords);
    Building SelectBuilding(Coordinates coords);
    Building PickUpBuilding(Building building);
    void DeleteBuilding(Building building);
    Carrier CreateCarrier(int index);
    void PlaceCarrier(Carrier carrier, Building parent);
    Item CreateItem(int index);
    void AddItem(Item item, Storage storage);
    void CreateAssignment(string type, int amount);
    void SaveConfiguration(string fileName);
    void LoadConfiguration(string fileName);
    string GetFileName();
}
