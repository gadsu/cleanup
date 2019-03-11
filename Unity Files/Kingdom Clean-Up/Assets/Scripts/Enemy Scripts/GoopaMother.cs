using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopaMother : MonoBehaviour
{
    System.Random rnd = new System.Random();
    public List<GameObject> gooplings = new List<GameObject>();
    int rightCount= 6;
    int leftCount = 0;
    public float leftVal;
    public float rightVal;

    bool facingRight;

    public void spawnGoopling()
    {
        facingRight = gameObject.GetComponent<EnemyState>().facingRight;

        if (facingRight && rightCount < 10)
        {
            gooplings[leftCount].GetComponent<Goopling>().spawn(leftVal);
            leftCount++;
        }
        else if (!facingRight && leftCount < 6)
        {
            gooplings[rightCount].GetComponent<Goopling>().spawn(rightVal);
            rightCount++;
        }
        

    }
    
}
