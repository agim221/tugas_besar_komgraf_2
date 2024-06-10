using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent ghost;

    void update()
    {
        ghost.SetDestination(target.position);
    }


}
