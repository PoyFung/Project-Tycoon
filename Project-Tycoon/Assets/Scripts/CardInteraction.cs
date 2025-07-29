using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private bool selected = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected == false)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 0.25f);
        }
        //Debug.Log("MOUSE ON");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected == false)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);
        }
        //Debug.Log("MOUSE OFF");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selected = true;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            selected = false;
        }
        //Debug.Log("CLICK");
    }
}
