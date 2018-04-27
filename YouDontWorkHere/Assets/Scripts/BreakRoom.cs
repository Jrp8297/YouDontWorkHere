using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoom : MonoBehaviour {


    public Queue <TableScript> pendingOrders;

    // Use this for initialization
    void Start () {
        pendingOrders = new Queue<TableScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestAdd(TableScript incTable)
    {
        pendingOrders.Enqueue(incTable);
        Debug.Log("added request #"  + pendingOrders.Count);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Enemy")
        {
            Enemy toAssign = collision.GetComponentInParent<Enemy>();
            TableScript dataSource = pendingOrders.Dequeue();
            toAssign.flags = dataSource.myFlags;
            toAssign.myState = Enemy.EnemyState.Seeking;
        }
    }
}
