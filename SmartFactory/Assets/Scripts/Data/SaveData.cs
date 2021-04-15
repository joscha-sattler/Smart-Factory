using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuildingSaveData
{
    public int prefabId;
    public Coordinates coords;
    public float orientation;

    public BuildingSaveData(Building building)
    {
        prefabId = building.PrefabId;
        coords = building.Cell.Coords;
        orientation = building.transform.rotation.eulerAngles.y;
    }
}

[System.Serializable]
public struct ItemSaveData
{
    public int id;
    public int prefabId;
    public Coordinates coords;

    public ItemSaveData(Item item)
    {
        id = item.Id;
        prefabId = item.PrefabId;
        coords = new Coordinates(item.transform.position);
    }
}

[System.Serializable]
public struct CarrierSaveData
{
    public int id;
    public int prefabId;
    public Coordinates coords;

    public CarrierSaveData(Carrier carrier)
    {
        id = carrier.Id;
        prefabId = carrier.PrefabId;
        coords = new Coordinates(carrier.transform.position);
    }
}

[System.Serializable]
public class SaveData
{
    public List<BuildingSaveData> buildingSaves = new List<BuildingSaveData>();
    public List<ItemSaveData> itemSaves = new List<ItemSaveData>();
    public List<CarrierSaveData> carrierSaves = new List<CarrierSaveData>();

    public SaveData(List<Building> buildings, List<Item> items, List<Carrier> carriers)
    {
        AddBuildings(buildings);
        AddItems(items);
        AddCarriers(carriers);
    }

    private void AddBuildings(List<Building> buildings)
    {
        foreach (Building building in buildings)
        {
            buildingSaves.Add(new BuildingSaveData(building));
        }
    }

    private void AddItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            itemSaves.Add(new ItemSaveData(item));
        }
    }

    private void AddCarriers(List<Carrier> carriers)
    {
        foreach (Carrier carrier in carriers)
        {
            carrierSaves.Add(new CarrierSaveData(carrier));
        }
    }
}
