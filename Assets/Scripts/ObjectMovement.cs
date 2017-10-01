using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : GameManager
{

    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected Rigidbody2D rb2d;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Vector2 targetVelocity;

    protected bool isGrounded;
    protected Vector2 groundNormal;

    protected const float minMoveDistance = 0.001f; // minimum distance before checking for a collision
    protected const float shellRadius = 0.01f; // prevents colliders from passing inside each other

    public float gravityModifier = 1f;

    public float minGroundNormalY = 0.65f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        computeVelocity();
    }

    protected virtual void computeVelocity()
    {

    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        isGrounded = false;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 deltaPosition = velocity * Time.deltaTime;

        // Runs x movement
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);


        // Runs y movement
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        int count = 0;
        Vector2 currentNormal = Vector2.zero;
        float distance = move.magnitude;

        // if distance is greater than minimum move distance
        // modify distance only if distance will result in a value lower than shell size
        if (distance > minMoveDistance)
        {
            count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
                hitBufferList.Add(hitBuffer[i]);

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    isGrounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                if (Vector2.Dot(velocity, currentNormal) < 0)
                {
                    velocity = velocity - Vector2.Dot(velocity, currentNormal) * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = ( modifiedDistance < distance ) ? modifiedDistance : distance;
            }

        }

        rb2d.position += move.normalized * distance;
    }
}
