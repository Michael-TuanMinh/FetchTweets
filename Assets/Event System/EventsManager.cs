using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
  public static EventsManager instance { get; private set; }

  public GameEvent PlaceObjectEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
