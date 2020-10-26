using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    ResourceManager resourceManager;

    public CardSO[] cards;
    CardSO currentlySelectedCard = null;

    public TextMeshProUGUI mainDescriptionText;
    public TextMeshProUGUI leftDescriptionText;
    public TextMeshProUGUI rightDescriptionText;

    bool choseLeft = false;
    bool choseRight = false;

    private void Start()
    {
        resourceManager = ResourceManager.instance;

        if(cards.Length == 0)
        {
            Debug.LogError("You have not added any cards to the array!");
        }
        else
        {
            ChooseNextCard();
        }
    }


    void ChooseNextCard()
    {
        resourceManager.UpdateResourcesDisplay();

        if(currentlySelectedCard == null)
        {
            //Picks out random card from all the cards
            ChooseNonFollowUpCard();
        }
        else
        {
            if (choseLeft && currentlySelectedCard.leftFollowUpCards.Length > 0)
            {
                ChooseFollowUpCard(true);
            }
            else if (choseRight && currentlySelectedCard.rightFollowUpCards.Length > 0)
            {
                ChooseFollowUpCard(false);
            }
            else
            {
                //Picks out random card from all the cards
                ChooseNonFollowUpCard();
            }
        }

        choseLeft = false;
        choseRight = false;

        UpdateCardDisplay();
    }

    void ChooseFollowUpCard(bool left)
    {
        //if left = true then we choose from the left follow up cards
        //if left = false then we choose from the right follow up cards

        if (left)
        {
            int randFollowUpCardIndex = Random.Range(0, currentlySelectedCard.leftFollowUpCards.Length);
            currentlySelectedCard = currentlySelectedCard.leftFollowUpCards[randFollowUpCardIndex];
        }
        else
        {
            int randFollowUpCardIndex = Random.Range(0, currentlySelectedCard.rightFollowUpCards.Length);
            currentlySelectedCard = currentlySelectedCard.rightFollowUpCards[randFollowUpCardIndex];
        }
        
    }

    void ChooseNonFollowUpCard()
    {
        int randCardIndex = Random.Range(0, cards.Length);
        CardSO nextCard = cards[randCardIndex];

        if (currentlySelectedCard != null)
        {
            //Loop to ensure that the same card cannot appear again
            while (nextCard == currentlySelectedCard)
            {
                randCardIndex = Random.Range(0, cards.Length);
                nextCard = cards[randCardIndex];
            }
        }

        currentlySelectedCard = nextCard;
    }

    void UpdateCardDisplay()
    {
        mainDescriptionText.text = currentlySelectedCard.mainDescription;
        leftDescriptionText.text = currentlySelectedCard.leftDescription;
        rightDescriptionText.text = currentlySelectedCard.rightDescription;
    }

    public void LeftCardButtonPressed()
    {
        resourceManager.hunger += currentlySelectedCard.left_changeInHunger;
        resourceManager.thirst += currentlySelectedCard.left_changeInThirst;
        resourceManager.shelter += currentlySelectedCard.left_changeInShelter;
        resourceManager.panic += currentlySelectedCard.left_changeInPanic;

        choseLeft = true;
        ChooseNextCard();
    }

    public void RightCardButtonPressed()
    {
        resourceManager.hunger += currentlySelectedCard.right_changeInHunger;
        resourceManager.thirst += currentlySelectedCard.right_changeInThirst;
        resourceManager.shelter += currentlySelectedCard.right_changeInShelter;
        resourceManager.panic += currentlySelectedCard.right_changeInPanic;

        choseRight = true;
        ChooseNextCard();
    }



}
