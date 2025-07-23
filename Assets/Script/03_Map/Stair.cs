using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public Collider2D Collider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Player))
        {
            if(GameManager.Instance.player.isOnStair == true)
                Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), Collider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Player)) 
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), Collider, false);
        }
    }
}
