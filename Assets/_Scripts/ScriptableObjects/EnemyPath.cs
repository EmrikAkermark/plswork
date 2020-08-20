using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPath", menuName = "Enemies/New Enemy Path")]

public class EnemyPath : ScriptableObject
{
    public List<Vector3> Waypoints = new List<Vector3>();
}
