using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    PhysicsObject po;
    FollowAI follow;
    RoamAI roam;
    public LayerMask playerMask;
    public float followRadius = 10f;

    // Use this for initialization
    void Start () {
        po = GetComponent<PhysicsObject>();
        follow = GetComponent<FollowAI>();
        roam = GetComponent<RoamAI>();
	}
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }

    void Update()
    {
        RaycastHit2D hit = (Physics2D.CircleCast(transform.position, followRadius, Vector2.up, 0.1f, playerMask));
        if (hit)
        {
            roam.enabled = false;
            follow.enabled = true;
        }
        else
        {
            //po.velocity = Vector2.Lerp(po.velocity, Vector2.zero, Time.deltaTime * 10f);
            roam.enabled = true;
            follow.enabled = false;
        }
    }
}
