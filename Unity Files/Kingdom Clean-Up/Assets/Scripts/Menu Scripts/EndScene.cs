using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

    public bool finalLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    

    private void OnTriggerStay2D(Collider2D col)
    {

        if (Input.GetButtonDown("Interact"))
        {
            if(!finalLevel)
            {
                Debug.Log("Next Level");
                //GameObject.Find("DontDestroyOnLoad").GetComponent<PlayerState>().sceneLoaded = false;
                //Move to next level
                Debug.Log(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Debug.Log("You win!");
                // go back to menu or display win screen or some shit idgaf
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

}
