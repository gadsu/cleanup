using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goopling : MonoBehaviour
{
    [Tooltip("Setting the prefab for what viscera it spawns")]
    public GameObject gooplingPrefab;

    public void spawn()
    {
        Transform startpos = gameObject.transform; //find location of self
        Instantiate<GameObject>(gooplingPrefab, startpos.position, startpos.rotation);  //Create slime
        Destroy(gameObject); //kill self
                             //SlimeViscera.transform.localScale = new Vector3(2.5f, 2.5f, 0);

    }

}
