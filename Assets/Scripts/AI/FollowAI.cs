using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour {

    PhysicsObject po;
    public float maxDistance = 0.5f;
    public float moveSpeed = 10f;
    public float bounceX = 10f;
    public Transform target;

    void Start ()
    {
        target = GameObject.FindWithTag("Player").transform;
        po = GetComponent<PhysicsObject>();
	}

    void Update ()
    {
        po.velocity = Vector2.ClampMagnitude(po.velocity, moveSpeed);
        RaycastHit2D closeEnough = (Physics2D.CircleCast(transform.position, maxDistance, Vector2.up, 0.1f, target.gameObject.layer));
        if (!closeEnough)
        {
            GameObject playerObject = target.gameObject;
            Vector2 diff = (playerObject.transform.position - transform.position);
            po.velocity = Vector2.Lerp(po.velocity,moveSpeed * diff.normalized,Time.deltaTime * 2f);
        }
        else
        {
            po.velocity = Vector2.Lerp(po.velocity, Vector2.zero, 2f * Time.deltaTime);
        }
        RaycastHit2D enemyHit = po.CheckHorizontalHit(LayerMask.GetMask("enemy"));
        if(enemyHit && enemyHit.collider.gameObject != transform.gameObject)
        {

        }
	}
}
