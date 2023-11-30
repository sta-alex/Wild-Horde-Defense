using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    private float maxHealth = 150f;
    private float currenHealth  = 0f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EnemyPathController pathController;
    [SerializeField] private float reduceSpeed = 2f;
    private Coroutine smoothSpeedCoroutine;
    private float movespeed;


    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        currenHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currenHealth);
        if(pathController.isActiveAndEnabled)
            movespeed = pathController.GetSpeed();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        if(timer > 200)
        {
            UpdateHealth(-30);
            timer = 0;
            
        }
    }

    public void UpdateHealth(float amount)
    {
        currenHealth += amount;
        if(currenHealth > 0 && currenHealth <= maxHealth)
        {
            healthBar.UpdateHealthBar(maxHealth, currenHealth);
        }else if (currenHealth <= 0)
        {
            Death();
        }
    }
        
    public void SetMaxHealth( float newmaxHealth)
    {
        this.maxHealth = newmaxHealth;
    }
     public void Death()
    {
        InterruptPathing(true);
        UpdateSpeed(0f);
        currenHealth = 0f;
        //doto dy animation und tag ändern -> Tower sagen anderes ziel wählen
    }
    
    public void Revive()
    {
        InterruptPathing(false);
        currenHealth = 1f;
        UpdateHealth(+50f);
    }

    public float GetCurrentHealth()
    {
        return this.currenHealth;
    }

    public void UpdateSpeed(float newspeedtarget)
    {
        if(movespeed != CurrentSpeed())
        smoothSpeedCoroutine = StartCoroutine(SmoothlyUpdateSpeed(newspeedtarget));
        movespeed = newspeedtarget;
    }
    
    public float CurrentSpeed()
    {
        return pathController.GetSpeed();
    }

    public void InterruptPathing(bool interrupt)
    {
        pathController.StopPathing(interrupt);
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
}
