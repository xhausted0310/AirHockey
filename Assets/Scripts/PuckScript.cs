using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    public AudioManager audioManager; 
    public static bool WasGoal { get; private set; }
    public float MaxSpeed;

    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     if(!WasGoal)
        {
            if(other.tag == "AiGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            }
            else if(other.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision(); 
    }
    private IEnumerator ResetPuck(bool didAiScore)
    {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        

        if(didAiScore)
        {
            rb.velocity = rb.position = new Vector2(0, -1.6f);
        }
        else
        {
            rb.velocity = rb.position = new Vector2(0, 1.6f);
        }
    }

    public void CenterPuck()
    {
        rb.position = new Vector2(0, 0);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
