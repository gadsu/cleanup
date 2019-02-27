using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopaMother : MonoBehaviour
{
    System.Random rnd = new System.Random();
    public List<GameObject> gooplings = new List<GameObject>();
    int i = 0;
    public void spawnGoopling()
    {
        gooplings[i].GetComponent<Goopling>().spawn();
        i++;
    }
    
}
