using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : GenericFactory<Item>, IItemManager
{
    // Singleton shared instance
    private static ItemManager _instance;

    private static int nextId = 0;
    private List<Item> items = new List<Item>();

    public static ItemManager Instance { get { return _instance; } }

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

    public Item CreateItem(int prefabId)
    {
        CheckNextId();
        Item item = CreateItem(prefabId, nextId);
        return item;
    }

    public Item CreateItem(int prefabId, int id)
    {
        Item item = GetNewInstance(prefabId);
        item.PrefabId = prefabId;
        item.Id = id;
        items.Add(item);        

        return item;
    }

    public Item CreateItem(string type)
    {
        int index = GetIndex(type);        
        return CreateItem(index);
    }

    private int GetIndex(string type)
    {
        for (int i=0; i<prefabs.Count; i++)
        {
            if (prefabs[i].Type == type)
            {
                return i;
            }
        }

        Debug.LogError("Could not find a prefab for: " + type);
        return -1;
    }

    public List<Item> GetAllItems()
    {
        return items;
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
        foreach (Item item in items)
        {
            if (item.Id == nextId)
            {
                return true;
            }
        }

        return false;
    }
}
