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
        foreach (var card in playerOne.playerHand)
        {
            Debug.Log(card);
        }
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
    }

    void cardSpacePositioning()
    {
        int handSize = playerOne.playerHand.Count;
        Vector2 spaceCenter = handSpace.transform.position;

        float leftPos = -1 * (handSize / 2);

        int currentCard = 0;
        for (int i= -1*((handSize-1)/2);i<=handSize/2;i++)
        {
            float spacing = i;
            playerOne.playerHand[currentCard].cardObject.transform.position
                = new Vector2(spacing * 1.25f, handSpace.transform.position.y);
            currentCard++;
        }
    }
}