using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisObjects : MonoBehaviour {

    public GameObject BossSpawnerObject;

    //turns on OBSOOO spawner
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("HE RAN THROUGH ME");
        if (col.gameObject.tag == "Player")
        {
            ShowObject();
        }
    }
    //turns on invisWalls
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    //shows Spawner
    public void ShowObject()
    {
        BossSpawnerObject.SetActive(true);
        Debug.Log("Spawner is on");
    }

    //hides Spawner
    public void HideObject()
    {

        BossSpawnerObject.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        BossSpawnerObject = GameObject.Find("BossSlimeSpawner");
        HideObject();
	}
}
