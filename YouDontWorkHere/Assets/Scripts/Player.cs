using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	Vector3 position;
	Vector3 velocity;
	float speed = .1f;
	public GameObject player;
    public Sprite[] moveSprites;

    //At the moment, food and orders do not discriminate between tables. This restraunt only serves one item

    //Is the player holding an order already?
    public bool hasOrder;

	//FUTURE Orders held? Like if the player can take like 5 orders at once
	int ordersHeld = 0;
    const int MAX_ORDERS = 5;
    public int orderNum = 0; // which table he's taken an order from; 0 = not a table

    //Is the player holding food? Assumes Player can only hold one Table order at a time.
    public bool hasFood;

	bool isColliding = false;

	//If the player is found by waiter, gameOver
	//Check SightLine.cs to see how this variable is changed.
	public bool  foundByWaiter = false;

	//Initialization
	void Start () {
		hasOrder = false;
		hasFood = false;
	}
	
	// Update is called once per frame

	//Currently Update() only contains the functionality to move the player and check for collision with the Waiter's Field of View
	void Update () {
		//Get position and modify it by the velocity that is gotten from User's key inputs
		position = player.transform.position;
		velocity = new Vector3(0,0,0);

		if (Input.GetKey(KeyCode.A))
		{
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[0];
			velocity.x = -speed;
		}
		if (Input.GetKey(KeyCode.D))
		{
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[2];
            velocity.x = speed;
		}
		if (Input.GetKey(KeyCode.S))
		{
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[3];
            velocity.y = -speed;
		}
		if (Input.GetKey(KeyCode.W))
		{
            gameObject.GetComponent<SpriteRenderer>().sprite = moveSprites[1];
            velocity.y = speed;
		}

		position += velocity;
		player.transform.position = position;

		//Check if player is colliding with 
		checkCollision ();
		

	}

	//Check for collisions with Tables and Such
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Grab the object collidiong with the player
		GameObject collided = collision.gameObject;

		//Check if they are an enemy, Table, or Kithen
		if (collided.tag == "Enemy") {
			//Do nothing for now

		} else if (collided.tag == "Table") {
			//This is simply testing purposes
			//If player doesn't have order or food in hands, grab order, if play has food, give food
			if (!hasOrder && !hasFood && collided.GetComponentInChildren<ConsumerScript>() != null && orderNum == 0) {
                if (collided.GetComponentInChildren<ConsumerScript>().phase == 0)
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

	void checkCollision (){
		if (foundByWaiter == true) {
            SceneManager.LoadScene("Gameover");
		}
	}
}
