using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    public static States instance;

    public int currentSceneID;

    // ------------------ Scene Playground Layout ------------------
    public bool gateOpen = false;

    // ------------------ Scene Inside Playground ------------------


    // ------------------ Scene Village ------------------
    public bool collarGiven = false;

    // ------------------ Scene Forest ------------------
    public bool leafsPlaced = false;
    public bool dogRopeCut = false;
    public bool ballThrown = false;
    public bool FirstSoulQuest = false;

    // ------------------ Scene Cave ------------------
    public bool crowOnSpider = false;

    // ------------------ Scene Room ------------------
    public bool maketaOnMonster = false;
    public bool correctPasscode = false;

    // ------------------ Scene Village ------------------
    public bool bonesOnGirl = false;


    // ------------------ Soul 2 ------------------
    public bool dressPickedUp = false;
    public bool dressGiven = false;
    public bool musicBoxGiven = false;
    public bool SecondSoulQuest = false;



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        // Get the current scene ID
        currentSceneID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    public void NewGame()
    {
        // Reset all states
        States.instance.gateOpen = false;
        States.instance.leafsPlaced = false;
        States.instance.dogRopeCut = false;
        States.instance.ballThrown = false;
        States.instance.FirstSoulQuest = false;
        States.instance.collarGiven = false;
    }
}
