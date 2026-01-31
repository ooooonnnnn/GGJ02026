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
        agent.speed=1f;
        agent.SetDestination(target.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.transform.position)<2){
            myAnimator.SetInteger("State", 2);
            agent.isStopped = true;
        }
    }
    public void Hit(){
        print("I was hit when I was young");
        myAnimator.SetInteger("State", 1);
        agent.isStopped = true;
        StartCoroutine(wait_they_dont_love_you());
        IEnumerator wait_they_dont_love_you(){
            yield return new WaitForSeconds(5f);
            //my code here after 3 seconds
            Destroy(gameObject);
        }
        
    }
}
