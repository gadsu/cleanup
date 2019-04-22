
//    ThrowParticle
//    Will need to be updated

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowParticle : MonoBehaviour {

    public Transform initialpos;
    Rigidbody2D rb;
    AimRender ar;

	// Use this for initialization
	void Start () {
        ar = GetComponentInParent<AimRender>();
        initialpos = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(11, 2);
        Physics2D.IgnoreLayerCollision(11, 9);
        Physics2D.IgnoreLayerCollision(11, 10);
  //      Physics.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Rigidbody2D>().collider);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (ar.inAir)
        {
            ar.AddSpot(transform);
        }
        else
        {
            transform.position = initialpos.position;
        }
	}

    public void Throw(Vector2 dir)
    {
        transform.position = initialpos.position;
        rb.velocity = dir;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Platform")
        {
            ar.DrawLine(transform);
            ar.inAir = false;
        }
    }
}
