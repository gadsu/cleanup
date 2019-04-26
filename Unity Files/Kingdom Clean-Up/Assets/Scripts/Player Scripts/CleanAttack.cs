
//-------------COMPLETE DO NOT TOUCH----------//


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
        //Debug.Log(col.gameObject.tag.ToString());
        if (gameObject.name == "mopRun") //Mop Run Attack
        {
            if (col.gameObject.tag == "Enemy" )  //Can oly hit basic enemy's not bosses
            {
                col.gameObject.GetComponent<EnemyState>().takeDamage(mopDamage / 2); //
                Debug.Log("SLIME HIT: " + col.gameObject.name);

            }
        }
        else //Normal Mop Attack
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")  //If you are hitting any enemy
            {
                col.gameObject.GetComponent<EnemyState>().takeDamage(mopDamage); //
                Debug.Log("SLIME HIT: " + col.gameObject.name);

            }
            else if(col.gameObject.tag == "GoopMother")
            {
                col.gameObject.GetComponent<GoopaMother>().spawnGoopling();
                col.gameObject.GetComponent<EnemyState>().takeDamage(mopDamage);
            }
            if (col.gameObject.tag == "Rollie")  //If you are hitting rollie
            {
                Debug.Log("SLIME HIT: " + col.gameObject.name);
                col.gameObject.GetComponent<EnemyState>().takeDamage(mopDamage);
            }
        }
    }

    public void swingMop()
    {
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("Swing")) //If you are not already playing the animation, play it
        {
            an.Play("Swing");
            GameObject.Find("DontDestroyOnLoad").GetComponent<PlaySound>().Play("mopHit");
        }
    }
    

}
