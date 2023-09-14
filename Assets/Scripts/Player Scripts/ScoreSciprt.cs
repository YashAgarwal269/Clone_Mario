using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSciprt : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;
    public int ScoreCount = 0;

    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
        
    }
    void Start()
    {
        coinTextScore = GameObject.Find("Coin Text").GetComponent<Text>();

    }
   

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.COIN_TAG)
        {
            target.gameObject.SetActive(false);
            ScoreCount++;
            
            coinTextScore.text = "x " + ScoreCount;
            audioManager.Play();
        }
        
    }

}