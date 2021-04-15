using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationServices
{
    IBuildingManager buildingManager = BuildingManager.Instance;
    GridServices gridServices = new GridServices();

    public List<Coordinates> GetPath(Coordinates start, string stationType, List<string> intake, List<string> output)
    {
        List<Station> workstations;
        if (intake != null)
        {
            workstations = buildingManager.FindSuitableProviders(stationType, intake[0]);
        } else
        {
            workstations = buildingManager.FindSuitableStorages(output[0]);
        }

        List<Coordinates> path = GetShortestPath(start, workstations);
        return path;
    }

    public List<Coordinates> GetPathToParkhouse(Coordinates start)
    {
        // When loading scene reference to buildingManager is null??
        List<Station> parkhouses = BuildingManager.Instance.GetParkhouses();
        List<Coordinates> path = GetShortestPath(start, parkhouses);

        return path;
    }

    private List<Coordinates> GetShortestPath(Coordinates start, List<Station> workstations)
    {
        List<Coordinates> shortestPath = null;
        int closestDistance = -1;

        foreach (Building building in workstations)
        {
            List<Coordinates> path = GetShortestPath(start, building.Cell.Coords);
            
            if (path.Count == 0)
            {
                continue;
            }

            if (closestDistance < 0 || path.Count < closestDistance)
            {
                closestDistance = path.Count;
                shortestPath = path;
            }
        }

        return shortestPath;
    }

    private List<Coordinates> GetShortestPath(Coordinates start, Coordinates target)
    {
        PriorityQueue queue = new PriorityQueue();
        List<Coordinates> path = new List<Coordinates>();
        Dictionary<Coordinates, Coordinates> cameFrom = new Dictionary<Coordinates, Coordinates>();
        Dictionary<Coordinates, float> costSoFar = new Dictionary<Coordinates, float>();

        queue.Add(new PriorityCoords(start, 0));
        cameFrom[start] = start;
        costSoFar[start] = 0;

        Coordinates current;

        while (!queue.IsEmpty())
        {
           current = queue.Pop();

            if (current == target) { break; }

            foreach (Coordinates next in gridServices.GetConnectedNeighborCoords(current))
            {
                float newCost = costSoFar[current] + 1;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    cameFrom[next] = current;
                    costSoFar[next] = newCost;
                    float priority = newCost + CalcManhattanDistance(next, target);

                    queue.Add(new PriorityCoords(next, priority));
                }
            }
        }

        //Reconstruct path
        current = target;        

        if (!cameFrom.ContainsKey(current))
        {
            return path;
        }
        while (current != start)
        {
            path.Insert(0, current);
            current = cameFrom[current];
        }
        path.Insert(0, current);

        return path;
    }

    public void SetConveyorPath(List<Coordinates> path, int startIdx)
    {        
        for (int i=startIdx; i<path.Count-1; i++)
        {
            Building building = gridServices.GetCell(path[i]).Building;

            // only sets path to next station
            if (building is Station && i != startIdx)
            {
                return;
            }
            
            building.SetExit(path[i + 1]);
        }
    }

    private float CalcManhattanDistance(Coordinates start, Coordinates end)
    {
        return Mathf.Abs(start.x-end.x) + Mathf.Abs(start.z-end.z);
    }
}
