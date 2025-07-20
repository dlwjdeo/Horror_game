using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private RoomController currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchRoom(RoomController newRoom)
    {
        if (currentRoom != null)
            currentRoom.SetOverlayActive(true);

        currentRoom = newRoom;
        currentRoom.SetOverlayActive(false);
    }
}
