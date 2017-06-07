using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIMenu_03 : MonoBehaviour {

    #region public variables
    public Transform[] transforms;
    #endregion

    #region private variables
    private int counter = 0;
    private string currentState;
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    private Transform[] checkpointPoints;
    private float walkSpeed;
    #endregion

    void Start()
    {
        walkSpeed = Random.Range(6.0f, 8.0f);
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
        }
    }

    void Update()
    {
        if (counter == checkpointPoints.Length)
        {
            Object.Destroy(this.gameObject);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(checkpointPoints[counter].transform.position - transform.position);
            float oryginalX = transform.rotation.x;
            float oryginalZ = transform.rotation.z;

            Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
            finalRotation.x = oryginalX;
            finalRotation.z = oryginalZ;
            transform.rotation = finalRotation;

            float distance = Vector3.Distance(transform.position, checkpointPoints[counter].transform.position);
            if (distance < 10.0f)
            {
                counter++;
            }
            else
            {
                animationSet("run");
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
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

    #endregion
}
