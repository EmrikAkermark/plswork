using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 10, ModSpeed = 5;
    public EnemyPath Modifier;
    private Vector3[] waypoints = new Vector3[0];
    public Transform Model;

    private int currentWaypoint, modCurrentWaypoint;
    private float remainingTravel, modRemainingTravel;
    private Vector3 newPosition, modNewPosition;
    private bool isModifierPresent = false;

    public void SetupPath(Vector3[] Path)
    {
        waypoints = Path;
        transform.position = waypoints[0];
        if (Modifier != null)
            isModifierPresent = true;
    }

    private void FixedUpdate()
    {
        Move();
        if (isModifierPresent)
            ModifierMove();


    }

    private void Move()
    {
        //remaningTravel = Speed * Time.fixedDeltaTime;
        newPosition = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint], Speed * Time.fixedDeltaTime);

        if (newPosition == waypoints[currentWaypoint])
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Length)
                Done();
            else
            {
                remainingTravel = Vector3.Distance(newPosition, transform.position);
                newPosition = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint], remainingTravel * Time.fixedDeltaTime);
                transform.position = newPosition;
            }
        }
        else
            transform.position = newPosition;
    }
    private void ModifierMove()
    {
        modNewPosition = Vector2.MoveTowards(Model.localPosition, Modifier.Waypoints[modCurrentWaypoint], ModSpeed * Time.fixedDeltaTime);
        if (modNewPosition == Modifier.Waypoints[modCurrentWaypoint])
        {
            modCurrentWaypoint++;
            if (modCurrentWaypoint == Modifier.Waypoints.Count)
                modCurrentWaypoint = 0;
            else
            {
                modRemainingTravel = Vector3.Distance(modNewPosition, Model.localPosition);
                modNewPosition = Vector2.MoveTowards(Model.localPosition, Modifier.Waypoints[modCurrentWaypoint], modRemainingTravel * Time.fixedDeltaTime);
                Model.localPosition = modNewPosition;
            }
        }
        else
            Model.localPosition = modNewPosition;
    }

    private void Done()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
