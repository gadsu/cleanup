using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBSOOO : MonoBehaviour {

    GameObject[] points;
    GameObject target = null;  //Will be the player
    public List<Transform> targetArr;
    public int hitCount = 0;
    public static float PTIME = 4f;

    public void bigjump()
    {

    }

    public void specialAttack()
    {

    }

    public void basicJump()
    {

    }

    public void movementController()
    {

    }

	// Use this for initialization
	void Start ()
    {
        target = GameObject.Find("Player");    //Find the player

        points = GameObject.FindGameObjectsWithTag("Spawner");   //Find all of the spawner objects in the scene
        targetArr.Clear();

        //Cycle through all spawner objects and only add the ones that match our character
        foreach (GameObject n in points)
        {
            if (n.name.Contains("PatrolPoint") && n.transform.parent.name.Contains("Boss"))
            {
                targetArr.Add(n.transform);
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    private void MoveTowardsPoint(Vector3 pos)
    {
        GetComponent<EnemyState>().walkto(pos); 
    }
}
