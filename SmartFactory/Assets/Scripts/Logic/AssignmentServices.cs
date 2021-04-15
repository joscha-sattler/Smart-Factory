using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentServices
{
    IAssignmentManager assignmentManager = AssignmentManager.Instance;
    ICarrierManager carrierManager = CarrierManager.Instance;
    IManualManager manualManager = ManualManager.Instance;
    IItemManager itemManager = ItemManager.Instance;
    InstructionServices instructionServices = new InstructionServices();

    public Assignment CreateAssignment(string type, int amount)
    {
        Assignment assignment = assignmentManager.CreateAssignment(type, amount);
        Carrier carrier = carrierManager.GetFreeCarrier();

        if (carrier == null)
        {
            Debug.LogError("No carrier available!");
            return assignment;
        }

        if (SetInstructions(carrier, assignment))
        {
            carrier.IsFree = false;
            instructionServices.CarryOutInstructions(carrier);
        }

        return assignment;
    }

    private bool SetInstructions(Carrier carrier, Assignment assignment)
    {
        List<Instruction> instructions = instructionServices.DetermineInstructions(carrier, assignment);

        if (instructions == null)
        {
            return false;
        }

        carrier.Instructions = instructions;
        
        if (carrier.Parent is Parkhouse)
        {
            ((Parkhouse)carrier.Parent).BringInCarrier(carrier);
        }

        return true;
    }    
}
