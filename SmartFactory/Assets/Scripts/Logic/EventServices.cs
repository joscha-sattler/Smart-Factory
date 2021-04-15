using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventServices
{
    private static EventServices instance = new EventServices();
    private GridServices gridServices = new GridServices();    
    private ItemServices itemServices = new ItemServices();
    private InstructionServices instructionServices = new InstructionServices();

    //wird genutzt, um Clicks im Menu zu vertonen
    public UnityEvent onButtonClick = new UnityEvent();
    private SoundServices soundServices;
    public static EventServices Instance { get { return instance; } }

    static EventServices() {
     }

    public void OnCarrierPassed(Carrier carrier)
    {
        carrier.Parent.Carriers.Remove(carrier);
        carrier.ResetTransformStatus();

        Building newParent = gridServices.GetBuildingAt(carrier.transform.position);

        if (newParent == null) { return; }

        carrier.Parent = newParent;
        newParent.Carriers.Add(carrier);
    }

    public void OnInstructionRequested(Carrier carrier)
    {
        instructionServices.CarryOutInstructions(carrier);
    }

    public void OnProductionRequested(Producer producer)
    {
        foreach (string itemType in producer.ProductionQueue)
        {
            Item product = itemServices.CreateItem(itemType);
            producer.CarryOutProduction(product);
           
            soundServices.PlayProductionSound();
        }

        producer.ProductionQueue.Clear();
    }

    
    public void SetSoundServices(SoundServices SoundServices){
        this.soundServices = SoundServices;
    }

    public void OnBuildingPlaced(int id)
    {
        soundServices.NewBuilding(id);
    }
}
