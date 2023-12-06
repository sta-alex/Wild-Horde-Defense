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
    public NavMeshAgent pathAgent;
    private int nearestWayPoint = 0;
    private Transform[] splitWayPointArray;
    private int splitWayPoint = 0;
    private bool isOnRoute = true;
    private bool reachedstartDest = true;
    void Start()
    {
        waveManager = GameObject.Find("Wavemanager").GetComponent<WaveManager>();
        waypointsPartent = GameObject.Find("WaypointParent").transform;
        pathAgent = GetComponent<NavMeshAgent>();
        SetNearestWayPoint(startPoint);
        pathAgent.SetDestination(startPoint.position);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (pathAgent != null)
        {
            if (pathAgent.isActiveAndEnabled)
            {
                float distance = pathAgent.remainingDistance;
                if (reachedstartDest)
                {
                    reachedstartDest = false;
                    pathAgent.SetDestination(startPoint.position);
                }
                else if (distance <= 4.0f)
                {
                    gameObject.tag = "EnemyAlive";
                    GoToNextWaypoint();
                }
            }
        }
    }
    private void SetNearestWayPoint(Transform startPoint)
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

        if (isOnRoute){ 

            if (nearestWayPoint < waypointsPartent.childCount)
            {
                if (waypointsPartent.GetChild(nearestWayPoint).CompareTag("WayPointSplit"))
                {

                    int rand = Random.Range(0, 3);
                    if (rand == 0)
                    {
                        splitWayPointArray = waypointsPartent.GetChild(nearestWayPoint).GetComponentsInChildren<Transform>();
                        isOnRoute = false;
                        splitWayPoint = 0;
                        pathAgent.SetDestination(splitWayPointArray[splitWayPoint].position);
                        splitWayPoint += 1;
                    }
                    else
                    {
                        pathAgent.SetDestination(waypointsPartent.GetChild(nearestWayPoint).position);
                        nearestWayPoint += 1;
                    }

                }
                else
                {
                    pathAgent.SetDestination(waypointsPartent.GetChild(nearestWayPoint).position);
                    nearestWayPoint += 1;
                }
            }
        }
        else
        {
            if(splitWayPoint < splitWayPointArray.Length)
            {
                pathAgent.SetDestination(splitWayPointArray[splitWayPoint].position);
                splitWayPoint += 1;
            }else
            {
                
                isOnRoute = true;
                SetNearestWayPoint(splitWayPointArray[splitWayPointArray.Length - 1]);
                pathAgent.SetDestination(waypointsPartent.GetChild(nearestWayPoint).position);
                splitWayPoint = 0;
                
            }
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
        if (pathAgent != null)
            return pathAgent.speed;
        else
            return 0f;
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
