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
    bool eating;


    // Use this for initialization
    void Start () {
        eating = false;
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
	}
	
	// Update is called once per frame
	void Update () {
        phaseTimer += Time.deltaTime;
        if(phaseTimer > 4)
        {
            phaseTimer = 0;
            phase += 1;
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
            Instantiate(food, new Vector3(transform.position.x - 1.5f, transform.position.y - 1f, -1.0f), baseQaut, gameObject.transform);
            mood.b = (1.0f - (phaseTimer / 4.0f));
            mood.r = (1.0f - (phaseTimer / 4.0f));
            mood.g = 1;

            myRenderer.color = mood;
        }
        if (phase >= 3)
        {
            myTable.spawned = false;
            Destroy(gameObject);
        }


    }
}
