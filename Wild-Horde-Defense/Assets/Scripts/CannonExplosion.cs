using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonExplosion : MonoBehaviour
{
    public Tower tower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {  
        if (other.CompareTag("EnemyAlive"))
        {

            Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, gameObject.GetComponent<SphereCollider>().radius + 10f);

            foreach (Collider col in collidersInRadius)
            {
                if (col.CompareTag("EnemyAlive"))
                {
                    col.GetComponent<EnemyStat>().UpdateHealth(-tower.dmg);
                }
            }
            this.gameObject.SetActive(false);

        }

    }
}
