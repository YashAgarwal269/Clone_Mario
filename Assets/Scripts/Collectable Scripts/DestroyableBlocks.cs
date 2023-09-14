using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBlocks : MonoBehaviour
{
    public Transform bottom_collision;
    public LayerMask playerLayer;

    void Update()
    {
        checkForCollision();
    }
    public void checkForCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(bottom_collision.position, Vector2.down, 0.1f, playerLayer);
        if (hit)
        {
            if (hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
