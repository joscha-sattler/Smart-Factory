using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartFactory : MonoBehaviour, ISmartFactory
{
    // Singleton shared instance
    private static SmartFactory _instance;

    private GridServices gridServices;    
    private ItemServices itemServices;
    private CarrierServices carrierServices;
    private BuildingServices buildingServices;
    private AssignmentServices assignmentServices;
    private PersistenceServices persistenceServices;
    private SoundServices soundServices;
    private OSCSendReceive oscsr;

    public static SmartFactory Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        // services created at runtime as managers are also only available at runtime
        gridServices = new GridServices();
        itemServices = new ItemServices();
        carrierServices = new CarrierServices();
        buildingServices = new BuildingServices();
        assignmentServices = new AssignmentServices();
        persistenceServices = new PersistenceServices();
        oscsr = GameObject.FindWithTag("OSC").GetComponent<OSCSendReceive>();
        soundServices = new SoundServices(oscsr);
        EventServices.Instance.SetSoundServices(soundServices);

        persistenceServices.InitScene();
    }
    
    public Building CreateBuilding(int index)
    {
        return buildingServices.CreateBuilding(index);
    }

    public void RotateBuilding(Building building)
    {
        buildingServices.RotateBuilding(building);
    }

    public void PlaceBuilding(Building building, Coordinates coords)
    {
        gridServices.PlaceBuilding(building, coords);        
    }

    public Building SelectBuilding(Coordinates coords)
    {
        GridCell cell = gridServices.GetCell(coords);
        return cell.Building;
    }

    public Building PickUpBuilding(Building building)
    {
        return gridServices.PickUpBuilding(building);
    }

    public void DeleteBuilding(Building building)
    {
        buildingServices.DeleteBuilding(building);
    }

    public Carrier CreateCarrier(int index)
    {
        return carrierServices.CreateCarrier(index);
    }

    public void PlaceCarrier(Carrier carrier, Building parent)
    {
        carrierServices.PlaceCarrier(carrier, parent);
    }

    public Item CreateItem(int index)
    {
        return itemServices.CreateItem(index);
    }

    public void AddItem(Item item, Storage storage)
    {
        itemServices.AddItem(item, storage);
    }

    public void CreateAssignment(string type, int amount)
    {
        assignmentServices.CreateAssignment(type, amount);
    }

    public void SaveConfiguration(string fileName)
    {
        persistenceServices.SaveConfiguration(fileName);
    }

    public void LoadConfiguration(string fileName)
    {
        persistenceServices.LoadConfiguration(fileName);
    }

    public string GetFileName()
    {
        return persistenceServices.GetFileName();
    }
}
