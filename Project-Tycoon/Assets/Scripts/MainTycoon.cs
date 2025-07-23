using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class player
{
    [SerializeField]
    public Dictionary<string,card> playerHand = new Dictionary<string,card>(); //Player's current hand
}

public class MainTycoon : MonoBehaviour
{
    [SerializeField] private GameObject p1Hand; //Player
    player playerOne = new player();

    [SerializeField] private GameObject cardPrefab;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fillHand()
    {
        var mtDeck = CreateDeck.Instance.mainDeck;

        for(int p1HandNumber = 0;p1HandNumber<2;p1HandNumber++)
        {
            var randomCard = mtDeck.ElementAt(UnityEngine.Random.Range(0, mtDeck.Count));
            playerOne.playerHand.Add(randomCard.Key, randomCard.Value);
            createCardObject(randomCard.Key);
            mtDeck.Remove(randomCard.Key);
        }
    }

    void createCardObject(string cardName)
    {
        GameObject cardObject = Instantiate(cardPrefab, p1Hand.transform);
        TextMeshProUGUI cardText = cardObject.GetComponentInChildren<TextMeshProUGUI>();
        cardText.text = cardName;
    }
}
