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
    
    [SerializeField] private GameObject p1Hand; //Player
    [SerializeField] private GameObject handSpace; //Player's Hand on Screen

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillHand();
        cardSpacePositioning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fillHand()
    {
        var mtDeck = CreateDeck.Instance.mainDeck;

        for(int p1HandNumber = 0;p1HandNumber<13;p1HandNumber++)
        {
            var randomCard = mtDeck.ElementAt(UnityEngine.Random.Range(0, mtDeck.Count));
            playerOne.playerHand.Add(randomCard.Value);
            randomCard.Value.cardObject.transform.parent = p1Hand.transform;
            mtDeck.Remove(randomCard.Key);
        }
        playerOne.playerHand.Sort((cardA, cardB) => cardA.cardRank.CompareTo(cardB.cardRank));
    }

    void cardSpacePositioning()
    {
        int handSize = playerOne.playerHand.Count;
        Vector2 spaceCenter = handSpace.transform.position;

        float leftPos = -1 * (handSize / 2);
        int currentCard = 0;
        for (float i= leftPos;i<=handSize/2;i++)
        {
            float spacing = i;
            playerOne.playerHand[currentCard].cardObject.transform.position
                = new Vector3(spacing * 1.25f, handSpace.transform.position.y,i*-1);

            //ACTIVATE CARD INTERACTION
            playerOne.playerHand[currentCard].cardObject.GetComponent<CardInteraction>().enabled=true;

            currentCard++;
            Debug.Log("Hand Size: "+handSize+" ,Spacing: "+spacing);
        }
    }
    void playedHandCheck ()
    {

    }
}