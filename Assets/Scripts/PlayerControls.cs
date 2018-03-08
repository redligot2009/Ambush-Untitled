using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    PhysicsObject po;
    public float moveSpeed;

	void Start () {
        po = GetComponent<PhysicsObject>();
	}
	
	void Update () {
        float xmove = Input.GetAxisRaw("Horizontal");
        float ymove = Input.GetAxisRaw("Vertical");
		if(xmove < 0 && !po.collisions.left)
        {
            po.velocity.x = Mathf.Lerp(po.velocity.x, -moveSpeed, Time.deltaTime * 10f);
        }
        if(xmove > 0 && !po.collisions.right)
        {
            po.velocity.x = Mathf.Lerp(po.velocity.x, moveSpeed, Time.deltaTime * 10f);
        }
        if(Mathf.Abs(xmove) <= 0.1f)
        {
            po.velocity.x = Mathf.Lerp(po.velocity.x, 0f, Time.deltaTime * 10f);
        }

        if (ymove > 0 && !po.collisions.above)
        {
            po.velocity.y = Mathf.Lerp(po.velocity.y, moveSpeed, Time.deltaTime * 10f);
        }
        if (ymove < 0 && !po.collisions.below)
        {
            po.velocity.y = Mathf.Lerp(po.velocity.y, -moveSpeed, Time.deltaTime * 10f);
        }
        if (Mathf.Abs(ymove) <= 0.1f)
        {
            po.velocity.y = Mathf.Lerp(po.velocity.y, 0f, Time.deltaTime * 10f);
        }
        po.velocity = Vector3.ClampMagnitude(po.velocity, moveSpeed);
        RaycastHit2D enemyHit = po.CheckHorizontalHit(LayerMask.GetMask("enemy"));
        if (enemyHit)
        {

        }
    }
}
