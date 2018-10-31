
//    Clean Attack
//    Controls the events that will happen when someone actually tries to hit someone
//    Will need to be updated with new animations

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanAttack : MonoBehaviour {

    int mopDamage = 10;  //How much damage does the attack do?
    Animator an;

    // Use this for initialization
    void Start() {
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D col)  
    {
        Debug.Log(col.gameObject.tag.ToString());
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")  //If you are hitting an enemy
        {
            col.gameObject.GetComponent<EnemyState>().takeDamage(mopDamage); //
            Debug.Log("SLIME HIT: " + col.gameObject.name);

        }
        else if(col.gameObject.tag == "slimeInteractable") //If you are hitting placed slime
        {
            //break the object
            col.gameObject.GetComponent<SlimeConstructs>().breakSlime();
        }
    }

    public void swingMop()
    {
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("Swing"))  //If you are not already playing the animation, play it
            an.Play("Swing");
    }
    

}
