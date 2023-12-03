using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;

    public WaveManager waveManager;
    public bool isDead = false;
    public bool isBossSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        waveManager = GameObject.Find("Wavemanager").GetComponent<WaveManager>();
        WalkAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.CompareTag("EnemyDead"))
        {
            DeadAnimation();
        }else if (isBossSpawned)
        {
            RunAnimation();
        }
    }

    private void OnDestroy()
    {
        DeadAnimation();
    }

    public void IdelAnimation()
    {
        anim.SetBool("isWalkAnim", false);
        anim.SetBool("isRunAnim", false);
    }
    public void WalkAnimation()
    {
        
        anim.SetBool("isWalkAnim", true);
        anim.SetBool("isRunAnim", false);
    }

    public void RunAnimation()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isWalkAnim", true);
        anim.SetBool("isRunAnim", true);
    }

    public void DeadAnimation()
    { 
        anim.SetBool("isDeadAnim", true);
    }

    public Animator getAnim()
    {
        return gameObject.GetComponent<Animator>();
    }

    public void setisBossSpawned(bool isspawned)
    {
        isBossSpawned = isspawned;
    }
}
