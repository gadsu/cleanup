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
    [Tooltip("Slime Meter object")]
    public GameObject greenMeter;
    [Tooltip("Slime Meter object")]
    public GameObject redMeter;
    [Tooltip("Slime Meter object")]
    public GameObject blueMeter;
    [Tooltip("How much health the player has as a float")]
    public float playerHealth = 100f;
    [Tooltip("Can the player be damaged?")]
    public bool isInvuln;
    [Tooltip("Time to be invulnerable")]
    public float invulnTime = 1.00f;
    [Tooltip("Frame the player was damaged on")]
    public float damageFrame;

    public GameObject SlimeMeter;

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
            setSlimeMeterImage(greenSlimeMeter, type);
            //Debug.Log("<color=green>GreenSlimeVal:</color> " + greenSlimeMeter);//tells the debug log that green slime was added to the slime meter
        }

        if ((redSlimeMeter < maxSlime) && type == "red")//Adds red slime to the slime meter
        {
            redSlimeMeter = Mathf.Clamp(redSlimeMeter + val, 0, 100);
            setSlimeMeterImage(redSlimeMeter, type);
            //Debug.Log("<color=red>RedSlimeVal:</color> " + redSlimeMeter);//tells the debug log that red slime was added to the slime meter
        }

        if ((blueSlimeMeter < maxSlime) && type == "blue")//Adds blue slime to the slime meter
        {
            blueSlimeMeter = Mathf.Clamp(blueSlimeMeter + val, 0, 100);
            //blueMeter.value = blueSlimeMeter;
            setSlimeMeterImage(blueSlimeMeter, type);
            //Debug.Log("<color=blue>BlueSlimeVal:</color> " + blueSlimeMeter);//tells the debug log that blue slime was added to the slime meter
        }

    }

    //starting logic for switching the image when slime val gets to certain points

    public void setSlimeMeterImage(int val, string type)
    {

        if (type == "blue")
        {
            SlimeMeter = blueMeter;
        }
        if (type == "red")
        {
            SlimeMeter = redMeter;
        }
        if (type == "green")
        {
            SlimeMeter = greenMeter;
        }

        if (val < 25)
        {
            SlimeMeter.transform.Find("1").gameObject.SetActive(true);
        }
        if (val >= 25 && val < 37)
        {
            SlimeMeter.transform.Find("1").gameObject.SetActive(false);
            SlimeMeter.transform.Find("2").gameObject.SetActive(true);
        }
        if (val >= 37 && val < 50)
        {
            SlimeMeter.transform.Find("2").gameObject.SetActive(false);
            SlimeMeter.transform.Find("3").gameObject.SetActive(true);
        }
        if (val >= 50 && val < 62)
        {
            SlimeMeter.transform.Find("3").gameObject.SetActive(false);
            SlimeMeter.transform.Find("4").gameObject.SetActive(true);
        }
        if (val >= 62 && val < 75)
        {
            SlimeMeter.transform.Find("4").gameObject.SetActive(false);
            SlimeMeter.transform.Find("5").gameObject.SetActive(true);
        }
        if (val >= 75 && val < 87)
        {
            SlimeMeter.transform.Find("5").gameObject.SetActive(false);
            SlimeMeter.transform.Find("6").gameObject.SetActive(true);
        }
        if (val >= 87 && val < 100)
        {
            SlimeMeter.transform.Find("6").gameObject.SetActive(false);
            SlimeMeter.transform.Find("7").gameObject.SetActive(true);
        }
        if (val >= 100)
        {
            SlimeMeter.transform.Find("7").gameObject.SetActive(false);
            SlimeMeter.transform.Find("8").gameObject.SetActive(true);
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
        }
    }

    // Use this for initialization
    void Start () {
        damageFrame = invulnTime;
    }
	
    public void loadScene()
    {
        greenMeter = GameObject.Find("greenMeter");
        redMeter = GameObject.Find("redMeter");
        blueMeter = GameObject.Find("blueMeter");

        player = GameObject.Find("Player");
    }

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("GreenMeter"))
        {
            loadScene();
        }
        if (isInvuln)
        {
            damageFrame -= Time.deltaTime;
            if (damageFrame <= 0)
            {
                damageFrame = invulnTime;
                isInvuln = false;
            }
        }
	}
}
