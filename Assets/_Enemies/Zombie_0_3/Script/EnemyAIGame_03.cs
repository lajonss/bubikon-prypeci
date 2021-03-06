﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EnemyAIGame_03 : MonoBehaviour
{
    #region serialize fields variables

    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float attackDistance = 3.0f;
    [SerializeField] private float attackDemage = 10.0f;
    [SerializeField] private float attackDelay = 1.0f;
    [SerializeField] private Transform[] transforms;
    private Component getDmg;
    #endregion

    #region private variables

    private float timer = 0;
    private string currentState;
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    private Transform[] checkpointPoints;
    private Boolean inTheTrigger = false;
    private int checkpointCounter = 0;

    #endregion

    void Start()
    {

        getDmg = GetComponent<getDmg>();
        inTheTrigger = false;
        animator = this.GetComponent<Animator>();
        currentState = "";
    }

    void TheStart(Transform[] v)
    {
        checkpointPoints = v;
    }

    #region trigers

    void OnTriggerExit(Collider other)
    {
        
        if (other.tag.Equals("Player"))
        {
            animationSet("idle0");
            inTheTrigger = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inTheTrigger = true;
        }
        
    }

    void Update()
    {
        if (getDmg != null && ((getDmg) getDmg).hp <= 0)
        {
            animationSet("death");
            Invoke("destroyMe", 4.5f);
        }
        if (inTheTrigger == false && checkpointPoints != null)
        {
            if (checkpointCounter == checkpointPoints.Length)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(checkpointPoints[checkpointCounter].transform.position - transform.position);
                float oryginalX = transform.rotation.x;
                float oryginalZ = transform.rotation.z;

                Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
                finalRotation.x = oryginalX;
                finalRotation.z = oryginalZ;
                transform.rotation = finalRotation;

                float distance = Vector3.Distance(transform.position,
                    checkpointPoints[checkpointCounter].transform.position);
                if (distance < 4.0f)
                {
                    checkpointCounter++;
                }
                else
                {
                    animationSet("run");
                    transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        var getDmg = GetComponent<getDmg>();

        if (other.tag.Equals("Player") && (getDmg == null || (getDmg != null && getDmg.hp > 0)))
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
                    timer = attackDelay;
                }
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
    }
    private void destroyMe()
    {
        Destroy(this.gameObject);
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

    #endregion
}