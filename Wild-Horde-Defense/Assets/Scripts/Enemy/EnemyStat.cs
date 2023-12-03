using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float maxHealth = 300f;
    public float maxSpeed = 25f;

    public float currenHealth;
    private float currentSpeed;


    public HealthBar healthBar;
    public EnemyPathController pathController;

    private float reduceSpeed = 2f;
    private Coroutine smoothSpeedCoroutine;
    private Coroutine slowCoroutine;

    private CapsuleCollider bodycollider;

    public GameObject freezeEffect;


    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        currenHealth = maxHealth;
        //healthBar.UpdateHealthBar(maxHealth, currenHealth);
        bodycollider = gameObject.GetComponent<CapsuleCollider>();
        if (pathController.isActiveAndEnabled)
            currentSpeed = pathController.GetSpeed();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    #region HEALTH
    public void SetMaxHealth(float newmaxHealth)
    {
        this.currenHealth = newmaxHealth;
        this.maxHealth = newmaxHealth;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public float GetCurrentHealth()
    {
        return this.currenHealth;
    }
    public void UpdateHealth(float amount)
    {
        currenHealth += amount;
        if (currenHealth > 0 && currenHealth <= maxHealth)
        {
            healthBar.UpdateHealthBar(maxHealth, currenHealth);
        }
        else if (currenHealth <= 0)
        {
            currenHealth = 0f;
            healthBar.UpdateHealthBar(maxHealth, currenHealth);
            Death();
        }
    }
    #endregion

    #region SPEED
    public void SetMaxSpeed( float newmaxSpeed)
    {
        UpdateSpeed(newmaxSpeed);
        this.maxSpeed = newmaxSpeed;
    }
    public float GetMaxSpeed()
    {
        return this.maxSpeed;
    }

    public float GetCurrentSpeed()
    {
        return pathController.GetSpeed();
    }

    public void UpdateSpeed(float newspeedtarget)
    {
        if (currentSpeed != GetCurrentSpeed())
            smoothSpeedCoroutine = StartCoroutine(SmoothlyUpdateSpeed(newspeedtarget));
        currentSpeed = newspeedtarget;
    }

    private void ResetSpeed()
    {
        UpdateSpeed(GetMaxSpeed());
        freezeEffect.SetActive(false);
    }

    public void SlowSpeed(float percentage)
    {
        freezeEffect.SetActive(true);
        float target = 1 - (percentage / 100);
        UpdateSpeed(GetCurrentSpeed() * target);
        slowCoroutine = StartCoroutine(EventTimerOnce(5.0f, ResetSpeed));
    }

    private IEnumerator SmoothlyUpdateSpeed(float targetValue)
    {
        float currentTime = 0f;
        float initialFillAmount = pathController.GetSpeed();

        while (currentTime < reduceSpeed)
        {
            currentTime += Time.deltaTime;
            pathController.UpdateSpeed(Mathf.Lerp(initialFillAmount, targetValue, currentTime / reduceSpeed));

            yield return null;
        }

        pathController.UpdateSpeed(targetValue);
    }
    #endregion
    
    public void Death()
    {
        UpdateSpeed(0f);
        currenHealth = 0f;
        gameObject.tag = "EnemyDead";
        gameObject.GetComponent<AnimationHandler>().isDead = true;
        InterruptPathing(true);
        StartCoroutine(EventTimerOnce(3f, DestroyObjekt));
    }

    public void Revive()
    {
        InterruptPathing(false);
        currenHealth = 1f;
        UpdateHealth(+50f);
        gameObject.tag = "EnemyAlive";
    }

    public void InterruptPathing(bool interrupt)
    {
        pathController.StopPathing(interrupt);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TownHallEntrance"))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator EventTimerOnce(float waitingtime, System.Action function)
    {
        yield return new WaitForSeconds(waitingtime);
        function.Invoke();
    }
    
    private void DestroyObjekt()
    {
        if(gameObject.name == "Troll Variant(Clone)" || gameObject.name == "Demon Variant(Clone)")
            GameObject.Find("Main Camera").GetComponent<GameManager>().increaseCurrency(500);
        else
            GameObject.Find("Main Camera").GetComponent<GameManager>().increaseCurrency(50);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameObject.Find("Wavemanager").GetComponent<WaveManager>().CharackterDeadInfo();
    }

}
