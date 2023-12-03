using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Boss;
    public List<GameObject> Spawnlocations;
    public List<GameObject> wayPointentrance;
    public Spawnhandler spawnhandler;

    public GameObject timerObj;

    public int LvlNumver = 0;
    public int maxWaves = 9;
    public int currentWave = 0;
    private int enemyStatMultiplier = 1;
    public float LvlStartDelay = 7f;
    private float shortStartDelay = 5f;
    public float WaveIntervallDelay = 20f;
    private Timer timer;
    public bool bossIsSpawned = false;

    public float SpeedReductionFactor = 0.002f;

    private int numberofAliveEnemies = 0;

    private Coroutine waveStartCoroutine;
    private Coroutine waveIntervallCoroutine;








    // Start is called before the first frame update
    void Start()
    {
        timer = timerObj.transform.Find("Timer").GetComponent<Timer>();
        timer.StartTimer();
        //SpawnPoints();
        //SpawnWaveofSize(Enemies[0], 6);
        waveStartCoroutine = StartCoroutine(EventTimerOnce(LvlStartDelay, StartWaves));
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
        numberofAliveEnemies -= 1;
        if(numberofAliveEnemies <= 0)
        {
            StopCoroutine(waveStartCoroutine);
            StopCoroutine(waveIntervallCoroutine);
            SpawnWaveWithPattern();
            waveIntervallCoroutine = StartCoroutine(EventTimerOnce(WaveIntervallDelay, StartWaves));
        }
    }

    private GameObject GetSpawnlocation()
    {
        int locationNumber = Random.Range(0, Spawnlocations.Count);
        return Spawnlocations[locationNumber];
    }


    public IEnumerator SpawnWaveofSize(GameObject enemy, int size)
    {
        for (int i = 0; i < size; i++)
        {
            numberofAliveEnemies += 1;
            spawnhandler.SpawnGameobject(GetSpawnlocation(), new Vector2(5, 5), enemy, 1, false);
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator EventTimerOnce(float waitingtime, System.Action function)
    {
        timer.StopTimer();
        timer.seconds = (int)waitingtime;
        timer.StartTimer();
        yield return new WaitForSeconds(waitingtime);
        function.Invoke();
    }

    IEnumerator RepeatEventTimer(float intervalltime, System.Action intervallfunction)
    {
        while (true)
        {
            timer.seconds = (int) intervalltime;
            timer.StartTimer();
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
                StartCoroutine(SpawnWaveofSize(Enemies[0], 6));
                break;
            case 1:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[1], 6));
                break;
            case 2:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[2], 6));
                enemyStatMultiplier += 1;
                break;
            case 3:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[0], 6));
                break;
            case 4:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[1], 6));
                break;
            case 5:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[2], 6));
                break;
            case 6:
                currentWave += 1;
                
                if (LvlNumver == 0)
                    StartCoroutine(SpawnWaveofSize(Boss[0], 1));
                else
                    StartCoroutine(SpawnWaveofSize(Boss[1], 1));
                enemyStatMultiplier += 1;
                bossIsSpawned = true;
                break;
            case 7:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[0], 6));
                break;
            case 8:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[1], 6));
                break;
            case 9:
                currentWave += 1;
                StartCoroutine(SpawnWaveofSize(Enemies[2], 6));
                break;
            default:
                Debug.Log("currentWave default ");
                StopCoroutine(waveStartCoroutine);
                timer.StopTimer();
                timerObj.SetActive(false);
                break;
        }
    }

    public (float,float) getEnemyStat_HP_SPEED(GameObject enemy)
    {

        float maxHealth = enemy.gameObject.GetComponent<EnemyStat>().GetMaxHealth() * enemyStatMultiplier;

        float maxSpeed = enemy.gameObject.GetComponent<EnemyStat>().GetMaxSpeed();

        float reducedmaxSpeed = maxSpeed - (maxHealth * SpeedReductionFactor);

        if (bossIsSpawned)
        {
            reducedmaxSpeed = reducedmaxSpeed * 1.5f;
        }

        return (maxHealth, reducedmaxSpeed);
    }

    private void StartWaves()
    {
        SpawnWaveWithPattern();
        waveIntervallCoroutine = StartCoroutine(RepeatEventTimer(WaveIntervallDelay, SpawnWaveWithPattern));
    }

    public bool isBossSpawn()
    {
        return bossIsSpawned;
    }

}
