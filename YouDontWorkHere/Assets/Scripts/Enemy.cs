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

    public float maxSpeed;

    public Sprite [] moveSprites;
    public enum EnemyState { Idle, Seeking, Serving, Returning};
    public EnemyState myState;

	//FUTURE Orders held? Like if the player can take like 5 orders at once
	int ordersHeld = 0;
	const int MAX_ORDERS = 5;
	public int orderNum = 0; // which table he's taken an order from; 0 = not a table
    
	//Is the enemy holding food? Assumes Enemy can only hold one Table order at a time.
	public bool hasFood;
	//Is the enemy holding an order already?
	public bool hasOrder;
   
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
                direction = Vector3.ClampMagnitude(flags[curNumber].transform.position - gameObject.transform.position, maxSpeed * Time.deltaTime);
                //Debug.Log(flags[curNumber].transform.position);
                if(direction.magnitude <= .035f)
                {//if we are close enough to the next flag
                    if(curNumber == flags.Length - 1)
                    {// if its the last flag, go to our Serving state
                        myState = EnemyState.Serving;
                        timer = 1.0f;
                    }
                    else
                    {//otherwise, just incriment the current index of the flags array
                        curNumber++;

                    }
                }
                gameObject.transform.position += direction ;
                //This server is heading towards a customer. They will follow their path in current order.
                break;

            case EnemyState.Serving:
                timer -= Time.deltaTime;
                //This server is actively interfacing with a customer, Have them idle for about a second for now.
                if(timer <= 0)
                {
                    myState = EnemyState.Returning;
                    curNumber = flags.Length;
                }
                break;
            case EnemyState.Returning:
                if(curNumber >= flags.Length)
                {
                    curNumber = flags.Length - 1; 
                }
                //This server has finished their task and is returning to their station.
                //go through your Flags in reverse order.

                direction = Vector3.ClampMagnitude(flags[curNumber].transform.position - gameObject.transform.position , maxSpeed * Time.deltaTime);

                if (direction.magnitude <= .035f)
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
                gameObject.transform.position += direction;
                break;
        }

        //assign sprite base on direction of movement.
        if (direction.x > .1f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[3];
            mySight.offsetAngle = 270;
        }
        else if (direction.x < -.1f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[2];
            mySight.offsetAngle = 90;
        }
        else if (direction.y > .1f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[0];
            mySight.offsetAngle = 0;
        }
        else if (direction.y < -.1f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[1];
            mySight.offsetAngle = 180;
        }
    }

	//Check for collisions with Tables and Such
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Grab the object colliding with the enemy
		GameObject collided = collision.gameObject;

		//Check if they are an enemy, Table, or Kithen
		if (collided.tag == "Enemy") {
			//Never going to happen currently

		} else if (collided.tag == "Table") {
			//This is simply testing purposes
			//If player doesn't have order or food in hands, grab order, if play has food, give food
			if (!hasOrder && !hasFood && collided.GetComponentInChildren<ConsumerScript>() != null && orderNum == 0) {
				if (collided.GetComponentInChildren<ConsumerScript>().phase == 1)
				{
					hasOrder = true;
					Debug.Log("Took Order");
					orderNum = collided.GetComponent<TableScript>().tableNum;
					//collided.GetComponentInChildren<ConsumerScript>().Idling = false;
				}
			} else if (hasFood && orderNum == collided.GetComponent<TableScript>().tableNum) {
				hasFood = false;
				if (collided.GetComponentInChildren<ConsumerScript>() != null)
				{
					collided.GetComponentInChildren<ConsumerScript>().Idling = false;
				}
				Debug.Log("Gave Food");
				orderNum = 0;
			} 

		} else if (collided.tag == "Kitchen") {
			//This is assuming the player only has one order
			if (hasOrder == true) {
				hasOrder = false;
				Debug.Log("Kitchen Gave Food");
				//This is assuming the kitchen doesn't need to prepare the food currently.
				hasFood = true;
			} 
		}
	}
  
}
