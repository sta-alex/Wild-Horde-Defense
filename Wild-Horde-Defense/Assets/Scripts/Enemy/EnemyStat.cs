using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    private float maxHealth = 150f;
    private float currenHealth  = 0f;
    private float moveSpeed;
    private float maxSpeed;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EnemyPathController pathController;
    
    private float reduceSpeed = 2f;
    private Coroutine smoothSpeedCoroutine;

    private CapsuleCollider bodycollider;


    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        currenHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currenHealth);
        bodycollider = gameObject.GetComponent<CapsuleCollider>();
        if(pathController.isActiveAndEnabled)
            moveSpeed = pathController.GetSpeed();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 
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
        if(moveSpeed != CurrentSpeed())
        smoothSpeedCoroutine = StartCoroutine(SmoothlyUpdateSpeed(newspeedtarget));
        moveSpeed = newspeedtarget;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TownHallEntrance"))
        {
            Destroy(gameObject);
        }
    }

}
