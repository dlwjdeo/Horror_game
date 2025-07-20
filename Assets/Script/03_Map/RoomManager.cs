using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    private RoomLightController currentRoom;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchRoom(RoomLightController newRoom)
    {
        if (currentRoom != null)
            currentRoom.SetLightActive(false);

        currentRoom = newRoom;
        currentRoom.SetLightActive(true);
    }
}
