﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour {
    public float phaseTimer;
    public int phase = 0;
    SpriteRenderer myRenderer;
    TableScript myTable;
    ConsumerScript myEater;
    Color mood;


    // Use this for initialization
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        //myTable = gameObject.GetComponentInParent<TableScript>();
        myEater = gameObject.GetComponentInParent<ConsumerScript>();
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
    void Update()
    {
        //Debug.Log(myEater.phase);
        if (myEater.phase >= 3)
        {
            //myTable.spawned = false;
            Destroy(gameObject);
        }
    }
}
