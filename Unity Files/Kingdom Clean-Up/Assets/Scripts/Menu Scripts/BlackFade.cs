using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeScreenInName(string name)
    {
        gameObject.GetComponent<Animator>().Play("BlackFadeIn");

    }
}
