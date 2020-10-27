using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardSO : ScriptableObject
{
    [Header("GeneralCardInfo")]
    public Sprite cardSprite;

    [TextArea(5,10)]
    public string mainDescription;

    public string leftDescription;
    public string rightDescription;

    [Header("FollowUpCards - (you can leave this blank if it has no follow up cards)")]
    public bool isAFollowUpCard;
    public CardSO[] leftFollowUpCards;
    public CardSO[] rightFollowUpCards;

    [Header("LeftResult")]
    public int left_changeInFood;
    public int left_changeInWater;
    public int left_changeInEnergy;
    public int left_changeInSanity;

    [Header("RightResult")]
    public int right_changeInFood;
    public int right_changeInWater;
    public int right_changeInEnergy;
    public int right_changeInSanity;

    [Header("MiniGame - (you can leave this blank if it is not a minigame card)")]
    public bool isMinigameCard;
    public string miniGameSceneString;


    [Header("GameOverCard - (you can leave this blank if it is not a game over card)")]
    public bool isGameOverCard;


}
