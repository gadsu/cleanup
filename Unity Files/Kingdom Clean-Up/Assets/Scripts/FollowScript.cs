using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{

    NavMeshAgent agent;
    
    public Transform target;
    public Transform[] targetArr;
    private bool playerFound = true;
    public float LostPlayerTimer = 5f;
    public float PatrolTimer = 5f;
    public int PatrolIndex = 0;
    

    void Start()
    {

    }

    void Update()
    {

        

        if (playerFound == true)
        {
            LostPlayerTimer -= Time.deltaTime;
            
        }
        
        
        if (LostPlayerTimer <= 0)
        {
            playerFound = false;
            
        }
        
        if (!playerFound)
        {
            PatrolTimer -= Time.deltaTime;
        }
        else if (target != null)
        {
            agent.SetDestination(target.position);
        }
        

        if (PatrolTimer <= 5 && playerFound == false && PatrolIndex < targetArr.Length && targetArr[PatrolIndex] != null)
        {
            agent.SetDestination(targetArr[PatrolIndex].position);
            if (PatrolTimer <= 0)
            {
                PatrolIndex++;
                PatrolTimer = 5;
               
                if (PatrolIndex == targetArr.Length)
                {
                    PatrolIndex = 0;
                }
            }
        }
   
    }

    private void PlayerFound(GameObject player)
    {
        playerFound = true;
        target = player.transform;
        LostPlayerTimer = 5f;
    }


}
