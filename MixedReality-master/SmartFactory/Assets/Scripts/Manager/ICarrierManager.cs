using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarrierManager
{
    Carrier CreateCarrier(int prefabId);
    Carrier CreateCarrier(int prefabId, int id);
    Carrier GetFreeCarrier();
    List<Carrier> GetAllCarriers();
}
