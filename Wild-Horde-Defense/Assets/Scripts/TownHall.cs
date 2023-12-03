using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownHall : MonoBehaviour
{
    public HealthBarHUDTester healthBar;
    private int lives = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAlive"))
        {
            
            Destroy(other.gameObject);
            ReduceTownHallHp();
        }
    }

    public void ReduceTownHallHp()
    {
        healthBar.Hurt(1);
        lives -= 1;
        if(lives <= 0)
        {
            SceneManager.LoadScene("GameOverMenu");
        }
    }
}
