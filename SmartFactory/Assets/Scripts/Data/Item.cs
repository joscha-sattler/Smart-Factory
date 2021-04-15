using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string type = "";

    private int id;
    private int prefabId;

    public string Type { get { return type; } }
    public int Id { get { return id; } set { id = value; } }
    public int PrefabId { get { return prefabId; } set { prefabId = value; } }

}
