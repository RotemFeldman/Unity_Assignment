using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventObject : MonoBehaviour
{
   public UnityEvent<PlayerCollidedEvent> OnCollideWithPlayer;

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         OnCollideWithPlayer?.Invoke(new PlayerCollidedEvent(other.gameObject, gameObject));
      }
   }
}


