using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Workstation
{
    private Item[,] stock = new Item[9,7];
    [SerializeField]
    private Vector3[] corners = new Vector3[2];

    public override void AddItem(Item item)
    {
        if (!InsertItem(item))
        {
            Debug.LogError("Storage full!");
            return;
        }        
        provisions.Add(item.Type);        
    }

    private bool InsertItem(Item item)
    {
        for (int i=0; i<stock.GetLength(0); i++)
        {
            for (int j = 0; j < stock.GetLength(1); j++)
            {
                if (stock[i,j] != null)
                {
                    continue;
                }

                stock[i, j] = item;
                PositionItem(item, i, j);
                return true;
            }
        }

        return false;
    }

    public bool FillCarrier(Carrier carrier, string type)
    {
        Item item = GetItem(type);
        carrier.Fill(item);

        return true;
    }
    
    protected override Item GetItem(string type)
    {
        for (int i = stock.GetLength(0)-1; i >= 0; i--)
        {
            for (int j = stock.GetLength(1)-1; j >= 0; j--)
            {
                if (stock[i,j] == null)
                {
                    continue;
                }

                if (stock[i,j].Type == type)
                {
                    Item item = stock[i, j];
                    stock[i,j] = null;
                    provisions.Remove(type);
                    return item;
                }
            }
        }

        return null;
    }

    protected override void CarryOutInstruction(Carrier carrier)
    {
        TakeOutItems(carrier);
        PutInItems(carrier);

        carrier.Instructions.RemoveAt(0);
    }

    private void PutInItems(Carrier carrier)
    {
        int count = carrier.Instructions[0].Input.Count;

        for (int i = 0; i < count; i++)
        {
            string item = carrier.Instructions[0].PopInput();
            // put back item if not added to carrier
            if (!FillCarrier(carrier, item))
            {
                carrier.Instructions[0].AddInput(item);
            }
        }
    }

    private void PositionItem(Item item, int i, int j)
    {
        Vector3 pos = transform.position + new Vector3(0, height, 0);
        pos += transform.right * (corners[0].x + 0.1f*i);
        pos += transform.up * corners[0].y;
        pos += transform.forward * (corners[0].z + 0.1f*j);

        item.transform.position = pos;
        item.transform.SetParent(transform);
    }
}
