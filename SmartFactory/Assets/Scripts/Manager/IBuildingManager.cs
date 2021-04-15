using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingManager
{
    Building CreateBuilding(int index);
    void DeleteBuilding(Building building);
    List<Station> FindSuitableProviders(string stationType, string item);
    List<Station> FindSuitableStorages(string item);
    List<Station> GetStations();
    List<Station> GetParkhouses();
    List<Station> GetAssemblers();
    List<Building> GetAllBuildings();
}
