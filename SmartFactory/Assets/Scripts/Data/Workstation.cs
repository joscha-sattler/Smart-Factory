using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Workstation : Station
{
    [SerializeField]
    protected List<string> provisions;

    protected List<Item> items = new List<Item>();    

    protected virtual void TakeOutItems(Carrier carrier)
    {
        int count = carrier.Instructions[0].Output.Count;

        for (int i = 0; i < count; i++)
        {
            string itemType = carrier.Instructions[0].PopOutput();
            AddItem(carrier.TakeItem(itemType));
        }
    }

    public virtual void AddItem(Item item)
    {
        items.Add(item);
    }

    protected virtual Item GetItem(string type)
    {
        foreach (Item item in items) {
            if (item.Type == type)
            {
                return item;
            }
        }

        return null;        
    }

    public virtual bool CanProvide(string itemType)
    {
        return provisions.Contains(itemType);
    }
}
