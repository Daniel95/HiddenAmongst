using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get { return GetInstance(); } }

    private static PointManager instance;

    private static PointManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PointManager>();
        }
        return instance;
    }

    private List<Point> points;

    private void FindPoints()
    {
        points = FindObjectsOfType<Point>().ToList();
    }
 
    public List<Point> GetPoints(PointType pointType)
    {
        if(points == null) { FindPoints(); }

        return points.FindAll(x => x.pointType == pointType);
    }
}
