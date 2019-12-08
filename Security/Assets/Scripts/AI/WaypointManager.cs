using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get { return GetInstance(); } }

    private static WaypointManager instance;

    private static WaypointManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<WaypointManager>();
        }
        return instance;
    }

    Dictionary<Point, GameObject> waypoints;

    public List<Point> GetAvailableWaypoints()
    {
        if (waypoints == null) { FindWayPoints(); }

        List<Point> points = waypoints.Where(pair => pair.Value == null)
                  .Select(pair => pair.Key).ToList();

        Debug.Assert(points.Count != 0, "No available waypoint found!");

        return points;
    }

    public void ClaimWaypoint(Point point, GameObject gameObject)
    {
        waypoints[point] = gameObject;
    }

    public void ClearWaypoint(Point point)
    {
        waypoints[point] = null;
    }

    private void FindWayPoints()
    {
        List<Point> points = PointManager.Instance.GetPoints(PointType.WayPoint);
        waypoints = new Dictionary<Point, GameObject>();

        foreach (Point point in points)
        {
            waypoints.Add(point, null);
        }
    }

}
