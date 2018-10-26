//
//   AIFollow
//   A script attached to each enemy who will follow the player
//   This script switches back and forth between following the player and patrolling the Patrol Points attached to its spawner.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AIFollow : MonoBehaviour
{

    public static float PTIME = 5f;   //the default setting of the timer countdowns


    GameObject target = null;  //Will be the player
    GameObject[] points;   //Will be a list of all spawner points

    [Header("Debug Variables")]
    [Tooltip("The list of its patrol points")]
    public List<Transform> targetArr;
    [Tooltip("Has the player been found?")]
    public bool playerFound = false;
    [Tooltip("How long it's been since the player has been close enough")]
    public float LostPlayerTimer;
    [Tooltip("How long it's been since it's started heading to the pos")]
    public float PatrolTimer;
    [Tooltip("Which point in the patrol list it's headed for")]
    public int PatrolIndex = 0;
    [Tooltip("How far from the player character this enemy is")]
    public float distance;
    [Tooltip("Unique character val")]
    public char patrolChar;


    void Start()
    {
        target = GameObject.Find("Player");    //Find the player
        patrolChar = name.ToCharArray()[0];    //Get your unique ID
        PatrolTimer = PTIME;
        LostPlayerTimer = PTIME;

        points = GameObject.FindGameObjectsWithTag("Spawner");   //Find all of the spawner objects in the scene
        targetArr.Clear();

        //Cycle through all spawner objects and only add the ones that match our character
        foreach (GameObject n in points)
        {
            if (n.name.Contains("PatrolPoint") && n.transform.name.ToCharArray()[0] == patrolChar)  //AGreenSlimeSpawner BGreenSLime
            {
                targetArr.Add(n.transform);
            }
        }
    }

    void FixedUpdate()  //Happens every fixed frame
    {
        distance = Vector2.Distance(target.transform.position, transform.position);   //Calculate the distance between the player and yourself
        if (playerFound)
        {
            MoveTowardsPoint(target.transform.position);
            if (LostPlayerTimer <= 0) //If the player has been out of range for too long
            {
                playerFound = false;
                MoveTowardsPoint(targetArr[PatrolIndex].position);
            }
            else if (distance > 30f)  //Decrements LostPlayerTimer if the player is too far away
            {
                LostPlayerTimer -= Time.deltaTime;
            }
            else  // Keep updating the timer since the player is close enough
            {
                LostPlayerTimer = PTIME;
            }
        }
        else
        {
            if (distance <= 30f)  //You found him!
            {
                PlayerFound();
            }
            else  //Patrolling
            {
                PatrolTimer -= Time.deltaTime;   //The timer between switching points ticks down

                if (PatrolIndex < targetArr.Count && targetArr[PatrolIndex] != null)  //If you're inside of the array and you have a target
                {
                    MoveTowardsPoint(targetArr[PatrolIndex].position);
                    if (PatrolTimer <= 0)   //If it has been longer than PTIME, increment the counter and move towards the next array target
                    {
                        PatrolIndex++;
                        PatrolTimer = PTIME;

                        if (PatrolIndex >= targetArr.Count)  //If you've reached the end of the array, start back at the bottom.
                        {
                            PatrolIndex = 0;
                        }
                    }
                }
            }
        }
    }

    //Simple flag to toggle the bool and reset the timer
    private void PlayerFound()
    {
        playerFound = true;
        LostPlayerTimer = PTIME;
    }

    //Calls the enemystate's walking function (may be different depending on slime)
    private void MoveTowardsPoint(Vector3 pos)
    {
        GetComponent<EnemyState>().walkto(pos);
    }
}
