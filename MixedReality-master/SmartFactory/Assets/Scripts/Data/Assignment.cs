using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment
{
    private static int nextID = 0;

    private int id;
    private string productType;
    private int amount;

    private List<Carrier> carriers;

    public int ID { get { return id; } }
    public string ProductType { get { return productType; } set { productType = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public List<Carrier> Carriers { get { return carriers; } }

    public Assignment(string _productType, int _amount)
    {
        id = nextID++;
        productType = _productType;
        amount = _amount;

        carriers = new List<Carrier>();
    }

    public void Assign(Carrier carrier)
    {
        carriers.Add(carrier);
    }
}
