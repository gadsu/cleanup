using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  //THIS IS WHAT MAKES THE OBJECT PERSIST

        SceneManager.LoadScene("MainMenu");
    }
}
