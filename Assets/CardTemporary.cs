using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardTemporary : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float maxXPos;
    CardManager cardManagerReferrence;

    [SerializeField] mouseOver leftSwipe;
    [SerializeField] mouseOver rightSwipe;

    [SerializeField] float maxZRotation;
    [SerializeField] float rotationMultiplier;


    RectTransform canvas;
    float canvasXOffset;

    private void Start()
    {
        cardManagerReferrence = CardManager.instance;
        canvas = CanvasSingleton.instance.GetComponent<RectTransform>();
        canvasXOffset = canvas.position.x;
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Rotation of card

        float differenceBtwMouseAndTransform = Input.mousePosition.x - transform.position.x;

        float distanceToMousePointer = (float)(differenceBtwMouseAndTransform / 100f);

        float zRotation = -1 * Mathf.Clamp(distanceToMousePointer * rotationMultiplier, -maxZRotation, maxZRotation);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, zRotation);


        //Movement of the card

        transform.position = new Vector3(Input.mousePosition.x + canvasXOffset, transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x - canvasXOffset, -maxXPos + canvasXOffset, maxXPos + canvasXOffset), transform.position.y, transform.position.z);

        bool isMouseOverLeftSwipe = leftSwipe.isMouseOver;
        bool isMouseOverRightSwipe = rightSwipe.isMouseOver;

        if (isMouseOverLeftSwipe)
        {
            cardManagerReferrence.leftDescriptionText.gameObject.SetActive(true);
        }
        else if (isMouseOverRightSwipe)
        {
            cardManagerReferrence.rightDescriptionText.gameObject.SetActive(true);
        }
        else
        {
            cardManagerReferrence.leftDescriptionText.gameObject.SetActive(false);
            cardManagerReferrence.rightDescriptionText.gameObject.SetActive(false);
        }


        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Reset transform
        transform.position = new Vector3(canvasXOffset, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 0);


        //Check if it was swiped and what side it was swiped to
        bool isMouseOverLeftSwipe = leftSwipe.isMouseOver;
        bool isMouseOverRightSwipe = rightSwipe.isMouseOver;

        if (isMouseOverLeftSwipe)
        {
            cardManagerReferrence.LeftCardButtonPressed();
        }
        else if (isMouseOverRightSwipe)
        {
            cardManagerReferrence.RightCardButtonPressed();
        }

    }
}
