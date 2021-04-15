using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : Building
{
    public InstructionRequestEvent OnInstructionRequested = new InstructionRequestEvent();

    protected override void Start()
    {
        base.Start();
        OnInstructionRequested.AddListener(EventServices.Instance.OnInstructionRequested);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnInstructionRequested.RemoveListener(EventServices.Instance.OnInstructionRequested);
    }

    override protected void ProcessCarrier(Carrier carrier)
    {
        if (!carrier.IsCentered)
        {
            CenterCarrier(carrier);
        }
        else if (carrier.IsAtTargetStation())
        {
            CarryOutInstruction(carrier);
            OnInstructionRequested.Invoke(carrier);
            return;
        }
        else if (!carrier.IsOrientated)
        {
            OrientateCarrier(carrier);
        } 
        else
        {
            MoveCarrier(carrier, GetMovVec(carrier));
        }
    }

    protected void CenterCarrier(Carrier carrier)
    {
        MoveCarrier(carrier, GetCenterVec(carrier));

        if ((GetCenterPos() - carrier.transform.position).magnitude < 0.01f)
        {
            carrier.IsCentered = true;
            OnInstructionRequested.Invoke(carrier);
        }
    }

    protected virtual void CarryOutInstruction(Carrier carrier) { }

    protected void OrientateCarrier(Carrier carrier)
    {
        float angle = Vector3.Angle(carrier.gameObject.transform.forward, GetMovVec(carrier));

        // change direction of rotation if needed
        if (Vector3.Cross(carrier.gameObject.transform.forward, GetMovVec(carrier)).y < 0)
        {            
            angle = -angle;
        }

        if (Math.Abs(angle) < 0.1)
        {
            carrier.IsOrientated = true;
            return;
        }

        carrier.gameObject.transform.Rotate(0, angle / 20, 0);
    }
}
