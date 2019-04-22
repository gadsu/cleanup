using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rollie : MonoBehaviour
{
    public int state; // 1 - Idle, 2 - Attack, 3 - Stopping
    public int acceleration;
    public int maxSpeed;
    public int blueSlimes;
    public int health;
    GameObject player;
    Rigidbody2D rb;
    float foundTimer;
    float distance;
    Animator an;
    bool facingRight;
    [Tooltip("Setting the prefab for what viscera it spawns")]
    public GameObject visceraPrefab;
   

    void Awake()
    {
        player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        state = 1;
        an = GetComponent<Animator>();
        facingRight = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= 30 && state == 1)
        {
            state = 2;
        }

        Controller();
        if (transform.position.y < -150)
            Destroy(gameObject);
    }
    void Flip()
    {
        facingRight = !facingRight;
        an.SetBool("facingRight", facingRight);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    void Controller()
    {
        if (state == 1) //Idle animations, etc.
        {
            an.Play("Rollie_Idle");
        }
        else if (state == 2) //ATTACK!!!
        {
            an.Play("Rollie");
            if (transform.position.x <= player.transform.position.x)
            {
                if (!facingRight)
                {
                    Flip();
                }
                rb.AddForce(new Vector2(acceleration, 0));
                Debug.Log("Attack Right");
            }
            else if (transform.position.x > player.transform.position.x)
            {
                if (facingRight)
                {
                    Flip();
                }
                rb.AddForce(new Vector2(-acceleration, 0));
                Debug.Log("Attack Left");
            }
        }
    }
    public void takeDamage(int dmg)
    {
        
            health -= dmg;

            if (health <= 0)
            {
                death();
                //           an.Play("death"); //calls death function at end of animation
            }
        }
    
    
    public void death()
    {
    
        //spawn viscera
        Transform currentPos = gameObject.transform;
        int i = 1;
        while (blueSlimes > 0)
        {
            GameObject SlimeViscera = Instantiate<GameObject>(visceraPrefab, currentPos.position, currentPos.rotation);
            SlimeViscera.transform.localScale = new Vector3(2.5f, 2.5f, 0);

            Vector2 vel = new Vector2(30f, 15f);
            if (i % 3 == 0)
            {
                vel.x *= 0;
            }
            else if (i % 3 == 1)
            {
                vel.x *= -1;
            }

            SlimeViscera.GetComponent<ItemInteraction>().setVelocity(vel);
            i++;
            blueSlimes--;
        }

        // Die
        Debug.Log("i should die now");
        Destroy(gameObject);
        // Destroy(gameObject.transform.parent.gameObject); //trying to get the parent to die 
    }
}
