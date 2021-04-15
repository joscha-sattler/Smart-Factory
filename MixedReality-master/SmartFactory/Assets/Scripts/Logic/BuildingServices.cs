using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingServices
{
    private EventServices eventServices = EventServices.Instance;

    private IBuildingManager buildingManager = BuildingManager.Instance;    

    public BuildingServices() {}

    public Building CreateBuilding(int index)
    {
        Building building = buildingManager.CreateBuilding(index);
        building.PrefabId = index;

        return building;
    }

    public void DeleteBuilding(Building building)
    {
        buildingManager.DeleteBuilding(building);
    }

    public void SetBuildingOrientation(Building building, float orientation)
    {
        building.transform.rotation = Quaternion.Euler(0.0f, orientation, 0.0f);
    }

    public void RotateBuilding(Building building)
    {
        building.transform.Rotate(0.0f, 90.0f, 0.0f);
    }

    public List<Station> GetStations()
    {
        return buildingManager.GetStations();
    }

    public List<Building> GetAllBuildings()
    {
        return buildingManager.GetAllBuildings();
    }
}
