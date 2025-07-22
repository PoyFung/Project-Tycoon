using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class card
{
    [SerializeField]
    public string cardType; //Number, Face or Joker?
    public int cardRank; //Card Number
    public string cardSuite; //Hearts, Diamonds, Spades and Clubs
}
public class CreateDeck : MonoBehaviour
{
    private int totalCards = 54;
    [SerializeField] private Dictionary<string, card> mainDeck = new Dictionary<string, card>() { };
    private void Awake()
    {
        createNewDeck();
        shuffleDeck();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var entry in mainDeck)
        {
            Debug.Log(entry);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createNewDeck()
    {
        /* 13 HEARTS
           13 DIAMONDS
           13 SPADES
           13 CLUBS
           2 Jokers */

        for (int i = 3; i <= 10; i++)
        {
            int rank = i - 2;
            card newHeart = new card() { cardType = "Number", cardRank = rank, cardSuite = "Heart" };
            card newDiamond = new card() { cardType = "Number", cardRank = rank, cardSuite = "Diamond" };
            card newSpade = new card() { cardType = "Number", cardRank = rank, cardSuite = "Spade" };
            card newClub = new card() { cardType = "Number", cardRank = rank, cardSuite = "Club" };

            mainDeck.Add(i + "H", newHeart);
            mainDeck.Add(i + "D", newDiamond);
            mainDeck.Add(i + "S", newSpade);
            mainDeck.Add(i + "C", newClub);
        }

        for (int i = 11; i <= 15; i++)
        {
            int rank = i - 2;
            if (rank <= 11)
            {
                card newHeart = new card() { cardType = "Face", cardRank = rank, cardSuite = "Heart" };
                card newDiamond = new card() { cardType = "Face", cardRank = rank, cardSuite = "Diamond" };
                card newSpade = new card() { cardType = "Face", cardRank = rank, cardSuite = "Spade" };
                card newClub = new card() { cardType = "Face", cardRank = rank, cardSuite = "Club" };

                switch (rank)
                {
                    case 9:
                        mainDeck.Add("JH", newHeart);
                        mainDeck.Add("JD", newDiamond);
                        mainDeck.Add("JS", newSpade);
                        mainDeck.Add("JC", newClub);
                        break;

                    case 10:
                        mainDeck.Add("QH", newHeart);
                        mainDeck.Add("QD", newDiamond);
                        mainDeck.Add("QS", newSpade);
                        mainDeck.Add("QC", newClub);
                        break;

                    case 11:
                        mainDeck.Add("KH", newHeart);
                        mainDeck.Add("KD", newDiamond);
                        mainDeck.Add("KS", newSpade);
                        mainDeck.Add("KC", newClub);
                        break;
                }
            }

            else if (rank > 11)
            {
                card newHeart = new card() { cardType = "Number", cardRank = rank, cardSuite = "Heart" };
                card newDiamond = new card() { cardType = "Number", cardRank = rank, cardSuite = "Diamond" };
                card newSpade = new card() { cardType = "Number", cardRank = rank, cardSuite = "Spade" };
                card newClub = new card() { cardType = "Number", cardRank = rank, cardSuite = "Club" };

                switch (rank)
                {
                    case 12:
                        mainDeck.Add("AH", newHeart);
                        mainDeck.Add("AD", newDiamond);
                        mainDeck.Add("AS", newSpade);
                        mainDeck.Add("AC", newClub);
                        break;

                    case 13:
                        mainDeck.Add("2H", newHeart);
                        mainDeck.Add("2D", newDiamond);
                        mainDeck.Add("2S", newSpade);
                        mainDeck.Add("2C", newClub);
                        break;
                }
            }
        }
        card newJoker = new card() { cardType = "Wild", cardRank = 24, cardSuite = "" };
        mainDeck.Add("Joker1", newJoker);
        mainDeck.Add("Joker2", newJoker);
    }

    private void shuffleDeck()
    {
        var shuffled = mainDeck.OrderBy(x => Random.value).ToList();
        mainDeck = shuffled.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}