using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private bool selected = false;
    int numClicks = 0;
    public string cardKey;
    private MainTycoon mainInstance;

    public void setCardKey(string key)
    {
        cardKey = key;
    }
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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            selected = true;
            numClicks+=1;

            if (numClicks == 2)
            {
                mainInstance = MainTycoon.instance;
                sendToPlayedPile();
                numClicks = 0;
                this.enabled = false;
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            numClicks = 0;
            selected = false;
        }
    }

    void sendToPlayedPile()
    {
        GameObject playedCards = GameObject.Find("Played Cards");
        transform.localPosition = playedCards.transform.localPosition;
        int index = mainInstance.playerOne.playerHand
            .FindIndex(pCard=>pCard.cardObject.gameObject==this.gameObject);
        mainInstance.playedPile.Add(mainInstance.playerOne.playerHand[index]);
        mainInstance.playerOne.playerHand.RemoveAt(index);
    }
}
