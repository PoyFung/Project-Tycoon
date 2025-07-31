using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class player
{
    [SerializeField]
    public List<GameObject> playerHand = new List<GameObject>();
    public List<GameObject> selectedCards = new List<GameObject>();
}

public class MainTycoon : MonoBehaviour
{
    public static MainTycoon instance { get; private set; }
    //GET PLAYER OBJECTS-------------------------
    public player playerOne = new player();
    player playerTwo = new player();
    player playerThree = new player();
    player playerFour = new player();

    [SerializeField] private GameObject p1Hand;
    [SerializeField] private GameObject p2Hand;
    [SerializeField] private GameObject p3Hand;
    [SerializeField] private GameObject p4Hand;
    
    public GameObject p1HandSpace; //Player's Hand on Screen
    [SerializeField] private GameObject p2HandSpace; //Player's Hand on Screen
    [SerializeField] private GameObject p3HandSpace; //Player's Hand on Screen
    [SerializeField] private GameObject p4HandSpace; //Player's Hand on Screen
    //-------------------------------------------

    //GAME PROPERTIES----------------------------
    List<int> deckSplit = new List<int>() {13,13,14,14}; //Splitting the deck
    public List<GameObject> playedPile = new List<GameObject>();
    public List<GameObject> showSelected = new List<GameObject>();

    //Type of hands being played
    public int setType = 0;
    public bool isSingle = false;
    public bool isDouble = false;
    public bool isTriple = false;
    public bool isQuadruple = false;
    public bool isRevolution = false;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        showSelected=playerOne.selectedCards;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillPlayerHand(playerOne,p1Hand);
        playerHandPositioning(playerOne, p1HandSpace);

        fillPlayerHand(playerTwo,p2Hand);
        fillPlayerHand(playerThree,p3Hand);
        fillPlayerHand(playerFour,p4Hand);

        opponentHandPositioning(playerTwo,p2HandSpace);
        opponentHandPositioning(playerThree,p3HandSpace);
        opponentHandPositioning(playerFour,p4HandSpace);
    }

    void fillPlayerHand(player inputPlayer, GameObject pHand)
    {
        var mtDeck = CreateDeck.instance.mainDeck;

        int pickRandomSplit = UnityEngine.Random.Range(0, deckSplit.Count);
        int pickedNum = deckSplit[pickRandomSplit];
        deckSplit.RemoveAt(pickRandomSplit);

        for(int p1HandNumber = 0;p1HandNumber<pickedNum;p1HandNumber++)
        {
            var randomCard = mtDeck.ElementAt(UnityEngine.Random.Range(0, mtDeck.Count));
            inputPlayer.playerHand.Add(randomCard.Value);
            mtDeck.Remove(randomCard.Key);
        }
        inputPlayer.playerHand.Sort
            ((cardA, cardB) => cardA.GetComponent<CardProperties>().cardRank.
            CompareTo(cardB.GetComponent<CardProperties>().cardRank));

        //Place cards from list into Player Parent
        foreach (var pCard in inputPlayer.playerHand)
        {
            pCard.transform.parent = pHand.transform;
        }
    }

    public void playerHandPositioning(player inputPlayer,GameObject handSpace)
    {
        int handSize = inputPlayer.playerHand.Count;
        Vector2 spaceCenter = handSpace.transform.position;

        //By dividing by 2 and not 2f, the resulting number becomes an int, making it good for odd numbered hands
        float cardPos = -1f * (handSize / 2);

        if (handSize%2==0)
        {
            cardPos += 0.5f;
        }

        int currentCard = 0;
        for (int i= 0;i<handSize;i++)
        {
            inputPlayer.playerHand[currentCard].transform.position
                = new Vector3(cardPos * 1.25f, handSpace.transform.position.y,cardPos*-1);

            //ACTIVATE CARD INTERACTION
            inputPlayer.playerHand[currentCard].GetComponent<CardInteraction>().enabled=true;
            currentCard++;
            cardPos++;
        }
    }

    void opponentHandPositioning(player inputOpponent,GameObject handSpace)
    {
        int handSize = inputOpponent.playerHand.Count;
        Vector2 spaceCenter = handSpace.transform.position;

        foreach(var opCard in inputOpponent.playerHand)
        {
            opCard.transform.position = handSpace.transform.position;
        }
    }

    public void displayPossiblePlays ()
    {
        foreach (var pCard in playerOne.playerHand)
        {
            var currentCard = pCard.GetComponent<CardInteraction>();
            var currentCardProperties = pCard.GetComponent<CardProperties>();

            if (playedPile.Any()==false)
            {
                if (currentCard.selected == false)
                {
                    currentCard.changeCardColor(new Color32(206, 96, 96, 255));
                    currentCard.GetComponent<CardInteraction>().enabled = true;
                }
            }

            else if (playedPile.Last().GetComponent<CardProperties>().cardRank
                <currentCardProperties.cardRank)
            {
                currentCard.changeCardColor(new Color32(206, 96, 96, 255));
                currentCard.GetComponent<CardInteraction>().enabled = true;
            }
        }

        if (playerOne.selectedCards.Any()) //When the player has selected cards
        {
            int currentRankedPlay = playerOne.selectedCards[0].GetComponent<CardProperties>().cardRank;
            foreach (var pCard in playerOne.playerHand)
            {
                var currentCard = pCard.GetComponent<CardInteraction>();
                var currentCardProperties = pCard.GetComponent<CardProperties>();

                /*When there are no cards layed in the pile and you've selected a card,
                 deactivate any cards that don't match the selected rank*/
                if (currentCard.selected == false
                    && currentCardProperties.cardRank != currentRankedPlay)
                {
                    currentCard.changeCardColor(new Color32(116, 45, 45, 255));
                    currentCard.GetComponent<CardInteraction>().enabled = false;
                }
            }
        }
    }

    public void determineSetType()
    {
        if (isSingle==false && isDouble==false && isTriple==false && isQuadruple==false)
        {
            switch (playedPile.Count)
            {
                case 1:
                    isSingle = true;
                    setType = 1;
                    break;
                case 2:
                    isDouble = true;
                    setType = 2;
                    break;
                case 3:
                    isTriple = true;
                    setType = 3;
                    break;
                case 4:
                    setType = 4;
                    break;
            }
        }
    }
}