using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool selected = false;
    int numClicks = 0;
    public string cardKey;
    public int cardRank;
    private MainTycoon mainInstance;

    public void setCardKey(string key)
    {
        cardKey = key;
    }

    public void setCardRank(int rank)
    {
        cardRank = rank;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected == false)
        {
            transform.localPosition =
                new Vector2(transform.localPosition.x, transform.localPosition.y + 0.25f);
        }
        //Debug.Log("MOUSE ON");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected == false)
        {
            transform.localPosition =
                new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);
        }
        //Debug.Log("MOUSE OFF");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            mainInstance = MainTycoon.instance;
            selected = true;
            mainInstance.displayPossiblePlays(cardRank);
            numClicks +=1;

            if (numClicks == 2)
            {
                foreach (var pCard in mainInstance.playerOne.playerHand)
                {
                    mainInstance = MainTycoon.instance;
                    var currentCardObject = pCard.cardObject.GetComponent<CardInteraction>().selected;
                    if (currentCardObject==true)
                    {
                        this.enabled = false;
                        sendToPlayedPile(pCard);
                    }
                }
                numClicks = 0;
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            numClicks = 0;
            selected = false;
        }
    }

    void sendToPlayedPile(card cardToSend)
    {
        GameObject playedCards = GameObject.Find("Played Cards");
        transform.localPosition = playedCards.transform.localPosition;
        int index = mainInstance.playerOne.playerHand
            .FindIndex(pCard=>pCard.cardObject.gameObject==this.gameObject);

        mainInstance.playedPile.Add(mainInstance.playerOne.playerHand[index]);
        mainInstance.playerOne.playerHand.RemoveAt(index);
    }

    public void changeCardColor()
    {
        this.GetComponent<SpriteRenderer>().color = new Color32(116, 45, 45, 255);
    }
}
