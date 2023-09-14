using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour
{
    private Rigidbody2D myBody;

    private Text lifeText;
    private int lifeScoreCount;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        lifeText = GameObject.Find("Life Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.PLAYER_TAG)
        {
            lifeScoreCount = 0;
            lifeText.text = "x " + lifeScoreCount;
            StartCoroutine(RestartGame());
        }
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Mario");
    }
}
