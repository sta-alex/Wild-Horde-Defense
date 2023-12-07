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

    private float reduceSpeed = 1f;
    private Coroutine smoothSpeedCoroutine;
    private Coroutine slowCoroutine;
    private Coroutine damageCoroutine;

    private CapsuleCollider bodycollider;

    public List <GameObject> particleEffects;
    private Coroutine particlecoroutine;
    private bool isDead = false;


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
        if (!isDead)
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
        if (smoothSpeedCoroutine != null)
            StopCoroutine(smoothSpeedCoroutine);
        if (currentSpeed != GetCurrentSpeed())
            smoothSpeedCoroutine = StartCoroutine(SmoothlyUpdateSpeed(newspeedtarget));
        currentSpeed = newspeedtarget;
    }

    private void ResetSpeed()
    {
        UpdateSpeed(GetMaxSpeed());
        activateParticleEffect(0, false);
        StopCoroutine(damageCoroutine);
    }

    public void SlowSpeed(float percentage)
    {
        StartDamageOverTime(2f, 5f, (int) percentage / 2);
        activateParticleEffect(0, true);
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
        isDead = true;
        UpdateSpeed(0f);
        currenHealth = 0f;
        gameObject.tag = "EnemyDead";
        KillMoney(true);
        gameObject.GetComponent<AnimationHandler>().isDead = true;
        InterruptPathing(true);
        StartCoroutine(EventTimerOnce(2f, DestroyObj));
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
    
    private void KillMoney(bool iskilled)
    {
        if (iskilled){
            if (gameObject.name == "Troll Variant(Clone)" || gameObject.name == "Demon Variant(Clone)")
                GameObject.Find("Main Camera").GetComponent<GameManager>().increaseCurrency(500);
            else
            {
                int myInt = (int)(maxHealth / 300  * 100f) ;
                GameObject.Find("Main Camera").GetComponent<GameManager>().increaseCurrency(myInt);
            }
                
        }
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject waveManagerObject = GameObject.Find("Wavemanager");
        if (waveManagerObject != null)
        {
            WaveManager waveManagerComponent = waveManagerObject.GetComponent<WaveManager>();
            if (waveManagerComponent != null)
            {
                waveManagerComponent.CharackterDeadInfo();
            }
        }
    }

    public void activateParticleEffect(int listNumber, bool isActive, float time = 0f)
    {
        if(particlecoroutine != null)
            StopCoroutine(particlecoroutine);
        if (isActive)
        {
            if(time == 0f)
                particleEffects[listNumber].SetActive(true);
            else
            {
                particleEffects[listNumber].SetActive(true);
                particlecoroutine = StartCoroutine(particletime(listNumber, time));
            }
        }
        else
        {
            particleEffects[listNumber].SetActive(false);
        }
    }

    IEnumerator particletime(int listNumber, float time)
    {
        yield return new WaitForSeconds(time);

        particleEffects[listNumber].SetActive(false);
    }


    private void StartDamageOverTime(float interval, float duration, int twrDMG)
    {
        damageCoroutine = StartCoroutine(DamageOverTime(interval, duration, twrDMG));
    }

    private IEnumerator DamageOverTime(float interval, float duration, int twrDMG)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float damageAmount = twrDMG;
            UpdateHealth(-damageAmount);
            yield return new WaitForSeconds(interval);
            elapsedTime += interval;
        }
        

    }

}
