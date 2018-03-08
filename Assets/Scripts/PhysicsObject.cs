using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsObject : MonoBehaviour
{
    /*movement stuff*/
    public float gravityModifier = 1f;
    public Vector2 velocity = Vector2.zero;

    /*box collider*/
    [HideInInspector]
    public BoxCollider2D coll;

    /*ray spacing + collision stuff*/
    public LayerMask collisionMask;
    public float skinDist = 0.05f;
    public int horizontalRayCount = 4, verticalRayCount = 4;
    float horizontalRaySpacing, verticalRaySpacing;
    RayCastOrigins raycastOrigins;
    public CollisionInfo collisions;
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public bool CheckHorizontal(LayerMask layer)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = skinDist;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, layer);
            if (hit) return true;
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
        }
        return false;
    }

    public RaycastHit2D CheckHorizontalHit(LayerMask layer)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = skinDist;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, layer);
            if (hit) return hit;
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
        }
        return new RaycastHit2D();
    }


    private void FixedUpdate()
    {
        Move(velocity * Time.deltaTime);
    }

    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();
        body.MovePosition(new Vector2((body.position.x + velocity.x * Time.deltaTime), body.position.y + velocity.y * Time.deltaTime));
    }
    /*
     * Finds new bottomleft, bottomright,
     * topleft, and topright points of collider.
     */
    void UpdateRaycastOrigins()
    {
        Bounds bounds = coll.bounds;
        //Gives collision a little bit of space
        bounds.Expand(skinDist * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = coll.bounds;
        bounds.Expand(skinDist * -2f);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RayCastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}
