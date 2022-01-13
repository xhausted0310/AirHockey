using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public List<PlayerMovement> Players = new List<PlayerMovement>();
           
    void Update()
    {
        for(int i = 0; i<Input.touchCount; i++)
        {
            Vector2 touchWorldPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            foreach(var player in Players)
            {
                if(player.LockedfingerID == null)
                {
                    if(Input.GetTouch(i).phase == TouchPhase.Began &&
                        player.PlayerCollider.OverlapPoint(touchWorldPos))
                    {
                        player.LockedfingerID = Input.GetTouch(i).fingerId;
                    }
                }
                else if(player.LockedfingerID == Input.GetTouch(i).fingerId)
                {
                    player.MoveToPosition(touchWorldPos);
                    if(Input.GetTouch(i).phase == TouchPhase.Ended ||
                        Input.GetTouch(i).phase == TouchPhase.Canceled)
                    {
                        player.LockedfingerID = null;
                    }
                }
            }
        }
    }
}
