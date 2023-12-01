using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        WalkAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isDeadAnim", true);
    }

    public Animator getAnim()
    {
        return gameObject.GetComponent<Animator>();
    }
}
