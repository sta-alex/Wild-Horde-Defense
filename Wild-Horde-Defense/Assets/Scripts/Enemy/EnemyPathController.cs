using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathController : MonoBehaviour
{
    // Start is called before the first frame update
    public WaveManager waveManager;
    private Transform waypointsPartent;
    private List<Transform> waypointList = new List<Transform>();
    private Transform startPoint;
    private List<Transform> movementPath;
    private NavMeshAgent pathAgent;
    private int nearestWayPoint = 0;
    private bool reachedstartDest = true;
    void Start()
    {
        waypointsPartent = GameObject.Find("WaypointParent").transform;
        pathAgent = gameObject.GetComponent<NavMeshAgent>();
        GetNearestWayPoints(startPoint);
        pathAgent.SetDestination(startPoint.position);
    }
   
    // Update is called once per frame
    void Update()
    {
        float distance = pathAgent.remainingDistance;
        if (reachedstartDest)
        {
            reachedstartDest = false;
            pathAgent.SetDestination(startPoint.position);
        }
        else if(distance <= 4.0f)
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
        else
        {
            Debug.Log("Destroyed");
            //Destroy(gameObject); // trigger Destroy
        }
  
    }

    public void UpdateSpeed(float speed)
    {
        float angularAndAccelleration = speed * 12;
        pathAgent.speed = speed;
        pathAgent.angularSpeed = angularAndAccelleration;
        pathAgent.acceleration = angularAndAccelleration;
    }
    public float GetSpeed()
    {
        return pathAgent.speed;
    }

    public void StopPathing(bool isStop)
    {
        pathAgent.isStopped = isStop;
    }

    public void setfirstLocation(Transform transform)
    {
        startPoint = transform;
    }
}
