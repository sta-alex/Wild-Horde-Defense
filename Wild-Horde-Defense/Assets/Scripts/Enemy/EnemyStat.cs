using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    private float maxHealth = 150;
    private float currenHealth;
    [SerializeField] private HealthBar healthBar;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        currenHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currenHealth);
        
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
        healthBar.UpdateHealthBar(maxHealth, currenHealth);
    }
}
