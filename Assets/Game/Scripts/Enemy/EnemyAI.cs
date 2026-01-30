using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class EnemyAI : MonoBehaviour
{
    enum state{
        Walking,
        Death,
        Attack,
        Stop

    }
    public GameObject target; 
    Animator myAnimator;
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindWithTag("Finish");
        if(target!=null){
        myAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.transform.position)<3){
            myAnimator.SetInteger("State", 2);
        }
    }
}
