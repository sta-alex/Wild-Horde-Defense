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

    public int LvlNumver = 0;
    public int maxWaves = 9;
    [SerializeField] int currentWave = 0;
    private int enemyStatMultiplier = 1;
    [SerializeField] static float LvlStartDelay = 7f;
    [SerializeField] static float WaveIntervallDelay = 20f;

    public float SpeedReductionFactor = 0.002f;



    [SerializeField] private int numberofAliveEnemies;








    // Start is called before the first frame update
    void Start()
    {
        //SpawnPoints();
        //SpawnWaveofSize(Enemies[0], 6);
        StartCoroutine(EventTimerOnce(LvlStartDelay, StartWaves));


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
        for (int i = 0; i < Spawnlocations.Count; i++)
        {
            if (Spawnlocations[i] == spawnobject)
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
        int locationNumber = Random.Range(0, Spawnlocations.Count);
        return Spawnlocations[locationNumber];
    }


    public void SpawnWaveofSize(GameObject enemy, int size)
    {
        for (int i = 0; i < size; i++)
        {
            spawnhandler.SpawnGameobject(GetSpawnlocation(), new Vector2(5, 5), enemy, 1, true);
        }
    }

    IEnumerator EventTimerOnce(float waitingtime, System.Action function)
    {
        yield return new WaitForSeconds(waitingtime);
        function.Invoke();
    }

    IEnumerator RepeatEventTimer(float intervalltime, System.Action intervallfunction)
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalltime);
            intervallfunction.Invoke();
        }
    }


    void SpawnWaveWithPattern()
    {
        switch (currentWave)
        {
            case 0:
                currentWave += 1;
                SpawnWaveofSize(Enemies[0], 6);
                break;
            case 1:
                currentWave += 1;
                SpawnWaveofSize(Enemies[1], 6);
                break;
            case 2:
                currentWave += 1;
                SpawnWaveofSize(Enemies[2], 6);
                enemyStatMultiplier += 1;
                break;
            case 3:
                currentWave += 1;
                SpawnWaveofSize(Enemies[0], 6);
                break;
            case 4:
                currentWave += 1;
                SpawnWaveofSize(Enemies[1], 6);
                break;
            case 5:
                currentWave += 1;
                SpawnWaveofSize(Enemies[2], 6);
                break;
            case 6:
                currentWave += 1;
                if (LvlNumver == 0)
                    SpawnWaveofSize(Boss[0], 1);
                else
                    SpawnWaveofSize(Boss[1], 1);
                enemyStatMultiplier += 1;
                break;
            case 7:
                currentWave += 1;
                SpawnWaveofSize(Enemies[0], 6);
                break;
            case 8:
                currentWave += 1;
                SpawnWaveofSize(Enemies[1], 6);
                break;
            case 9:
                currentWave += 1;
                SpawnWaveofSize(Enemies[2], 6);
                break;
            default:
                Debug.Log("currentWave default ");
                break;
        }
    }

    public (float,float) getEnemyStat_HP_SPEED(GameObject enemy)
    {

        float maxHealth = enemy.gameObject.GetComponent<EnemyStat>().GetMaxHealth() * enemyStatMultiplier;

        float maxSpeed = enemy.gameObject.GetComponent<EnemyStat>().GetMaxSpeed();

        float reducedmaxSpeed = maxSpeed - (maxHealth * SpeedReductionFactor);

        return (maxHealth, reducedmaxSpeed);
    }

    private void StartWaves()
    {
        SpawnWaveWithPattern();
        StartCoroutine(RepeatEventTimer(WaveIntervallDelay, SpawnWaveWithPattern));
    }

}
