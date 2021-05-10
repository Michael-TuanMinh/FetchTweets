using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum ParameterType 
{
    NONE, Float, Int, Bool, String, GameObject, Vector2, Vector3, Vector2Int, Vector3Int
};

public class EventListener : MonoBehaviour
{
    public GameEvent[] Events;
    [SerializeField] ParameterType ParameterType;
    
    public UnityEvent Response;
    
    public UnityEventGameObject ResponseGameObject;
    
    public UnityEventInt ResponseInt;

    public UnityEventFloat ResponseFloat;
    
    public UnityEventString ResponseString;
    
    public UnityEventBool ResponseBool;
    
    public UnityEventVector2 ResponseVector2;
    
    public UnityEventVector3 ResponseVector3;
    
    public UnityEventVector2 ResponseVector2Int;
    
    public UnityEventVector3 ResponseVector3Int;


    void OnEnable()
    {
        foreach (GameEvent ev in Events) { ev.Register(this); }
    }
    void OnDisable()
    {
        foreach (GameEvent ev in Events) { ev.DeRegister(this); }
    }

    public void OnEventRaised() 
    {
        Response.Invoke(); 
    }

    public void OnEventRaised(GameObject parameter) 
    {
        ResponseGameObject.Invoke(parameter); 
    }

    public void OnEventRaised(int parameter) 
    {
        ResponseInt.Invoke(parameter); 
    }

    public void OnEventRaised(float parameter) 
    {
        ResponseFloat.Invoke(parameter); 
    }

    public void OnEventRaised(string parameter) 
    {
        ResponseString.Invoke(parameter); 
    }

    public void OnEventRaised(bool parameter) 
    {
        ResponseBool.Invoke(parameter); 
    }

    public void OnEventRaised(Vector2 parameter) 
    {
        ResponseVector2.Invoke(parameter); 
    }

    public void OnEventRaised(Vector3 parameter) 
    {
        ResponseVector3.Invoke(parameter); 
    }
    
    public void OnEventRaised(Vector2Int parameter) 
    {
        ResponseVector2Int.Invoke(parameter); 
    }

    public void OnEventRaised(Vector3Int parameter) 
    {
        ResponseVector3Int.Invoke(parameter); 
    }

}
