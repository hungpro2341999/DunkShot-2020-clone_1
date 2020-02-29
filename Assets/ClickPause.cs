using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ClickPause : MonoBehaviour, IPointerDownHandler
{
    public static bool Click = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        ClickPause.Click = true;
    }

   
}
