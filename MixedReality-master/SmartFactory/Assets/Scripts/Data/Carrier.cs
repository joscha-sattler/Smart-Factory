using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    private int id;
    private int prefabId;
    private Item[] slots;    

    private Building parent;    
    private List<Instruction> instructions = new List<Instruction>();

    private bool isFree = true;
    private bool isCentered = false;
    private bool isOrientated = false;

    [SerializeField]
    private List<Vector3> slotPositions = new List<Vector3>();
    
    public int Id { get { return id; } set { id = value; } }
    public int PrefabId { get { return prefabId; } set { prefabId = value; } }
    public Building Parent { get { return parent; } set { parent = value; } }
    public List<Instruction> Instructions { get { return instructions; } set { instructions = value; } }
    public bool IsFree { get { return isFree; } set { isFree = value; } }
    public bool IsCentered { get { return isCentered; } set { isCentered = value; } }
    public bool IsOrientated { get { return isOrientated; } set { isOrientated = value; } }

    // Start is called before the first frame update
    void Start()
    {
        slots = new Item[slotPositions.Count];
    }

    public void Fill(Item item)
    {
        for (int i=0; i<slots.Length; i++)
        {
            if (slots[i] == null)
            { 
                slots[i] = item;
                item.transform.position = transform.position + GetGlobalSlotPosition(i); 
                item.transform.SetParent(transform);
                return;
            }
        }
    }

    public Item TakeItem(string type)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].Type == type)
            {
                Item item = slots[i];
                slots[i] = null;
                return item;
            }
        }
        return null;
    }

    public void AddInstruction(Instruction newInstruction)
    {
        instructions.Add(newInstruction);
    }

    public int CountItems()
    {
        int count = 0;

        foreach (Item slot in slots)
        {
            if (slot != null)
            {
                count++;
            }
        }
        return count;
    }

    private Vector3 GetGlobalSlotPosition(int index)
    {
        Vector3 offset = slotPositions[index];
        Vector3 vec = transform.right * offset.x;
        vec += transform.up * offset.y;
        vec += transform.forward * offset.z;

        return vec;
    }

    public bool IsAtTargetStation()
    {
        if (!hasInstructions())
        {
            if (parent is Parkhouse)
            {
                instructions.Add(new Instruction(null, "Parkhouse"));
                return true;
            }
            return false;
        }

        List<Coordinates> path = instructions[0].Path;
        return parent.Cell.Coords == path[path.Count - 1];
    }

    public void ResetTransformStatus()
    {
        isCentered = false;
        isOrientated = false;
    }

    private bool hasInstructions()
    {
        return (instructions.Count != 0);
    }
}
