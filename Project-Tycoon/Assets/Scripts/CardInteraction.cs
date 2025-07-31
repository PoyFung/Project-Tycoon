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

    //ON MOUSE ENTER HIT BOX
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selected == false)
        {
            transform.localPosition =
                new Vector2(transform.localPosition.x, transform.localPosition.y + 0.25f);
        }
    }

    //ON MOUSE EXIT HIT BOX
    public void OnPointerExit(PointerEventData eventData)
    {
        if (selected == false)
        {
            transform.localPosition =
                new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f);
        }
    }

    //ON MOUSE CLICK
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) //Left Mouse Button
        {
            mainInstance = MainTycoon.instance;
            selected = true;
            mainInstance.playerOne.selectedCards.Add(this.gameObject);
            mainInstance.displayPossiblePlays();
            numClicks +=1;

            if (numClicks == 2)
            {
                mainInstance = MainTycoon.instance;
                for(int i=0;i<mainInstance.playerOne.selectedCards.Count-1;i++)
                {
                    sendToPlayedPile(mainInstance.playerOne.selectedCards[i]);
                    mainInstance.playerOne.playerHand.Remove(mainInstance.playerOne.selectedCards[i]);
                    this.enabled = false;
                }
                numClicks = 0;

                mainInstance = MainTycoon.instance;
                mainInstance.playerOne.selectedCards.Clear();
                mainInstance.playerHandPositioning(mainInstance.playerOne,mainInstance.p1HandSpace);
                mainInstance.displayPossiblePlays();

                mainInstance.determineSetType();
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right) //Right Mouse button
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
