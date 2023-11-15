using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for Theodore character animation control
public class TheodoreAnimator : MonoBehaviour
{
    Animator anim;
    int lastAnimIndex = 12;     // Index of last animation index in animator
    float timeBetween = 0;      //Variable for random time for Idle animation
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(timeBetween <= 0)
        {
            RandomAnim();
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }

    public void Mistake()
    {
        anim.SetTrigger("Wrong");
    }

    public void Correct()
    {
        anim.SetTrigger("Right");
    }

    //Random animation from tree
    public void RandomAnim()
    {
        anim.SetInteger("IdleState", Random.Range(1, lastAnimIndex+1));
    }
    //Set idle animation with random time
    public void ResetIdleState()
    {
        anim.SetInteger("IdleState", 0);
        timeBetween = Random.Range(3f, 5f);
    }

    

}
