using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoom : MonoBehaviour {


    public Queue <TableScript> pendingOrders;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RequestAdd(TableScript incTable)
    {
        pendingOrders.Enqueue(incTable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
