using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollidedEvent : UnityEvent
{
    public GameObject player;
    public GameObject collidedWith;

    public PlayerCollidedEvent(GameObject Player, GameObject Other)
    {
        player = Player;
        collidedWith = Other;
    }
}
