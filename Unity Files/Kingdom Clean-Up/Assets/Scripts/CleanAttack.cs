using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanAttack : MonoBehaviour {

    int mopDamage = 10;
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
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyState>().takeDamage(mopDamage);
            Debug.Log("SLIME HIT: " + col.gameObject.name);

        }
        else if(col.gameObject.tag == "slimeInteractable")
        {
            //break the object
            //col.gameObject.GetComponent
        }
    }
    public void swingMop()
    {
        if (!an.GetCurrentAnimatorStateInfo(0).IsName("Swing"))
            an.Play("Swing");
    }
    

}
