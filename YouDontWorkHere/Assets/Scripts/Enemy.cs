using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    public SightLine mySight;

	//Where the enemy wants to move to next (seek)
    public GameObject [] flags;

    public int curNumber = 0;
    float timer = 0.0f;
    Vector3 direction;

    float lockPos = 0.0f;
    float rotation = 0.0f;

    public float maxSpeed = 1.0f;

    public Sprite [] moveSprites;
    public enum EnemyState { Idle, Seeking, Serving, Returning};
    public EnemyState myState;
    
    
   
    // Use this for initialization
    void Start()
    {
        myState = EnemyState.Idle;        
    }

    // Update is called once per frame
    void Update()
    {

        switch (myState)
        {
            case EnemyState.Idle:
                //This server is in their little station. Have them run idle animations.
                break;

            case EnemyState.Seeking:
                direction = Vector3.ClampMagnitude(flags[curNumber].transform.position - gameObject.transform.position, maxSpeed);
                Debug.Log(flags[curNumber].transform.position);
                if(direction.magnitude <= .5f)
                {//if we are close enough to the next flag
                    if(curNumber == flags.Count-1)
                    {// if its the last flag, go to our Serving state
                        myState = EnemyState.Serving;
                        timer = 1.0f;
                    }
                    else
                    {//otherwise, just incriment the current index of the flags array
                        curNumber++;

                    }
                }
                gameObject.transform.position += direction * Time.deltaTime;
                //This server is heading towards a customer. They will follow their path in current order.
                break;

            case EnemyState.Serving:
                timer -= Time.deltaTime;
                //This server is actively interfacing with a customer, Have them idle for about a second for now.
                if(timer <= 0)
                {
                    myState = EnemyState.Returning;
                    curNumber = flags.Count;
                }
                break;
            case EnemyState.Returning:
                if(curNumber >= flags.Count)
                {
                    curNumber = flags.Count - 1; 
                }
                //This server has finished their task and is returning to their station.
                //go through your Flags in reverse order.

                direction = Vector3.ClampMagnitude(flags[curNumber].transform.position - gameObject.transform.position , maxSpeed);

                if (direction.magnitude <= .5f)
                {//if we are close enough to the next flag
                    if (curNumber == 0)
                    {// if its the last flag, go to our idle  state
                        myState = EnemyState.Idle;
                    }
                    else
                    {//otherwise, just incriment the current index of the flags array
                        curNumber--;

                    }
                }
                gameObject.transform.position += direction * Time.deltaTime;
                break;
        }

        //assign sprite base on direction of movement.
        if (direction.x > .5f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[3];
            mySight.offsetAngle = 270;
        }
        else if (direction.x < -.5f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[2];
            mySight.offsetAngle = 90;
        }
        else if (direction.y > .5f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[0];
            mySight.offsetAngle = 0;
        }
        else if (direction.y < -.5f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[1];
            mySight.offsetAngle = 180;
        }
    }

  
}
