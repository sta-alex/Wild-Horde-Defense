using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform waypointsPartent;
    public List<Transform> waypointList = new List<Transform>();
    public Transform startPoint;
    private List<Transform> movementPath;
    public NavMeshAgent pathAgent;
    private int nearestWayPoint = 0;
    void Start()
    {
        GetNearestWayPoints(startPoint);
        pathAgent.SetDestination(startPoint.position);
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if(pathAgent.remainingDistance < 4.0)
        {
            GoToNextWaypoint();
        }
    }
    private void GetNearestWayPoints(Transform startPoint)
    {
        float shortestDistance = float.MaxValue;
        if(waypointsPartent != null)
        {
            for(int i = 0; i< waypointsPartent.childCount; i++)
            {
                
                float distance = Vector3.Distance(startPoint.position, waypointsPartent.GetChild(i).position);
                if(distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestWayPoint = i;
                }
            }
        }
        else
        {
            Debug.LogError("WaypointError while getting Child from Parent");
        }
    }
    private void GoToNextWaypoint()
    {
        if(nearestWayPoint < waypointsPartent.childCount)
        {
            pathAgent.SetDestination(waypointsPartent.GetChild(nearestWayPoint).position);
            nearestWayPoint+=1;
        }
  
    }

    public void updateSpeed(float speed)
    {
        float angularAndAccelleration = speed * 12;
        pathAgent.speed = speed;
        pathAgent.angularSpeed = angularAndAccelleration;
        pathAgent.acceleration = angularAndAccelleration;
    }
    public float getSpeed()
    {
        return pathAgent.speed;
    }

}
