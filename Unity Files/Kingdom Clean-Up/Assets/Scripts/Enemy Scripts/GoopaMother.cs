using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoopaMother : MonoBehaviour
{
    public List<GameObject> gooplings = new List<GameObject>();
    [Tooltip("Setting the prefab for what viscera it spawns")]
    public GameObject gooplingPrefab;

    public void takeDamage(int dmg)
    {

        GameObject SlimeViscera = Instantiate<GameObject>(gooplingPrefab);//, currentPos.position, currentPos.rotation);
        SlimeViscera.transform.localScale = new Vector3(2.5f, 2.5f, 0);
        
    }
    
       


    

}
