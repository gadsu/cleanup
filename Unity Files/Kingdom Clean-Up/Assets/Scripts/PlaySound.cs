using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public List<AudioClip> aclips = new List<AudioClip>();
    AudioSource source;
    //1
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play(string soundName)
    {
        switch (soundName)
        {
            case "mopHit":
                source.PlayOneShot(aclips[0]);
                break;
            case "garbageCan":
                source.PlayOneShot(aclips[1]);
                break;
            case "cleaningEffect":
                source.PlayOneShot(aclips[2]);
                break;
        }
    }
}
