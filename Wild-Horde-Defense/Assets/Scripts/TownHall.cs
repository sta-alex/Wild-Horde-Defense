using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
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
        Debug.Log("TOWNENTER COLLIDER");
        if (other.gameObject.CompareTag("EnemyAlive"))
        {
            Destroy(other.gameObject);
            ReduceTownHallHp();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TOWNENTER COLLIDER");
        if (collision.gameObject.CompareTag("EnemyAlive"))
        {
            Destroy(collision.gameObject);
            ReduceTownHallHp();
        }
    }

    public void ReduceTownHallHp()
    {
        //todo
    }
}
