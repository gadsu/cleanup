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
    public float invulnTime = 1;
    [Tooltip("Frame the player was damaged on")]
    public float damageFrame = 1;

    public GameObject CleanProgressBar;
    public string SceneName;
    public int groundSlimeMax;
    public int groundSlimeCleaned;
    public bool sceneLoaded;

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
        Debug.Log("Getting Slime");
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
    public bool useSlime()
    {
        Debug.Log("Using Slime");
        if(greenSlimeMeter >= 10 )
        {
            greenSlimeMeter -= 10;
            setSlimeMeterImage(greenSlimeMeter, greenChildren);
            return true;
        }
        else
        {
            return false;
        }
    }

    //starting logic for switching the image when slime val gets to certain points

    public void setSlimeMeterImage(int val, List<GameObject> SlimeMeter)
    {
        if (GameObject.Find("greenMeter"))
        {
            if (val < 25)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[0].SetActive(true);
            }
            if (val >= 25 && val < 37)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[1].gameObject.SetActive(true);
            }
            if (val >= 37 && val < 50)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[2].SetActive(true);
            }
            if (val >= 50 && val < 62)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[3].SetActive(true);
            }
            if (val >= 62 && val < 75)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[4].SetActive(true);
            }
            if (val >= 75 && val < 87)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[5].SetActive(true);
            }
            if (val >= 87 && val < 100)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[6].SetActive(true);
            }
            if (val >= 100)
            {
                disableSlimeMeters(SlimeMeter);
                SlimeMeter[7].SetActive(true);
            }
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
        sceneLoaded = false;
        groundSlimeCleaned = 0;
        groundSlimeMax = 0;
    }
	
    public void loadScene()
    {
        int i;
        greenChildren.Clear();
        redChildren.Clear();
        blueChildren.Clear();
        greenMeter = GameObject.Find("greenMeter");
        redMeter = GameObject.Find("redMeter");
        blueMeter = GameObject.Find("blueMeter");
        CleanProgressBar = GameObject.Find("CleanProgress");

        if (GameObject.Find("greenMeter"))
        {
            for (i = 1; i <= 8; i++)
            {
                greenChildren.Add(greenMeter.transform.Find(i.ToString()).gameObject);
                redChildren.Add(redMeter.transform.Find(i.ToString()).gameObject);
                blueChildren.Add(blueMeter.transform.Find(i.ToString()).gameObject);
            }
        }

        //CleanProgressBar = null;
        groundSlimeMax = 0;
        groundSlimeCleaned = 0;
        CountGroundSlime();
        setSlimeMeterImage(0, greenChildren);
        setSlimeMeterImage(0, blueChildren);
        setSlimeMeterImage(0, redChildren);
        player = GameObject.Find("Player");
        playerHealth = 100f;
    }

    public void CountGroundSlime()
    {
        List<GameObject> groundSlimes = GameObject.FindGameObjectsWithTag("slimeObject").ToList<GameObject>();
        List<GameObject> slimeSpawners = GameObject.FindGameObjectsWithTag("Spawner").ToList<GameObject>();
        Debug.Log("Slime Count: " + groundSlimes.Count + ", Spawner Count: " + slimeSpawners.Count);

        if (groundSlimeMax == 0 || groundSlimes.Count + groundSlimeCleaned > groundSlimeMax)
            groundSlimeMax = groundSlimes.Count;

        foreach (GameObject spawner in slimeSpawners)
        {
            if (spawner)
                groundSlimeMax += spawner.GetComponent<SlimeSpawner>().totalSlime * 3;
            else
                Debug.Log("SPAWNER NOT FOUND");
        }

        updateCleanProgress();
    }

    public void updateCleanProgress()
    {
        if (!CleanProgressBar)
            CleanProgressBar = GameObject.Find("CleanProgress");

        float slimeCleaned = Mathf.Round(((float)groundSlimeCleaned / (float)groundSlimeMax) * 100);
        CleanProgressBar.GetComponent<Slider>().value = slimeCleaned;
        CleanProgressBar.GetComponentInChildren<Text>().text = CleanProgressBar.GetComponent<Slider>().value.ToString() + "%";
    }

    public void disableSlimeMeters(List<GameObject> meter)
    {
        int size = meter.Count;

        for(int i = 0; i < size; i++)
        {
            meter[i].SetActive(false);
        }
    }
    

	// Update is called once per frame
	void Update () {
        if (GameObject.Find("greenMeter") && sceneLoaded == false && !SceneName.Equals("_DontDestroyOnLoad"))
        {
            Debug.Log("LOAD SCENE");
            loadScene();
            sceneLoaded = true;
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
