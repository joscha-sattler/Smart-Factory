using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionServices
{
    private IManualManager manualManager = ManualManager.Instance;
    private NavigationServices navigationServices = new NavigationServices();

    public List<Instruction> DetermineInstructions(Carrier carrier, Assignment assignment)
    {
        List<Instruction> instructions = new List<Instruction>();
        Coordinates start = carrier.Parent.Cell.Coords;
        
        AddProductionInstructions(ref instructions, ref start, assignment.ProductType);
        AddStorageInstructions(ref instructions, ref start, assignment.ProductType);        

        return instructions;
    }

    public void AddProductionInstructions(ref List<Instruction> instructions, ref Coordinates start, string itemType)
    {
        Manual manual = manualManager.GetManual(itemType);

        AddResourceInstructions(ref instructions, ref start, new List<string>(manual.Items));

        List<Coordinates> path;

        path = navigationServices.GetPath(start, "Assembler", new List<string> {itemType }, null);
        if (path == null)
        {
            Debug.LogError("Workstation not available!");
            return;
        }

        Instruction instruction = new Instruction(path, "Assembler");
        instruction.Output = new List<string>(manual.Items);
        instruction.AddInput(itemType);
        instructions.Add(instruction);

        start = path[path.Count - 1];
    }

    public void AddResourceInstructions(ref List<Instruction> instructions, ref Coordinates start, List<string> itemTypes)
    {
        List<Coordinates> path;
        path = navigationServices.GetPath(start, "Storage", itemTypes, null);

        if (path == null)
        {
            Debug.LogError("Resource not available!");
            return;
        }

        Instruction instruction = new Instruction(path, "Storage");
        instruction.Input = itemTypes;
        instructions.Add(instruction);

        start = path[path.Count - 1];
    }

    public void AddStorageInstructions(ref List<Instruction> instructions, ref Coordinates start, string itemType)
    {
        List<Coordinates> path;
        path = navigationServices.GetPath(start, "Storage", null, new List<string> { itemType });
        if (path == null)
        {
            Debug.LogError("No Storage available!");
            return;
        }

        Instruction instruction = new Instruction(path, "Storage");
        instruction.AddOutput(itemType);
        instructions.Add(instruction);

        start = path[path.Count - 1];
    }

    public bool CarryOutInstructions(Carrier carrier)
    {
        if (carrier.Instructions.Count == 0)
        {
            carrier.IsFree = true;
            TakeOutCarrier(carrier);
        }

        List<Coordinates> path = carrier.Instructions[0].Path;

        // index of cell already reached on path
        int startIdx = path.FindIndex(c => c == carrier.Parent.Cell.Coords);

        // end of path not reached yet
        if (startIdx != path.Count - 1)
        {            
            navigationServices.SetConveyorPath(path, startIdx);
        }

        return true;
    }

    private void TakeOutCarrier(Carrier carrier)
    {
        Coordinates start = carrier.Parent.Cell.Coords;
        List<Coordinates> path = navigationServices.GetPathToParkhouse(start);

        Instruction instruction = new Instruction(path, "Parkhouse");
        carrier.AddInstruction(instruction);
    }
}
