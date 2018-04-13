using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerScript : MonoBehaviour {
    public float phaseTimer;
    public int phase =0;
    SpriteRenderer myRenderer;
    TableScript myTable;
    Color mood;


	// Use this for initialization
	void Start () {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myTable = gameObject.GetComponentInParent<TableScript>();
        mood.a = 1;
        mood.r = 1;
        phaseTimer = 0;
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
