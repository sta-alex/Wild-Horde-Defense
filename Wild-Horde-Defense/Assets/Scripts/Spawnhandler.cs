using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawnhandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Terrain TerrainMap;
    private float baserespawntimer = 25f;
    public float spawntimer = 2.25f;
    public List<GameObject> spawnList;
    public WaveManager waveManager;
    private GameObject spawnPointobject;

    /*Tests
    public GameObject TestspawnPoint;
    public Vector2 TestspawnSizeXZ;
    public GameObject TestobjectToSpawn;
    public int Testspawnnumber;
    public bool TestonTerrain;
    private NavMeshAgent navAgent;
    */

    void Start()
    {
        waveManager = GameObject.Find("Wavemanager").GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnGameobject(GameObject spawnPoint, Vector2 spawnSizeXZ, GameObject objectToSpawn, int spawnnumber, bool onTerrain = false)
    {
        spawnPointobject = spawnPoint;
        StartCoroutine(SpawnObject(spawnPoint, spawnSizeXZ, objectToSpawn, spawnnumber, onTerrain));

    }
    IEnumerator SpawnObject(GameObject spawnPoint, Vector2 spawnSizeXZ, GameObject objectToSpawn, int spawnnumber, bool onTerrain = false)
    {
        NavMeshAgent navAgent = objectToSpawn.GetComponent<NavMeshAgent>();
        if (navAgent != null)
        {
            (float maxHp, float maxSpeed) = waveManager.getEnemyStat_HP_SPEED(objectToSpawn);
            spawntimer = (baserespawntimer / maxSpeed) + 0.5f ;
        }

        for (int i = 0; i < spawnnumber; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(spawnPoint, spawnSizeXZ, onTerrain);
            SpawnObjectAtPostition(objectToSpawn, spawnPosition);
            yield return new WaitForSeconds(spawntimer);
        }
    }


    private Vector3 GetSpawnPosition(GameObject spawnPoint, Vector2 spawnSizeXZ, bool onTerrain)
    {
        Vector3 spawnPosition = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
        if (onTerrain)
        {
            float heighty = TerrainMap.SampleHeight(spawnPosition);
            spawnPosition = new Vector3(spawnPosition.x, heighty, spawnPosition.z); //todo vtl etwas höher?
        }
        float spawnsizeX = spawnSizeXZ.x;
        float spawnsizeZ = spawnSizeXZ.y;
        float randomx = Random.Range((-spawnsizeX) / 2, spawnsizeX / 2);
        float randomz = Random.Range((-spawnsizeZ) / 2, spawnsizeZ / 2);
        return spawnPosition = new Vector3(spawnPosition.x + randomx, spawnPosition.y, spawnPosition.z + randomz);
    }

    private void SpawnObjectAtPostition(GameObject objectToSpawn, Vector3 spawnPosition)
    {

        if (objectToSpawn == null)
        {
            Debug.LogError("Null SpawnObject");
            
        }
        else
        {
             NavMeshAgent navAgent = objectToSpawn.GetComponent<NavMeshAgent>();
            
            GameObject createdObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            createdObject.GetComponent<EnemyPathController>().setfirstLocation(waveManager.getWayPointsEntrance(spawnPointobject));
            (float maxHp , float maxSpeed) = waveManager.getEnemyStat_HP_SPEED(createdObject);
            createdObject.GetComponent<EnemyStat>().SetMaxHealth(maxHp);
            createdObject.GetComponent<EnemyStat>().SetMaxSpeed(maxSpeed);
            createdObject.GetComponent<EnemyPathController>().UpdateSpeed(maxSpeed);
            if (waveManager.isBossSpawn()){
                createdObject.GetComponent<AnimationHandler>().setisBossSpawned(true);
            }
                


            spawnList.Add(createdObject);

            if (navAgent != null)
            {
                spawntimer = baserespawntimer / maxSpeed;
            }
            
        }
    }

    public void SpawnTestfunc()
    {
        //SpawnGameobject(TestspawnPoint, TestspawnSizeXZ, TestobjectToSpawn, Testspawnnumber, TestonTerrain);
    }

    public List<GameObject> GetEnemies()
    {
        return spawnList;
    }
    public void ResetEnemies()
    {
        spawnList.Clear();
    }

  
}
