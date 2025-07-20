using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject DarkOverlay;

    private bool hasEntered = false;

    private BoxCollider2D roomTrigger;

    private void Awake()
    {
        roomTrigger = GetComponentInChildren<BoxCollider2D>();
    }

    private void Start()
    {
        DarkOverlay.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!hasEntered && collision.CompareTag(TagName.Player))
        {
            hasEntered = true;
            RoomManager.Instance.SwitchRoom(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.Instance.SwitchRoom(this);
        }
    }

    public void SetOverlayActive(bool active)
    {
        if (DarkOverlay != null)
            DarkOverlay.SetActive(active);
    }
}
