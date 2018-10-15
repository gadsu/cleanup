using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AIFollow : MonoBehaviour
{

    public static float PTIME = 5f;


    GameObject target = null;
    public List<Transform> targetArr;
    GameObject[] points;
    public bool playerFound = false;
    public float LostPlayerTimer;
    public float PatrolTimer;
    public int PatrolIndex = 0;
    public float distance;
    public char patrolNum;


    void Start()
    {
        target = GameObject.Find("Player");
        patrolNum = name.ToCharArray()[0];
        PatrolTimer = 5f;
        LostPlayerTimer = 5f;

        points = GameObject.FindGameObjectsWithTag("Spawner");
  //      Debug.Log(points.Length);
        targetArr.Clear();

        foreach (GameObject n in points)
        {
            if (n.name.Contains("PatrolPoint") && n.transform.parent.name.ToCharArray()[0] == patrolNum)  //AGreenSlimeSpawner BGreenSLime
            {
                targetArr.Add(n.transform);
   //             Debug.Log("Added " + n.name + "to list");
            }
        }
    }

    void FixedUpdate()
    {
        distance = Vector2.Distance(target.transform.position, transform.position);
        if (playerFound)
        {
            // agent.SetDestination(target.transform.position);   REPLACE WITH MOVE FUNCTION
            MoveTowardsPoint(target.transform.position);
            if (LostPlayerTimer <= 0)
            {
                playerFound = false;
                //  agent.SetDestination(targetArr[0].position);
                MoveTowardsPoint(targetArr[PatrolIndex].position);
            }
            else if (distance > 30f)
            {
                LostPlayerTimer -= Time.deltaTime;
            }
            else
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
                PatrolTimer -= Time.deltaTime;

                if (PatrolIndex < targetArr.Count && targetArr[PatrolIndex] != null)
                {
                    //agent.SetDestination(targetArr[PatrolIndex].position);
                    MoveTowardsPoint(targetArr[PatrolIndex].position);
                    if (PatrolTimer <= 0)
                    {
                        PatrolIndex++;
                        PatrolTimer = PTIME;

                        if (PatrolIndex >= targetArr.Count)
                        {
                            PatrolIndex = 0;
                        }
                    }
                }
            }
        }
    }

    private void PlayerFound()
    {
        playerFound = true;
        LostPlayerTimer = PTIME;
    }


    private void MoveTowardsPoint(Vector3 pos)
    {
        GetComponent<EnemyState>().walkto(pos);
    }
}
