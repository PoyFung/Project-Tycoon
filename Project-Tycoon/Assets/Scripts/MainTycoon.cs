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
    public List<card> playerHand = new List<card>();
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
    
    [SerializeField] private GameObject p1HandSpace; //Player's Hand on Screen
    [SerializeField] private GameObject p2HandSpace; //Player's Hand on Screen
    [SerializeField] private GameObject p3HandSpace; //Player's Hand on Screen
    [SerializeField] private GameObject p4HandSpace; //Player's Hand on Screen
    //-------------------------------------------

    //GAME PROPERTIES----------------------------
    List<int> deckSplit = new List<int>() {13,13,14,14}; //Splitting the deck
    public List<card> playedPile = new List<card>();

    /*
    //Type of hands being played
    private bool isSingle = false;
    private bool isDouble = false;
    private bool isTriple = false;
    private bool isQuadruple = false;
    private bool isRevolution = false;
    */

    private void Awake()
    {
        instance = this;
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
            //Debug.Log(mtDeck.Count);
            var randomCard = mtDeck.ElementAt(UnityEngine.Random.Range(0, mtDeck.Count));
            inputPlayer.playerHand.Add(randomCard.Value);
            mtDeck.Remove(randomCard.Key);
        }
        inputPlayer.playerHand.Sort((cardA, cardB) => cardA.cardRank.CompareTo(cardB.cardRank));

        //Place cards from list into Player Parent
        foreach (card pCard in inputPlayer.playerHand)
        {
            pCard.cardObject.transform.parent = pHand.transform;
        }
    }

    void playerHandPositioning(player inputPlayer,GameObject handSpace)
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
            inputPlayer.playerHand[currentCard].cardObject.transform.position
                = new Vector3(cardPos * 1.25f, handSpace.transform.position.y,cardPos*-1);

            //ACTIVATE CARD INTERACTION
            inputPlayer.playerHand[currentCard].cardObject.GetComponent<CardInteraction>().enabled=true;
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
            opCard.cardObject.transform.position = handSpace.transform.position;
        }
    }
    void displayPossiblePlays ()
    {
        //playerOne.playerHand.ForEach(pCard => {if(pCard.cardRank==)});
    }
}