using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    private Vector3 initialPosition;
    private RectTransform rectTransform;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.localPosition;
    }

    public void ResetPosition()
    {
        rectTransform.localPosition = initialPosition;
    }
}
