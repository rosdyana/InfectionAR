using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;
using Vuforia;

public class EnemyHandler : MonoBehaviour
{

    private Transform goal;
    private NavMeshAgent agent;
    public float speed;
    public DefaultTrackableEventHandler trackingHandler;
    public String anim_walk;
    public String anim_death;


    void Update()
    {
        if (trackingHandler.isTracked)
        {
            Debug.Log("start walking");
            //create references
            goal = GameObject.FindGameObjectWithTag("coin").transform;
            transform.position = Vector3.MoveTowards(transform.position, goal.position, speed*Time.deltaTime);
            //Debug.Log(goal);
            //agent = GetComponent<NavMeshAgent>();
            ////set the navmesh agent's desination equal to the main camera's position (our first person character)
            //agent.SetDestination(goal.position);
            //start the walking animation
            //GetComponent<Animation>().Play("Zombie_Walk_01");
            GetComponent<Animation>().Play(anim_walk);
            //trackingHandler.isTracked = false;
        }
    }
        

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "coin")
        {
            Debug.Log("Coin Destroyed");
            Destroy(col.gameObject);
            trackingHandler.isTracked = false;
        }
        if (col.gameObject.tag == "bullet")
        {
            Debug.Log("Enemy Destroyed");
            GetComponent<Animation>().Play(anim_death);
            Destroy(gameObject, 2);
        }
    }
}
