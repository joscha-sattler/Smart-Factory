using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : GenericFactory<Building>, IBuildingManager
{
    // Singleton shared instance
    private static BuildingManager instance;

    private List<Building> buildings = new List<Building>();

    private List<Station> storages = new List<Station>();
    private List<Station> assemblers = new List<Station>();
    private List<Station> parkhouses = new List<Station>();
    private List<Station> stations = new List<Station>();
    //diese fuer sound

    public static BuildingManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public Building CreateBuilding(int index)
    {
        Building building = GetNewInstance(index);
        buildings.Add(building);

        if (building is Storage)
        {
            storages.Add((Storage)building);
        }
        else if (building is Assembler)
        {
            assemblers.Add((Assembler)building);
        }
        else if (building is Parkhouse)
        {            
            parkhouses.Add((Parkhouse)building);
        }
        if (building is Station)
        {
            stations.Add((Station)building);
        }
        return building;
    }

    public void DeleteBuilding(Building building)
    {
        buildings.Remove(building);

        if (building is Storage)
        {
            storages.Remove((Storage)building);
        }
        else if (building is Assembler)
        {
            assemblers.Remove((Assembler)building);
        }
        else if (building is Parkhouse)
        {            
            parkhouses.Remove((Parkhouse)building);
        }
        if (building is Station)
        {
            stations.Remove((Station)building);
        }

        Destroy(building.gameObject);
    }

    public List<Station> FindSuitableProviders(string stationType, string item)
    {
        List<Station> workstations;

        switch (stationType)
        {
            case "Storage":
                workstations = storages;
                break;
            case "Assembler":
                workstations = assemblers;
                break;
            default:
                return null;
        }

        List<Station> suitables = new List<Station>();

        foreach (Station workstation in workstations)
        {
            if (((Workstation)workstation).CanProvide(item))
            {
                suitables.Add(workstation);
            }
        }

        return suitables;
    }

    public List<Station> FindSuitableStorages(string item)
    {
        return storages;
    }

    public List<Station> GetStations()
    {
        return stations;
    }

    public List<Station> GetParkhouses()
    {
        return parkhouses;
    }

    public List<Station> GetAssemblers()
    {
        return assemblers;
    }

    public List<Building> GetAllBuildings()
    {
        return buildings;
    }
}
