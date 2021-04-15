using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierServices
{
    private ICarrierManager carrierManager;

    public CarrierServices()
    {
        carrierManager = CarrierManager.Instance;
    }

    public Carrier CreateCarrier(int prefabId)
    {
        return carrierManager.CreateCarrier(prefabId);
    }

    public Carrier CreateCarrier(int prefabId, int id)
    {
        return carrierManager.CreateCarrier(prefabId, id);
    }

    public void PlaceCarrier(Carrier carrier, Building building)
    {
        carrier.Parent = building;
        building.Carriers.Add(carrier);
        carrier.transform.position = building.transform.position + new Vector3(0, building.Height, 0);
    }

    public List<Carrier> GetAllCarriers()
    {
        return carrierManager.GetAllCarriers();
    }
}
