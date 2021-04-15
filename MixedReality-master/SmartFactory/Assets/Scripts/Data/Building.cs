using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    private List<Vector3Int> routeDeltas = new List<Vector3Int>();

    private int prefabID;

    protected GridCell cell;
    protected List<GridCell> neighborCells = new List<GridCell>();
    protected List<Carrier> carriers = new List<Carrier>();

    protected int exitRoute;

    protected float height = 0.7f;
    protected float speed = 0.0025f;

    public CarrierPassedEvent OnCarrierPassed = new CarrierPassedEvent();

    public List<Vector3Int> RouteDeltas { get { return routeDeltas; } }
    public int PrefabId { get { return prefabID; } set { prefabID = value; } }
    public GridCell Cell { get { return cell; } set { cell = value; } }
    public List<GridCell> NeighborCells { get { return neighborCells; } set { neighborCells = value; } }
    public List<Carrier> Carriers { get { return carriers; } set { carriers = value; } }
    public float Height { get { return height; } }

    protected virtual void Start()
    {
        OnCarrierPassed.AddListener(EventServices.Instance.OnCarrierPassed);
    }

    void Update()
    {
        // list copy so carriers can be removed in loop
        foreach (Carrier carrier in new List<Carrier>(carriers))
        {
            ProcessCarrier(carrier);
        }
    }

    protected virtual void OnDestroy()
    {
        OnCarrierPassed.RemoveListener(EventServices.Instance.OnCarrierPassed);
    }

    virtual protected void ProcessCarrier(Carrier carrier)
    {
        MoveCarrier(carrier, GetMovVec(carrier));
    }

    protected Vector3 GetCenterPos()
    {
        return transform.position + new Vector3(0, height, 0);
    }

    protected Vector3 GetCenterVec(Carrier carrier)
    {
        Vector3 center = GetCenterPos();
        return (center - carrier.transform.position).normalized * speed;
    }

    protected Vector3 GetExitPos()
    {
        return neighborCells[exitRoute].Coords.ToWorldPosition() + new Vector3(0, height, 0);
    }

    protected Vector3 GetMovVec(Carrier carrier)
    {
        return (GetExitPos() - carrier.transform.position).normalized * speed;
    }

    protected void MoveCarrier(Carrier carrier, Vector3 movVec)
    {
        carrier.transform.position += movVec;

        if (new Coordinates(carrier.transform.position) != new Coordinates(transform.position))
        {
            OnCarrierPassed.Invoke(carrier);
        }
    }

    public void SetExit(Coordinates target)
    {
        if (neighborCells[exitRoute].Coords == target)
        {
            return;
        }
        for (int i=0; i<neighborCells.Count; i++)
        {
            if (neighborCells[i].Coords == target)
            {
                exitRoute = i;
            }
        }
    }
}
