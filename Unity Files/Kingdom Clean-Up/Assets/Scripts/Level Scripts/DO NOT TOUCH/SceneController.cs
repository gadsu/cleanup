using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("DontDestroyOnLoad").GetComponent<GameData>().ReloadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
