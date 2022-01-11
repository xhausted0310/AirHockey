using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool canMove;
    bool wasJustClicked = true;

    Rigidbody2D rb;
    Vector2 startPosition;

    public Transform BoundaryHolder;
    Boundary playerBoundary;

    Collider2D playerCollider;

    

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mosePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (wasJustClicked)
            {
                wasJustClicked = false;
                if(playerCollider.OverlapPoint(mosePos))
                {
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
            }
            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mosePos.x, playerBoundary.left,playerBoundary.right),
                                                      Mathf.Clamp(mosePos.y, playerBoundary.down, playerBoundary.up));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    }
    public void ResetPosition()
    {
        rb.position = startPosition;
    }
}
