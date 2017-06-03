using System;
using UnityEngine;
using System.Collections;

public class EnemyAI_03 : MonoBehaviour
{
    #region public variables
    [SerializeField] public float walkSpeed = 5.0f;
    [SerializeField] public float attackDistance = 3.0f;
    [SerializeField] public float attackDemage = 10.0f;
    [SerializeField] public float attackDelay = 1.0f;
    [SerializeField] public float hp = 20.0f;
    public Transform[] transforms;
    #endregion

    #region private variables
    private float timer = 0;
    private string currentState;
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    #endregion

    void Start()
    {
        animator = transforms[0].GetComponent<Animator>();
        currentState = "";
    }


    #region trigers
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animationSet("idle0");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && hp > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - transform.position);
            float oryginalX = transform.rotation.x;
            float oryginalZ = transform.rotation.z;

            Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
            finalRotation.x = oryginalX;
            finalRotation.z = oryginalZ;
            transform.rotation = finalRotation;

            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance > attackDistance && !stateInfo.IsName("Base Layer.wound"))
            {
                animationSet("run");
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else if (distance <= attackDistance)
            {
                if (timer <= 0)
                {
                    animationSet("attack0");
                    // TODO Messenging
                    //other.SendMessage("takeHit", attackDemage);
                    timer = attackDelay;
                }
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }
    #endregion

    #region animation and messaging  
    private void animationReset()
    {
        if (!stateInfo.IsName("Base Layer.idle0"))
        {
            animator.SetBool("idle0ToIdle1", false);
            animator.SetBool("idle0ToWalk", false);
            animator.SetBool("idle0ToRun", false);
            animator.SetBool("idle0ToWound", false);
            animator.SetBool("idle0ToSkill0", false);
            animator.SetBool("idle0ToAttack1", false);
            animator.SetBool("idle0ToAttack0", false);
            animator.SetBool("idle0ToDeath", false);
        }
        else
        {
            animator.SetBool("walkToIdle0", false);
            animator.SetBool("runToIdle0", false);
            animator.SetBool("deathToIdle0", false);
        }
    }

    private void animationSet(string animationToPlay)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationReset();

        if (animationToPlay != "run")
        {
            Debug.Log(stateInfo.IsName("Base Layer.wound"));
        }

        if (currentState == "")
        {
            currentState = animationToPlay;
            if (stateInfo.IsName("Base Layer.walk") && currentState != "walk")
            {
                animator.SetBool("walkToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.run") && currentState != "run")
            {
                animator.SetBool("runToIdle0", true);
            }

            if (stateInfo.IsName("Base Layer.death") && currentState != "death")
            {
                animator.SetBool("deathToIdle0", true);
            }

            string state = "idle0To" + currentState.Substring(0, 1).ToUpper() + currentState.Substring(1);
            animator.SetBool(state, true);
            currentState = "";
        }
    }

    void takeHit(float demage)
    {
        hp -= demage;
        if (hp <= 0)
        {
            animationSet("death");
        }
        else
        {
            animator.CrossFade("wound", 0.5f);
        }
    }
    #endregion
}