using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<EventListener> eventListeners = new List<EventListener>();

    public bool showLog = true;

    public void Raise()
    {
        if(showLog) Debug.Log(this.name + " Raised");
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised();
        }
    }
    public void Raise(GameObject mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With GameObject " + mParameter.name);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Raise(int mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Int " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Raise(float mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Float " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Raise(bool mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Bool " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Raise(Vector2 mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Vector2 " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }
    
    public void Raise(string mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With string " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Raise(Vector3 mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Vector3 " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }
    
    public void Raise(Vector2Int mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Vector2Int " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Raise(Vector3Int mParameter)
    {
        if(showLog) Debug.Log(this.name + " Raised With Vector3Int " + mParameter);
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised(mParameter);
        }
    }

    public void Register(EventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void DeRegister(EventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
