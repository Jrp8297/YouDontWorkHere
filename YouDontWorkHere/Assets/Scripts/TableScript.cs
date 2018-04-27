using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour {
    public GameObject ConsumerPrefab;
    public GameObject FoodPrefab;
    public BreakRoom WaiterZone;
    public float timerMax;
    public float timerCur;
    public bool spawned = false;    
    private Quaternion baseQaut;
    public bool eating = true;
    public GameObject [] myFlags; // store the directions to get to this table
    public int tableNum;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawned){//tick the clock if we can spawn stuff

            timerCur += Time.deltaTime;//tick clock

            if (timerCur > timerMax)
            {//compare to max value
                Vector3 tempPos = gameObject.transform.position;
                tempPos.x += 1.5f;
                tempPos.y += 1;
                Instantiate(ConsumerPrefab, tempPos, baseQaut, gameObject.transform);//create a consumer above the table
                /*if (eating == true)
                {
                    Instantiate(FoodPrefab, new Vector3(tempPos.x - 1.5f, tempPos.y - 1f, -1.0f), baseQaut, gameObject.transform);
                    eating = false;
                }*/
                timerCur = 0;
                spawned = true;
            }
        }
	}

    public void SendRequest()
    {
        WaiterZone.RequestAdd(this);
    }
}
