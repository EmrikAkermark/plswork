using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{


    public float Speed = 10;
    public List<Vector3> Waypoints = new List<Vector3>();

    private int currentWaypoint;
    private float remaningTravel;
    private Vector3 newPosition;
    private Rigidbody rb;
    private bool active = true;


    private void FixedUpdate()
    {
        if(active)
            Move();
    }

    private void Move()
    {
        //remaningTravel = Speed * Time.fixedDeltaTime;
        newPosition = Vector2.MoveTowards(transform.position, Waypoints[currentWaypoint], Speed * Time.fixedDeltaTime);

        if (newPosition == Waypoints[currentWaypoint])
        {
            currentWaypoint++;
            if (currentWaypoint == Waypoints.Count)
                Done();
            else
            {
                remaningTravel = Vector3.Distance(newPosition, transform.position);
                newPosition = Vector2.MoveTowards(transform.position, Waypoints[currentWaypoint], Speed * Time.fixedDeltaTime);
                transform.position = newPosition;
            }
        }
        else
            transform.position = newPosition;
    }

    private void Done()
    {
        active = false;
    }
}
