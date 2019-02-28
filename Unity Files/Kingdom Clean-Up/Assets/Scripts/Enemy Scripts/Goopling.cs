using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goopling : MonoBehaviour
{
    [Tooltip("Setting the prefab for what viscera it spawns")]
    public GameObject gooplingPrefab;
    Vector3 pos;
    Quaternion rotation;


  
    public void spawn(float val)
    {
        float x = gameObject.transform.position.x + gameObject.GetComponent<CapsuleCollider2D>().offset.x;
        float y = gameObject.transform.position.y + gameObject.GetComponent<CapsuleCollider2D>().offset.y + 10; //find location of self 
        Vector3 pos = new Vector3(x, y);
        Quaternion rotation = gameObject.transform.rotation;

        Instantiate<GameObject>(gooplingPrefab, pos, rotation );  //Create slime
        Destroy(gameObject); //kill self
        //SlimeViscera.transform.localScale = new Vector3(2.5f, 2.5f, 0);   
    }

}
