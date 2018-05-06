using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerScript : MonoBehaviour {
    public float phaseTimer;
    public int phase =0;
    SpriteRenderer myRenderer;
    TableScript myTable;
    public GameObject food;
    Color mood;
    private Quaternion baseQaut;
    bool hasPlate;
    bool eating;
	public bool gaveOrder;
    bool requestSent = false;
    public bool Idling = false;
	public bool switchPhase = false;


    // Use this for initialization
    void Start () {
        eating = false;
		gaveOrder = false;
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myTable = gameObject.GetComponentInParent<TableScript>();
        mood.a = 1;
        mood.r = 1;
        phaseTimer = 0;

		//Phases the customer is at
		//0 is first seated waiting to get their order taken
		//1 is waiting for food.
		//2 Eating 
		//3 Done eating, need to get check collected and food cleared.
        phase = 0;
        Idling = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (switchPhase == true) {
			changePhase ();
		}

        phaseTimer += Time.deltaTime;

        if (phaseTimer > 4)
        {
            if (!Idling && phaseTimer < 8)
            {//prevent characters from just moving through various levels
				if (phase == 0 || phase == 1) {
					Idling = true;
				} else {
					phaseTimer = 0;
					phase += 1;
				}
            }
            else if(phaseTimer > 8 && !requestSent)
            {//If we have waited to long to get our stuff.
                myTable.SendRequest();
                requestSent = true;
            }
            else if (!Idling)
            {                
                phaseTimer = 0;
                phase += 1;
            }
          
        }


        if (phase == 0)
        {
            mood.b = (1.0f - (phaseTimer / 4.0f));
            mood.g = (1.0f - (phaseTimer / 4.0f));

            myRenderer.color = mood;
        }
        if (phase == 1)
        {
            mood.b = 1;
            mood.r = (1.0f - (phaseTimer / 4.0f));
            mood.g = (1.0f - (phaseTimer / 4.0f));

            myRenderer.color = mood;
        }
        if (phase == 2)
        {
			//This is made to make sure there isn't a continous amount of plates being instantiated.
            if(hasPlate == false)
            {
                Instantiate(food, new Vector3(transform.position.x - 1.5f, transform.position.y - 1f, -1.0f), baseQaut, gameObject.transform);
                hasPlate = true;
            } 

            mood.b = (1.0f - (phaseTimer / 4.0f));
            mood.r = (1.0f - (phaseTimer / 4.0f));
            mood.g = 1;

            myRenderer.color = mood;
        }
        if (phase >= 3)
        {
            hasPlate = false;
            myTable.spawned = false;
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
			changePhase();
        }


    }

    public void changePhase()
    {
            phase++;
            phaseTimer = 0;
			switchPhase = false;
    }
}
