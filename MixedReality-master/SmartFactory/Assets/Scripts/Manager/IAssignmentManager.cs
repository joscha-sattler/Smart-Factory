using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssignmentManager
{
    Assignment CreateAssignment(string type, int amount);
    void AddCarrierToAssignment(Carrier carrier, Assignment assignment);
    List<Assignment> GetAssignments();
}
