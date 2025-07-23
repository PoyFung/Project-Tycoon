using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class player
{
    [SerializeField]
    public Dictionary<string,card> playerHand = new Dictionary<string,card>(); //Player's current hand
}

public class MainTycoon : MonoBehaviour
{
    [SerializeField] private GameObject playHand; //Player

    private void Awake()
    {
        player playerOne = new player();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fillHand()
    {
        var mtDeck = CreateDeck.Instance.mainDeck;
        int i = 0;
        foreach(var entry in mtDeck)
        {
            
        }
    }
}
