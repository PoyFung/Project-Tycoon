using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class hand
{
    [SerializeField]
    public string playerType; //Current user or Opponents?
    public int numCards; //The numbers of cards currently in hand
}

public class MainTycoon : MonoBehaviour
{
    CreateDeck createDeckScript = new CreateDeck();
    [SerializeField] private GameObject playHand; //Player

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
