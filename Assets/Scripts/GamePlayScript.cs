using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NewBehaviourScript : MonoBehaviour
{
    private List<string> deck;
    private List<string> playerHand;
    private List<string> dealerHand;
    private bool isPlayerFinished;

    private void Start()
    {
        InitializeDeck();
        SuffleDeck();
        StartGame();
    }

    public void InitializeDeck()
    {
        deck = new List<string>();
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        foreach (var suit in suits)
        {
            foreach (var value in values)
            {
                deck.Add($"{value} of {suit}");
            }
        }
    }

    public void SuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            string temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void StartGame()
    {
        playerHand = new List<string> { GetCard(), GetCard()};
        dealerHand = new List<string> { GetCard(), GetCard()};
        
        Debug.Log(playerHand[0] + " + " + playerHand[1]);
        Debug.Log(dealerHand[0] + "= ClosedCard in game!" + " + " + dealerHand[1]);

        PlayerTurn();
    }

    public string GetCard()
    {
        string card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    public void PlayerTurn()
    {
        isPlayerFinished = false;

        while (isPlayerFinished)
        {
            int playerScore = CalculateScore(playerHand);
        }
    }

    public int CalculateScore(List<string> hand)
    {
        int score = 0;
        int aces = 0;

        foreach (var card in hand)
        {
            string value = card.Split(' ')[0];
            if (int.TryParse(value, out int cardValue))
            {
                score += cardValue;
            }
            else if (value == "A")
            {
                score += 11; // Initially count Ace as 11
                aces++;
            }
            else
            {
                score += 10; // J, Q, K
            }
        }

        // Adjust for Aces
        while (score > 21 && aces > 0)
        {
            score -= 10; // Count Ace as 1 instead of 11
            aces--;
        }

        return score;
    }
}
