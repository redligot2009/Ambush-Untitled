using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
    
    public Transform target;
    public float deadzone = 2f;
    Vector3 temp;
    public float speed = 10f;

	void Start ()
    {
        temp = transform.position;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawCube(transform.position, Vector3.one * deadzone);
    }

    void LateUpdate ()
    {
        float xdiff = (target.position.x - transform.position.x);
        float ydiff = (target.position.y - transform.position.y);
        if (xdiff >= deadzone / 2f && xdiff >= 0)
        {
            temp.x = target.transform.position.x - deadzone / 2f;
        }
        else if(xdiff <= -deadzone / 2f && xdiff <= 0)
        {
            temp.x = target.transform.position.x + deadzone / 2f;
        }
        else
        {
            temp.x = transform.position.x;
        }
        if (ydiff >= deadzone / 2f && ydiff >= 0)
        {
            temp.y = target.transform.position.y - deadzone / 2f;
        }
        else if (ydiff <= -deadzone / 2f && ydiff <= 0)
        {
            temp.y = target.transform.position.y + deadzone / 2f;
        }
        else
        {
            temp.y = transform.position.y;
        }
        transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
    }
}
