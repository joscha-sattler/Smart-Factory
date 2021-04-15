using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkhouse : Station
{
    [SerializeField]
    private Vector3[] slots = new Vector3[6];
    private Carrier[] storedCarriers;

    protected override void Start()
    {
        base.Start();
        storedCarriers = new Carrier[slots.Length];
    }    

    protected override void CarryOutInstruction(Carrier carrier)
    {
        TakeOutCarrier(carrier);
    }

    private bool TakeOutCarrier(Carrier carrier)
    {
        for (int i=0; i<slots.Length; i++)
        {
            if (storedCarriers[i] == null)
            {                
                storedCarriers[i] = carrier;
                carrier.transform.rotation = transform.rotation;
                PositionCarrier(carrier, i);

                carrier.Instructions.RemoveAt(0);
                carriers.Remove(carrier);
                return true;
            }
        }
        return false;
    }

    public bool BringInCarrier(Carrier newCarrier)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (storedCarriers[i] == newCarrier)
            {
                carriers.Add(newCarrier);
                newCarrier.transform.position = transform.position + new Vector3(0, height, 0);
                storedCarriers[i] = null;
            }
        }
        return false;
    }

    private void PositionCarrier(Carrier carrier, int i)
    {
        Vector3 pos = transform.position;
        pos += transform.right * slots[i].x;
        pos += transform.up * slots[i].y;
        pos += transform.forward * slots[i].z;

        carrier.transform.position = pos;
        carrier.transform.SetParent(transform);
    }
}
