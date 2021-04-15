using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceServices
{
    BuildingServices buildingServices = new BuildingServices();
    GridServices gridServices = new GridServices();
    ItemServices itemServices = new ItemServices();
    CarrierServices carrierServices = new CarrierServices();

    public void SaveConfiguration(string fileName)
    {
        List<Building> buildings = buildingServices.GetAllBuildings();
        List<Item> items = itemServices.GetAllItems();
        List<Carrier> carriers = carrierServices.GetAllCarriers();

        SaveData saveData = new SaveData(buildings, items, carriers);

        FileManager.SaveFile(fileName, saveData);
    }

    public void LoadConfiguration(string fileName)
    {
        // sets static variable for loading after scene restart
        FileManager.toLoad = fileName;

        SceneManager.LoadScene("SampleScene");
    }

    public void InitScene()
    {
        if (FileManager.toLoad == null)
        {
            return;
        }

        SaveData saveData = FileManager.LoadFile();

        LoadBuildings(saveData.buildingSaves);
        LoadItems(saveData.itemSaves);
        LoadCarriers(saveData.carrierSaves);
    }

    public string GetFileName()
    {
        return FileManager.GetFileName();
    }

    private void LoadBuildings(List<BuildingSaveData> buildingSaves)
    {
        foreach (BuildingSaveData buildingSave in buildingSaves)
        {
            Building building = buildingServices.CreateBuilding(buildingSave.prefabId);

            buildingServices.SetBuildingOrientation(building, buildingSave.orientation);
            gridServices.PlaceBuilding(building, buildingSave.coords);
        }
    }

    private void LoadItems(List<ItemSaveData> itemSaves)
    {
        foreach (ItemSaveData itemSave in itemSaves)
        {
            Item item = itemServices.CreateItem(itemSave.prefabId, itemSave.id);
            Building building = gridServices.GetCell(itemSave.coords).Building;

            if (building is Storage)
            {
                itemServices.AddItem(item, (Storage)building);
            }
        }
    }

    private void LoadCarriers(List<CarrierSaveData> carrierSaves)
    {
        foreach (CarrierSaveData carrierSave in carrierSaves)
        {
            Carrier carrier = carrierServices.CreateCarrier(carrierSave.prefabId, carrierSave.id);
            Building building = gridServices.GetCell(carrierSave.coords).Building;

            carrierServices.PlaceCarrier(carrier, building);
        }
    }
}
