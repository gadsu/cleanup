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
    [Tooltip("List of the slime meter's children")]
    public List<GameObject> greenChildren = new List<GameObject>();
    [Tooltip("List of the slime meter's children")]
    public List<GameObject> redChildren = new List<GameObject>();
    [Tooltip("List of the slime meter's children")]
    public List<GameObject> blueChildren = new List<GameObject>();
    [Tooltip("How much health the player has as a float")]
    public float playerHealth = 100f;
    [Tooltip("Can the player be damaged?")]
    public bool isInvuln;
    [Tooltip("Time to be invulnerable")]
    public float invulnTime = 1.00f;
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
            setSlimeMeterImage(greenSlimeMeter, greenChildren);
            //Debug.Log("<color=green>GreenSlimeVal:</color> " + greenSlimeMeter);//tells the debug log that green slime was added to the slime meter
        }

        if ((redSlimeMeter < maxSlime) && type == "red")//Adds red slime to the slime meter
        {
            redSlimeMeter = Mathf.Clamp(redSlimeMeter + val, 0, 100);
            setSlimeMeterImage(redSlimeMeter, redChildren);
            //Debug.Log("<color=red>RedSlimeVal:</color> " + redSlimeMeter);//tells the debug log that red slime was added to the slime meter
        }

        if ((blueSlimeMeter < maxSlime) && type == "blue")//Adds blue slime to the slime meter
        {
            blueSlimeMeter = Mathf.Clamp(blueSlimeMeter + val, 0, 100);
            //blueMeter.value = blueSlimeMeter;
            setSlimeMeterImage(blueSlimeMeter, blueChildren);
            //Debug.Log("<color=blue>BlueSlimeVal:</color> " + blueSlimeMeter);//tells the debug log that blue slime was added to the slime meter
        }

    }

    //starting logic for switching the image when slime val gets to certain points

    public void setSlimeMeterImage(int val, List<GameObject> SlimeMeter)
    {
        if (val < 25)
        {
            SlimeMeter[0].SetActive(true);
        }
        if (val >= 25 && val < 37)
        {
            SlimeMeter[0].SetActive(false);
            SlimeMeter[1].gameObject.SetActive(true);
        }
        if (val >= 37 && val < 50)
        {
            SlimeMeter[1].SetActive(false);
            SlimeMeter[2].SetActive(true);
        }
        if (val >= 50 && val < 62)
        {
            SlimeMeter[2].SetActive(false);
            SlimeMeter[3].SetActive(true);
        }
        if (val >= 62 && val < 75)
        {
            SlimeMeter[3].SetActive(false);
            SlimeMeter[4].SetActive(true);
        }
        if (val >= 75 && val < 87)
        {
            SlimeMeter[4].SetActive(false);
            SlimeMeter[5].SetActive(true);
        }
        if (val >= 87 && val < 100)
        {
            SlimeMeter[5].SetActive(false);
            SlimeMeter[6].SetActive(true);
        }
        if (val >= 100)
        {
            SlimeMeter[6].SetActive(false);
            SlimeMeter[7].SetActive(true);
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
        int i;
        greenMeter = GameObject.Find("greenMeter");
        redMeter = GameObject.Find("redMeter");
        blueMeter = GameObject.Find("blueMeter");

        if(greenChildren.Count == 0)
        {
            for (i = 1; i <= 8; i++)
            {
                greenChildren.Add(greenMeter.transform.Find(i.ToString()).gameObject);
                redChildren.Add(redMeter.transform.Find(i.ToString()).gameObject);
                blueChildren.Add(blueMeter.transform.Find(i.ToString()).gameObject);
            }
        }

        player = GameObject.Find("Player");
    }
    

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("greenMeter"))
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
