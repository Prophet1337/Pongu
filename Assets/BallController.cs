using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 10f;
    [SerializeField] private float SpeedIncrease = 0.5f;
    [SerializeField] private Text Player1Score;
    [SerializeField] private Text Player2Score;

    private int HitCounter;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 1.5f);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, InitialSpeed + (SpeedIncrease * HitCounter));
    }
    private void StartBall()
    {
        if(Random.Range(1,10) > 5)
        {
            rb.velocity = new Vector2(-1, Random.Range(-1f, 1f)) * (InitialSpeed + SpeedIncrease * HitCounter);
        }
        else
        {
            rb.velocity = new Vector2(1, Random.Range(-1f, 1f)) * (InitialSpeed + SpeedIncrease * HitCounter);
        }
    }
    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        HitCounter = 0;
        Invoke("StartBall", 2f);
    }
    private void PlayerBounce(Transform myObject)
    {
        HitCounter += 1;
        Vector2 BallPos = transform.position;
        Vector2 PlayerPos = myObject.position;

        float DirectionX, DirectionY;
        if(transform.position.x > 0)
        {
            DirectionX = -1;
        }
        else
        {
            DirectionX = 1;
        }
        DirectionY = (BallPos.y - PlayerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if(DirectionY == 0)
        {
            DirectionY = 0.25f;
        }
        rb.velocity = new Vector2(DirectionX, DirectionY) * (InitialSpeed + (HitCounter * SpeedIncrease));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            PlayerBounce(collision.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            Player1Score.text = (int.Parse(Player1Score.text) + 1).ToString();
        }
        else
        {
            Player2Score.text = (int.Parse(Player2Score.text) + 1).ToString();
        }
        ResetBall();
    }
}