using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : Workstation
{
    private List<string> productionQueue = new List<string>();

    public ProductionRequestEvent OnProductionRequest = new ProductionRequestEvent();

    public List<string> ProductionQueue { get { return productionQueue; } }

    protected override void Start()
    {
        base.Start();
        OnProductionRequest.AddListener(EventServices.Instance.OnProductionRequested);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnProductionRequest.RemoveListener(EventServices.Instance.OnProductionRequested);
    }

    protected override void CarryOutInstruction(Carrier carrier)
    {
        string toProduce = carrier.Instructions[0].PopInput();

        if (toProduce == null)
        {
            carrier.Instructions.RemoveAt(0);
            return;
        }

        productionQueue.Add(toProduce);

        TakeOutItems(carrier);
        OnProductionRequest.Invoke(this);
    }

    public void CarryOutProduction(Item product)
    {
        foreach (Item item in items)
        {
            Destroy(item.gameObject);
        }

        items = new List<Item>();
        carriers[0].Fill(product);        
    }    
}
