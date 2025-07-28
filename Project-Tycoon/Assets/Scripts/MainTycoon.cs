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
    player playerOne = new player();
    player playerTwo = new player();
    player playerThree = new player();
    player playerFour = new player();
    
    [SerializeField] private GameObject p1Hand;
    [SerializeField] private GameObject p2Hand;
    [SerializeField] private GameObject p3Hand;
    [SerializeField] private GameObject p4Hand;
    [SerializeField] private GameObject handSpace; //Player's Hand on Screen

    List<int> deckSplit = new List<int>() {13,13,14,14};

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillPlayerHand(playerOne,p1Hand);
        fillPlayerHand(playerTwo,p2Hand);
        fillPlayerHand(playerThree,p3Hand);
        fillPlayerHand(playerFour,p4Hand);

        cardSpacePositioning(playerOne);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fillPlayerHand(player inputPlayer, GameObject pHand)
    {
        var mtDeck = CreateDeck.Instance.mainDeck;

        int pickRandomSplit = UnityEngine.Random.Range(0, deckSplit.Count);
        int pickedNum = deckSplit[pickRandomSplit];
        deckSplit.RemoveAt(pickRandomSplit);

        Debug.Log(pickedNum);

        for(int p1HandNumber = 0;p1HandNumber<pickedNum;p1HandNumber++)
        {
            var randomCard = mtDeck.ElementAt(UnityEngine.Random.Range(0, mtDeck.Count));
            inputPlayer.playerHand.Add(randomCard.Value);
            randomCard.Value.cardObject.transform.parent = pHand.transform;
            mtDeck.Remove(randomCard.Key);
        }
        inputPlayer.playerHand.Sort((cardA, cardB) => cardA.cardRank.CompareTo(cardB.cardRank));
    }

    void cardSpacePositioning(player inputPlayer)
    {
        int handSize = inputPlayer.playerHand.Count;
        Vector2 spaceCenter = handSpace.transform.position;

        float leftPos = -1 * (handSize / 2)+0.5f;
        int currentCard = 0;
        for (float i= leftPos;i<handSize/2;i++)
        {
            float spacing = i;
            inputPlayer.playerHand[currentCard].cardObject.transform.position
                = new Vector3(spacing * 1.25f, handSpace.transform.position.y,i*-1);

            //ACTIVATE CARD INTERACTION
            inputPlayer.playerHand[currentCard].cardObject.GetComponent<CardInteraction>().enabled=true;

            currentCard++;
            //Debug.Log("Hand Size: "+handSize+" ,Spacing: "+spacing);
        }
    }
    void possiblePlays ()
    {

    }
}