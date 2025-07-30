using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool selected = false;
    int numClicks = 0;
    private MainTycoon mainInstance;

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
            mainInstance.playerOne.selectedCards.Add(this.gameObject);
            mainInstance.displayPossiblePlays();
            numClicks +=1;

            if (numClicks == 2)
            {
                foreach (var sCard in mainInstance.playerOne.selectedCards)
                {
                    sendToPlayedPile(sCard);
                    mainInstance.playerOne.playerHand.Remove(sCard);
                    this.enabled = false;
                }
                numClicks = 0;
                mainInstance.playerOne.selectedCards.Clear();
                mainInstance.playerHandPositioning(mainInstance.playerOne,mainInstance.p1HandSpace);
                mainInstance.displayPossiblePlays();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            mainInstance = MainTycoon.instance;
            selected = false;
            mainInstance.playerOne.selectedCards.Remove(this.gameObject);
            mainInstance.displayPossiblePlays();
            numClicks = 0;
        }
    }

    void sendToPlayedPile(GameObject cardToSend)
    {
        GameObject playedCards = GameObject.Find("Played Cards");
        cardToSend.transform.parent = playedCards.transform;
        cardToSend.transform.position = playedCards.transform.localPosition;
        mainInstance.playedPile.Add(cardToSend);
    }

    public void changeCardColor(Color32 newColor)
    {
        this.GetComponent<SpriteRenderer>().color = newColor;
    }
}
