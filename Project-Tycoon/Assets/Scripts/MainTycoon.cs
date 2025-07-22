using UnityEngine;

public class hand
{
    [SerializeField]
    public string playerType; //Current user or Opponents?
    public int numCards; //The numbers of cards currently in hand
}

public class card
{
    [SerializeField]
    public string cardType; //Number, Face or Joker?
    public string cardNumber; //Card Number
    public string cardSuite; //Spades, Clubs, Hearts of Diamonds
}

public class MainTycoon : MonoBehaviour
{
    [SerializeField] private GameObject playHand; //Player

    private int totalCards = 54;
    /* 
    13 CLUBS
    13 SPADES
    13 HEARTS
    13 DIAMONDS
    2 Jokers
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i=0;i<totalCards;i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
