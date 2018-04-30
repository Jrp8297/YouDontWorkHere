using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoom : MonoBehaviour {

    public Enemy waiter;
    public Queue <TableScript> pendingOrders;

    // Use this for initialization
    void Start () {
        pendingOrders = new Queue<TableScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(pendingOrders.Count > 0)
        {//if we have an order to give
            if(waiter.myState == Enemy.EnemyState.Idle)
            {
                TableScript dataSource = pendingOrders.Dequeue();
                waiter.flags = dataSource.myFlags;
                waiter.myState = Enemy.EnemyState.Seeking;
            }
        }
	}

    public void RequestAdd(TableScript incTable)
    {
        pendingOrders.Enqueue(incTable);
        Debug.Log("added request #"  + pendingOrders.Count);
    }
  
}
