using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierManager : GenericFactory<Carrier>, ICarrierManager
{
    // Singleton shared instance
    private static CarrierManager _instance;

    private static int nextId;
    private List<Carrier> carriers = new List<Carrier>();

    public static CarrierManager Instance { get { return _instance; } }

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

    public Carrier CreateCarrier(int prefabId)
    {
        CheckNextId();
        Carrier carrier = CreateCarrier(prefabId, nextId);
        return carrier;
    }
    

    public Carrier CreateCarrier(int prefabId, int id)
    {
        Carrier carrier = GetNewInstance(prefabId);
        carrier.PrefabId = prefabId;
        carrier.Id = id;

        carriers.Add(carrier);        
        return carrier;
    }

    public Carrier GetFreeCarrier()
    {
        foreach (Carrier carrier in carriers)
        {
            if (carrier.IsFree)
            {
                return carrier;
            }
        }
        return null;
    }

    public List<Carrier> GetAllCarriers()
    {
        return carriers;
    }

    private void CheckNextId()
    {
        while (IdAssigned())
        {
            nextId++;
        }
    }

    private bool IdAssigned()
    {
        foreach (Carrier carrier in carriers)
        {
            if (carrier.Id == nextId)
            {
                return true;
            }
        }

        return false;
    }
}
