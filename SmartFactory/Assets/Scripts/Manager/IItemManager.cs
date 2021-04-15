using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemManager
{
    Item CreateItem(int prefabId);
    Item CreateItem(int prefabId, int id);
    Item CreateItem(string type);
    List<Item> GetAllItems();
}
