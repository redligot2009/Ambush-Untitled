using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamAI : MonoBehaviour {

    public float moveSpeed = 10f;
    public float roamRadius = 5f;
    public float maxDistance = 0.5f;
    public float roamDelay = 2f;
    float roamTimer = 2f;
    PhysicsObject po;

    Vector2 targetVelocity, targetPos;

    void Start ()
    {
        po = GetComponent<PhysicsObject>();
        roamTimer = roamDelay;
        targetVelocity = Vector2.zero;
        targetPos = transform.position;
    }

    Vector2 diff;

    void Update () {
		if(roamTimer <= 0)
        {
            float randX = Random.Range(-1f, 1f), randY = Random.Range(-1, 1f);
            Vector2 dir = new Vector2(randX, randY);
            dir = dir.normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, roamRadius, po.collisionMask);
            if(hit)
            {
                //Debug.Log("YO");
                targetPos = hit.point - (dir.normalized*1f);
            }
            else
            {
                targetPos = (Vector2)transform.position + (dir * roamRadius);
            }
            roamTimer = roamDelay;
        }
        Vector2 diff = targetPos - (Vector2)transform.position;
        float dist = (diff).magnitude;
        if(dist > maxDistance)
        {
            targetVelocity = diff.normalized * moveSpeed;
        }
        else
        {
            targetVelocity = Vector2.zero;
        }
        Debug.DrawLine(transform.position, targetPos);
        po.velocity = Vector2.Lerp(po.velocity, targetVelocity, Time.deltaTime * 10f);
        if (roamTimer > 0) roamTimer -= Time.deltaTime;
	}
}
