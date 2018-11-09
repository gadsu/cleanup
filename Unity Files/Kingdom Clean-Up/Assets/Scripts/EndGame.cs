using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public string scene;
    GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x > gameObject.transform.position.x)
        {
            finish(scene);
        }
	}

    void finish(string name)
    {
        SceneManager.LoadScene(name);
    }

}
