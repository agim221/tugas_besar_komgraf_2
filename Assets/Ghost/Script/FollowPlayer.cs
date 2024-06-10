using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target_obj;
    public float speed = 2.0f;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target_obj.position, step);

        Vector3 direction = target_obj.position - transform.position;
        Quaternion to_rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, to_rotation, speed * Time.deltaTime);
    }
}