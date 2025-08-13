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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Player)) 
        {
        }
    }
}
