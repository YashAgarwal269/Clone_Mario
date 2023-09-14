using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;
    public LayerMask playerLayer;

    private bool canMove;
    private bool stunned;
    public Transform down_Collision, left_Collision, right_Collision, top_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;
        void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left_Collision_Pos = left_Collision.position;
        right_Collision_Pos = right_Collision.position;
    }

    void Start()
    {
        moveLeft = true;
        canMove = true;

    }

    
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        CheckCollision();
        
    }
        void CheckCollision()
        {
        RaycastHit2D lefthit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer); 
        RaycastHit2D righthit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D tophit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if(tophit != null)
        {
            if (tophit.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    tophit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(tophit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                    anim.Play("Stunned");
                    stunned = true;
                    //beetle code here
                    if(tag == "Beetle")
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }
        if (lefthit)
        {
            if(lefthit.collider.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    lefthit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
           
        }
        if (righthit)
        {
            if (righthit.collider.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    righthit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != "Beetle")
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        
        }
        //if no ground then
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
            {
            ChangeDirection(); 
        }
        
    }
        
    void ChangeDirection()
        {
        moveLeft = !moveLeft;
        Vector3 tempscale = transform.localScale;
        if (moveLeft)
        {
            tempscale.x = Mathf.Abs(tempscale.x);
            left_Collision.position = left_Collision_Pos;
            right_Collision.position=right_Collision_Pos;
        }
        else
        {
            tempscale.x = -Mathf.Abs(tempscale.x);
            left_Collision.position = right_Collision_Pos;
            right_Collision.position = left_Collision_Pos;
        }
        
        transform.localScale = tempscale;
        }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                canMove = false;
                myBody.velocity = new Vector2(0, 0);

                StartCoroutine(Dead(0.3f));
            }

            if(tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
