using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class CreateDeck : MonoBehaviour
{
    public static CreateDeck instance { get; private set; }
    public Dictionary<string, GameObject> mainDeck = new Dictionary<string, GameObject>() { };

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject mainDeckObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Assign the singleton instance
            DontDestroyOnLoad(gameObject); // Optional: keep between scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }

        createNewDeck();
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
            createCard(i + "H","Number",rank,"Heart");
            createCard(i + "D","Number",rank,"Diamond");
            createCard(i + "S","Number",rank,"Spade");
            createCard(i + "C","Number",rank,"Club");

        }

        for (int i = 11; i <= 15; i++)
        {
            int rank = i - 2;
            if (rank <= 11)
            {
                switch (rank)
                {
                    case 9:
                        createCard("JH", "Face", rank, "Heart");
                        createCard("JD", "Face", rank, "Diamond");
                        createCard("JS", "Face", rank, "Spade");
                        createCard("JC", "Face", rank, "Club");
                        break;

                    case 10:
                        createCard("QH", "Face", rank, "Heart");
                        createCard("QD", "Face", rank, "Diamond");
                        createCard("QS", "Face", rank, "Spade");
                        createCard("QC", "Face", rank, "Club");
                        break;

                    case 11:
                        createCard("KH", "Face", rank, "Heart");
                        createCard("KD", "Face", rank, "Diamond");
                        createCard("KS", "Face", rank, "Spade");
                        createCard("KC", "Face", rank, "Club");
                        break;
                }
            }

            else if (rank > 11)
            {
                switch (rank)
                {
                    case 12:
                        createCard("AH", "Number", rank, "Heart");
                        createCard("AD", "Number", rank, "Diamond");
                        createCard("AS", "Number", rank, "Spade");
                        createCard("AC", "Number", rank, "Club");
                        break;

                    case 13:
                        createCard("2H", "Number", rank, "Heart");
                        createCard("2D", "Number", rank, "Diamond");
                        createCard("2S", "Number", rank, "Spade");
                        createCard("2C", "Number", rank, "Club");
                        break;
                }
            }
        }
        createCard("Joker1", "Number", 24, "Wild");
        createCard("Joker2", "Number", 24, "Wild");
        shuffleDeck();
    }

    void createCard(string inputName, string inputType, int inputRank, string inputSuite)
    {
        GameObject cardObject = Instantiate(cardPrefab, mainDeckObject.transform);

        CardProperties cardProperties = cardObject.GetComponent<CardProperties>();
        TextMeshPro cardText = cardObject.GetComponentInChildren<TextMeshPro>();

        cardProperties.cardName = inputName;
        cardProperties.cardType = inputType;
        cardProperties.cardRank = inputRank;
        cardProperties.cardSuite = inputSuite;
        cardText.text = inputName;
        cardObject.name = inputName;
        
        mainDeck.Add(inputName,cardObject);
    }

    private void shuffleDeck()
    {
        var shuffled = mainDeck.OrderBy(x => Random.value).ToList();
        mainDeck = shuffled.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}