using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentManager : IAssignmentManager
{
    private static readonly AssignmentManager instance = new AssignmentManager();

    private List<Assignment> assignments = new List<Assignment>();

    public static AssignmentManager Instance { get { return instance; } }

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static AssignmentManager()
    {
    }

    public Assignment CreateAssignment(string type, int amount)
    {
        Assignment newAssignment = new Assignment(type, amount);
        assignments.Add(newAssignment);

        return newAssignment;
    }

    public void AddCarrierToAssignment(Carrier carrier, Assignment assignment)
    {
        assignments.Find(a => a.ID == assignment.ID).Assign(carrier);
    }

    public List<Assignment> GetAssignments()
    {
        return assignments;
    }
}