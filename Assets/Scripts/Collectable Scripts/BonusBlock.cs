using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBlock : MonoBehaviour
{

    public Transform bottom_Collision;

    private Animator anim;

    public LayerMask playerLayer;

    private Vector3 moveDirection = Vector3.up;
    private Vector3 originPosition;
    private Vector3 animPosition;
    public bool startAnim;
    public bool canAnimate = true;
    private Text coinText;
    private AudioSource audioManager;


    void Awake()
    {
        anim = GetComponent<Animator>();
        audioManager = GetComponent<AudioSource>();
    }

    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
        coinText = GameObject.Find("Coin Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
        
    }

    public void CheckForCollision()
    {
        if (canAnimate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottom_Collision.position, Vector2.down, 0.1f, playerLayer);

            if (hit)
            {
                if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {
                    hit.collider.gameObject.GetComponent<ScoreSciprt>().ScoreCount++;
                    coinText.text = "x " + hit.collider.gameObject.GetComponent<ScoreSciprt>().ScoreCount;
                    audioManager.Play();
                    anim.Play("Idle");
                    startAnim = true;
                    canAnimate = false;
                }
            }
        }
    }

    void AnimateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if (transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;

            }
            else if (transform.position.y <= originPosition.y)
            {
                startAnim = false;
            }
        }
    }

} // class





































