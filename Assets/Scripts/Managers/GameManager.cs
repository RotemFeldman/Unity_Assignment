using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EventObject eventObject;


    public void CharacterCollided(PlayerCollidedEvent args)
    {
        Debug.Log(args.player.name + " Collided With " + args.collidedWith.name);
    }

    private void OnEnable()
    {
        eventObject.OnCollideWithPlayer.AddListener(CharacterCollided);
    }

    private void OnDisable()
    {
        eventObject.OnCollideWithPlayer.RemoveListener(CharacterCollided);
    }
}
