using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startPos;

    public Rigidbody2D Puck;

    public Transform playerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;

    private Vector2 targetPos;

    private bool isFirstTimeInOpponentsHalf = true;
    private float offsetXFromTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        //startPos = new Vector2(0,0);

        playerBoundary = new Boundary(playerBoundaryHolder.GetChild(0).position.y,
                                      playerBoundaryHolder.GetChild(1).position.y,
                                      playerBoundaryHolder.GetChild(2).position.x,
                                      playerBoundaryHolder.GetChild(3).position.x);
        puckBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y,
                                    PuckBoundaryHolder.GetChild(1).position.y,
                                    PuckBoundaryHolder.GetChild(2).position.x,
                                    PuckBoundaryHolder.GetChild(3).position.x);
    }
    private void FixedUpdate()
    {
        if (!PuckScript.WasGoal)
        {
            float movementSpeed;

            if (Puck.position.y < puckBoundary.down)
            {
                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetXFromTarget = Random.Range(-1f, 1f);
                }
                movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
                targetPos = new Vector2(Mathf.Clamp(Puck.position.x + offsetXFromTarget, playerBoundary.left, playerBoundary.right),
                                        startPos.y);
            }
            else
            {
                isFirstTimeInOpponentsHalf = true;

                movementSpeed = Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed);
                targetPos = new Vector2(Mathf.Clamp(Puck.position.x, playerBoundary.left, playerBoundary.right),
                                       Mathf.Clamp(Puck.position.y, playerBoundary.down, playerBoundary.up));
            }
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, movementSpeed * Time.fixedDeltaTime));
        }
    }
    public void ResetPosition()
    {
        rb.position = startPos;
    }
}
