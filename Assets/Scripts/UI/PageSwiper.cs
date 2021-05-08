using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector3 panelLocation;
    public int currentPage = 1;
    
    public int gapPage = 1; // amount of page was moved by  pool

    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;

    private Vector3 initialPosition;

   [HideInInspector] 
   public bool horizontal = true;

    public delegate void OnEndSwipe();
	public event OnEndSwipe OnSwiped;
   
    private void Start()
    {
        initialPosition = transform.position;
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 difference = data.pressPosition - data.position;
        transform.position = horizontal ? panelLocation - new Vector3(difference.x, 0, 0) : panelLocation - new Vector3(0, difference.y, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = horizontal ? (data.pressPosition.x - data.position.x) / Screen.width : (-data.pressPosition.y + data.position.y) / Screen.height ;

        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if(percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                if(OnSwiped != null) OnSwiped.Invoke();

                if(horizontal)
                    newLocation += new Vector3(-Screen.width, 0, 0);
                else
                    newLocation += new Vector3(0, Screen.height, 0);
            }
            else if(percentage < 0 && currentPage > 1)
            {
                currentPage--;
                if(OnSwiped != null) OnSwiped.Invoke();

                if(horizontal)
                    newLocation += new Vector3(Screen.width, 0, 0);
                else
                    newLocation += new Vector3(0, -Screen.height, 0);
            }
            
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
    }

    private IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }


    public void ShowLatestLevel(int time)
    {
        for (int i = 0; i < time; i++)
        {
            Vector3 newLocation = panelLocation;
            currentPage++;

            if(horizontal)
                    newLocation += new Vector3(-Screen.width, 0, 0);
                else
                    newLocation += new Vector3(0, Screen.height, 0);

            StartCoroutine(SmoothMove(transform.position, newLocation, 0));
            panelLocation = newLocation;
        }

        if(OnSwiped != null) OnSwiped.Invoke();
    }

    private void OnDisable()
    {
        currentPage = 1;
        transform.position = panelLocation = initialPosition;
    }
}
