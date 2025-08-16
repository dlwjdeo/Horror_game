using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private PlayerMover playerMover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Player))
        {
            playerMover = collision.GetComponent<PlayerMover>();
            playerMover.SetStair(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Player)) 
        {
            playerMover.SetStair(false);
            //playerMover.ExitStair();
        }
    }
}
