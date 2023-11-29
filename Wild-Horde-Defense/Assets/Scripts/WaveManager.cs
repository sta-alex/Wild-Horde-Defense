using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Boss;
    public List<GameObject> Spawnlocations;
    public List<GameObject> wayPointentrance;
    [SerializeField] Spawnhandler spawnhandler;
    public int currentWave;
    [SerializeField] private int numberofAliveEnemies;
    public int maxWaves = 10;



    // Start is called before the first frame update
    void Start()
    {
        //SpawnPoints();
        SpawnWaveofSize(Enemies[0], 6);



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void SpawnPoints()
    {
        foreach (GameObject spawn in Spawnlocations)
        {
            wayPointentrance.Add(spawn.GetComponentInChildren<GameObject>());
        }
    }

    public Transform getWayPointsEntrance(GameObject spawnobject)
    {
        for( int i = 0; i < Spawnlocations.Count; i++)
        {
            if(Spawnlocations[i] == spawnobject)
            {
                return wayPointentrance[i].transform;
            }
        }
        return null;
    }
    public void CharackterDeadInfo()
    {

    }

    private GameObject GetSpawnlocation()
    {
        int locationNumber = Random.Range(0, Spawnlocations.Count );
        return Spawnlocations[locationNumber];
    }


    public void SpawnWaveofSize(GameObject enemy, int size)
    {
        for(int i = 0; i < size; i++)
        {
            spawnhandler.SpawnGameobject(GetSpawnlocation(), new Vector2(5, 5), enemy, 1, true); 
        }
    }

}
