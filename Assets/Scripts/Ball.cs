using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask bounceLayers;
    [SerializeField] int scoreWorth = 10;
    float bounceCheckRadius = 0.2f;
    Rigidbody2D rigBody2D;
    public Bounds ballBounds;


    public delegate void GetHitAction(int scoreWorth);
    public static event GetHitAction OnHit;
    public delegate void LastBallDeath();
    public static event LastBallDeath onLastBallDeath;

    private void Awake()
    {        
        rigBody2D = GetComponent<Rigidbody2D>();
        rigBody2D.velocity = new Vector2(moveSpeed, 0);
        transform.localScale = Vector3.one * size;
        ballBounds = GetComponentInChildren<MeshRenderer>().bounds;
    }

    void OnEnable()
    {
        PlayerController.OnDeath += ToggleMovement;
        GameManager.onPause += ToggleMovement;
    }
    void OnDisable()
    {
        PlayerController.OnDeath -= ToggleMovement;
        GameManager.onPause -= ToggleMovement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Rope>() != null)
        {
            SplitBall();
            return;
        }
        if (collision.collider.GetComponentInParent<PlayerController>() != null)
        {
            collision.collider.GetComponentInParent<PlayerController>().Die();            
        }
        if (rigBody2D.velocity.x * moveSpeed <0) // if listed movespeed is positive but actual horizontal velocity is negative or vice verca
        {
            moveSpeed *= -1;
        }
        Vector2 bottomPoint = (Vector2)transform.position + ballBounds.extents.x * Vector2.down;
        if (Physics2D.OverlapCircle(bottomPoint, bounceCheckRadius, bounceLayers))
        {
            BounceFromGround();
        }
        
    }

    void ToggleMovement()
    {
        rigBody2D.simulated = !rigBody2D.simulated;
    }

    void BounceFromGround()
    {
        rigBody2D.velocity = new Vector2(moveSpeed, Mathf.Sqrt(-2f * Physics2D.gravity.y * size));
    }

    void SplitBall()
    {
        size--;
        OnHit(scoreWorth);
        if (size == 0)
        {
            if (transform.parent.childCount==1)
            {
                onLastBallDeath();
            }
            Destroy(gameObject);
            return;
        }
        transform.localScale = Vector3.one * size;
        BounceFromGround();
        GameObject newBall = Instantiate(gameObject, transform.parent);
        Ball newBallComponent = newBall.GetComponent<Ball>();
        newBallComponent.moveSpeed *= -1;
        newBallComponent.BounceFromGround();

    }
}
