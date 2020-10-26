using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    #region Singleton 

    public static CardManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are two card managers, please remove one of them");
        }
    }


    #endregion

    public string mainMenuSceneString;
    ResourceManager resourceManager;

    public TextMeshProUGUI mainDescriptionText;
    public TextMeshProUGUI leftDescriptionText;
    public TextMeshProUGUI rightDescriptionText;

    [Header("Cards")]
    public CardSO[] cards;
    CardSO currentlySelectedCard = null;
    List<CardSO> usedCards;

    [Header("GameOverCards")]
    public CardSO hungerGameOverCard;
    public CardSO thirstGameOverCard;
    public CardSO shelterGameOverCard;
    public CardSO panicGameOverCard;

    [Header("WinCards")]
    public CardSO allCardsCompletedCard;


    bool choseLeft = false;
    bool choseRight = false;

    private void Start()
    {
        resourceManager = ResourceManager.instance;
        usedCards = new List<CardSO>();

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
        bool gameOver = resourceManager.CheckIfGameOver();

        if (gameOver)
        {
            //make currently selected card a gameOverCard

            if(resourceManager.hunger == 0)
            {
                currentlySelectedCard = hungerGameOverCard;
            }
            else if(resourceManager.thirst == 0)
            {
                currentlySelectedCard = thirstGameOverCard;
            }
            else if (resourceManager.shelter == 0)
            {
                currentlySelectedCard = shelterGameOverCard;
            }
            else if (resourceManager.panic == 100)
            {
                currentlySelectedCard = panicGameOverCard;
            }

        }
        else
        {
            if (currentlySelectedCard == null)
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
            if(cards.Length == usedCards.Count)
            {
                //This means that we have looked at all of the cards and therefore the player can win
                nextCard = allCardsCompletedCard;                
            }
            else
            {
                while (nextCard == currentlySelectedCard || usedCards.Contains(nextCard))
                {
                    randCardIndex = Random.Range(0, cards.Length);
                    nextCard = cards[randCardIndex];
                }
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
        if (currentlySelectedCard.isGameOverCard)
        {
            SceneManager.LoadScene(mainMenuSceneString);
        }
        else
        {
            resourceManager.hunger += currentlySelectedCard.left_changeInHunger;
            resourceManager.thirst += currentlySelectedCard.left_changeInThirst;
            resourceManager.shelter += currentlySelectedCard.left_changeInShelter;
            resourceManager.panic += currentlySelectedCard.left_changeInPanic;

            choseLeft = true;
            ChooseNextCard();
        }        
    }

    public void RightCardButtonPressed()
    {
        if (currentlySelectedCard.isGameOverCard)
        {
            SceneManager.LoadScene(mainMenuSceneString);
        }
        else
        {
            resourceManager.hunger += currentlySelectedCard.right_changeInHunger;
            resourceManager.thirst += currentlySelectedCard.right_changeInThirst;
            resourceManager.shelter += currentlySelectedCard.right_changeInShelter;
            resourceManager.panic += currentlySelectedCard.right_changeInPanic;

            //Need to ensure that if this is a follow on card that we do not add it to the list
            if (!currentlySelectedCard.isAFollowUpCard)
            {
                usedCards.Add(currentlySelectedCard);
            }            

            choseRight = true;
            ChooseNextCard();
        }        
    }
    
}
