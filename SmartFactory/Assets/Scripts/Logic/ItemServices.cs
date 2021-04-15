using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemServices
{
    private IItemManager itemManager;

    public ItemServices()
    {
        itemManager = ItemManager.Instance;
    }

    public Item CreateItem(int prefabId)
    {
        return itemManager.CreateItem(prefabId);
    }

    public Item CreateItem(int prefabId, int id)
    {
        return itemManager.CreateItem(prefabId, id);
    }

    public Item CreateItem(string type)
    {
        return itemManager.CreateItem(type);
    }

    public void AddItem(Item item, Storage storage)
    {
        storage.AddItem(item);
    }

    public List<Item> GetAllItems()
    {
        return itemManager.GetAllItems();
    }
}
