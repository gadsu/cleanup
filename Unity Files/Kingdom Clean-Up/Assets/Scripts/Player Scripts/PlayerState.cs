using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour {
    Dictionary<string, string> saveDic;

    [Header("Debug Variables")]
    [Tooltip("How much green slime?")]
    public int greenSlimeMeter;
    [Tooltip("How much red slime?")]
    public int redSlimeMeter;
    [Tooltip("How much blue slime?")]
    public int blueSlimeMeter;
    [Tooltip("Slider object")]
    public Slider greenMeter;
    [Tooltip("Slider object")]
    public Slider redMeter;
    [Tooltip("Slider object")]
    public Slider blueMeter;
    [Tooltip("How much health the player has as a float")]
    public float playerHealth = 100f;
    [Tooltip("Can the player be damaged?")]
    public bool isInvuln;
    [Tooltip("Frame the player was damaged on")]
    public float damageFrame;


    GameObject player;
    int maxSlime = 100;

    //public TextAsset PlayerFile; DOES NOT WORK FOR SOME RAISIN

    public void loadData(string playernum)
    {
        TextAsset PlayerFile = new TextAsset();
        PlayerFile = Resources.Load("SaveFile" + playernum) as TextAsset;
    }

    public void addSlime(int val, string type)// Adds slime to the slime meter
    {
        if ((greenSlimeMeter < maxSlime) && type == "green")//Adds green slime to the slime meter
        {
            greenSlimeMeter = Mathf.Clamp(greenSlimeMeter + val, 0, 100);
            greenMeter.value = greenSlimeMeter;
            //Debug.Log("<color=green>GreenSlimeVal:</color> " + greenSlimeMeter);//tells the debug log that green slime was added to the slime meter
        }

        if ((redSlimeMeter < maxSlime) && type == "red")//Adds red slime to the slime meter
        {
            redSlimeMeter = Mathf.Clamp(redSlimeMeter + val, 0, 100);
            redMeter.value = redSlimeMeter;
            //Debug.Log("<color=red>RedSlimeVal:</color> " + redSlimeMeter);//tells the debug log that red slime was added to the slime meter
        }

        if ((blueSlimeMeter < maxSlime) && type == "blue")//Adds blue slime to the slime meter
        {
            blueSlimeMeter = Mathf.Clamp(blueSlimeMeter + val, 0, 100);
            blueMeter.value = blueSlimeMeter;
            //Debug.Log("<color=blue>BlueSlimeVal:</color> " + blueSlimeMeter);//tells the debug log that blue slime was added to the slime meter
        }
    }

    public void takeDamage(float dmg)
    {
        if (!isInvuln)
        {
            playerHealth -= dmg;
            GameObject.Find("Health").GetComponent<Slider>().value = playerHealth;
            if (playerHealth <= 0)
            {
                Debug.Log("YOU DIED :(");
                //an.Play("death"); //calls death function at end of animation
                GameObject.Find("UI Canvas").GetComponent<KillScreen>().KillScreenControl();
                playerHealth = 100;
            }
            isInvuln = true;
            damageFrame = Time.deltaTime;
        }
    }

    // Use this for initialization
    void Start () {

    }
	
    public void loadScene()
    {
        greenMeter = GameObject.Find("GreenMeter").GetComponent<Slider>();
        redMeter = GameObject.Find("RedMeter").GetComponent<Slider>();
        blueMeter = GameObject.Find("BlueMeter").GetComponent<Slider>();

        player = GameObject.Find("Player");
    }

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("GreenMeter"))
        {
            loadScene();
        }
        if (isInvuln && damageFrame <= Time.deltaTime + 0.5f)
        {
            isInvuln = false;
        }
	}
}
