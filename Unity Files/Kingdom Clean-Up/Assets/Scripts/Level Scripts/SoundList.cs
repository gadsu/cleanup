using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundList : ScriptableObject
{
    public List<SoundItem> Sound;
    public AudioSource _source;
}
