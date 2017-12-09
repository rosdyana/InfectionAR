using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandlerGP2 : MonoBehaviour {
    private Transform goal;
    private NavMeshAgent agent;
    [SerializeField]
    private float speed;
    public DefaultTrackableEventHandler trackingHandler;
    [SerializeField]
    public String anim_walk;
    [SerializeField]
    public String anim_death;
    [SerializeField]
    public String anime_idle;
    [SerializeField]
    private String goal_name;
    [SerializeField]
    private AudioSource deathSoundFx;
    [SerializeField]
    private GameObject pnlGameOver;
    private bool targetAchieved = false;

    //public PlayerShoot ps;

    void Update()
    {
        if (trackingHandler.isTracked)
        {
            if (!targetAchieved)
            {
                //Debug.Log("start walking");
                //create references
                goal = GameObject.FindGameObjectWithTag(goal_name).transform;
                transform.position = Vector3.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
                //start the walking animation
                //GetComponent<Animation>().Play("Zombie_Walk_01");
                PlayAnimation(anim_walk);
                //trackingHandler.isTracked = false;
                targetAchieved = false;
            }
            else
            {
                PlayAnimation(anime_idle);
            }

        }
    }


    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "bullet")
        {
            deathSoundFx.Play();
            Debug.Log("Enemy Destroyed");
            PlayAnimation(anim_death);
            Destroy(gameObject, 2);
        }
        if (col.gameObject.tag == goal_name)
        {
            Debug.Log("Goal Destroyed");
            Destroy(col.gameObject);
            trackingHandler.isTracked = false;
            targetAchieved = true;
            GameOver(pnlGameOver);
        }
    }

    void GameOver(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void PlayAnimation(String anim_name)
    {
        GetComponent<Animation>().Play(anim_name);
    }

    public void StopAnimation(String anim_name)
    {
        GetComponent<Animation>().Stop(anim_name);
    }
}
