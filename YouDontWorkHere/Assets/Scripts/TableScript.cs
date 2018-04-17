using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour {
    public GameObject ConsumerPrefab;
    public float timerMax;
    public float timerCur;
    public bool spawned = false;    
    private Quaternion baseQaut;


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
                tempPos.y += 4;
                Instantiate(ConsumerPrefab, tempPos, baseQaut, gameObject.transform);//create a consumer above the table
                timerCur = 0;
                spawned = true;
            }
        }
	}
}
