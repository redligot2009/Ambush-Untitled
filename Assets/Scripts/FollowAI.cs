using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour {

    PhysicsObject po;
    public float followRadius = 5f;
    public float maxDistance = 0.5f;
    public float moveSpeed = 10f;
    public float bounceX = 10f;
    public LayerMask playerMask;
    Vector2 oldPos, newPos;

	void Start ()
    {
        po = GetComponent<PhysicsObject>();
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }

    void Update ()
    {
        RaycastHit2D hit = (Physics2D.CircleCast(transform.position, followRadius, Vector2.up, 0.1f, playerMask));

        po.velocity = Vector2.ClampMagnitude(po.velocity, moveSpeed);
        if (hit)
        {
            RaycastHit2D closeEnough = (Physics2D.CircleCast(transform.position, maxDistance, Vector2.up, 0.1f, playerMask));
            if (!closeEnough)
            {
                GameObject playerObject = hit.collider.gameObject;
                po.velocity = Vector2.Lerp(po.velocity,moveSpeed * (playerObject.transform.position - transform.position).normalized,Time.deltaTime * 2f);
            }
            else
            {
                po.velocity = Vector2.Lerp(po.velocity, Vector2.zero, 2f * Time.deltaTime);
            }
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
