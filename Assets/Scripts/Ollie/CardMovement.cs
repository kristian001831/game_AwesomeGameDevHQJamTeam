using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : MonoBehaviour
{
    CardManager cardManagerReferrence;

    [Header("General Variables")]
    [SerializeField] float maxZRotation;
    [SerializeField] float maxXMovement;


    float originalX_worldSpace;
    [SerializeField] Vector3 originalRotation;

    [SerializeField] float cardMoveSpeed;

    bool isTouchingLeftSwipe;
    bool isTouchingRightSwipe;

    [Header("UI To Move")]
    [SerializeField] RectTransform textParent;

    private void Start()
    {
        cardManagerReferrence = CardManager.instance;
        originalX_worldSpace = transform.parent.position.x;
    }

    private void OnMouseDrag()
    {
        //Rotation of card
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        float distanceToMousePointer = mousePos.x - transform.position.x;

        float zRotation = -1 * Mathf.Clamp(distanceToMousePointer, -maxZRotation, maxZRotation);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, zRotation);

        //Movement of card
        float xPos = Mathf.Clamp(transform.position.x + distanceToMousePointer, originalX_worldSpace - maxXMovement, originalX_worldSpace + maxXMovement);

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        //Checking what side the card has been swiped to

        if (isTouchingLeftSwipe)
        {
            cardManagerReferrence.leftDescriptionText.gameObject.SetActive(true);
        }
        else if(isTouchingRightSwipe)
        {
            cardManagerReferrence.rightDescriptionText.gameObject.SetActive(true);
        }
        else
        {
            cardManagerReferrence.leftDescriptionText.gameObject.SetActive(false);
            cardManagerReferrence.rightDescriptionText.gameObject.SetActive(false);
        }

        textParent.localPosition = new Vector3(transform.localPosition.x * 100, textParent.localPosition.y, textParent.localPosition.z);


    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            if (isTouchingLeftSwipe)
            {
                cardManagerReferrence.LeftCardButtonPressed();
            }
            else if (isTouchingRightSwipe)
            {
                cardManagerReferrence.RightCardButtonPressed();
            }


            transform.localPosition = Vector3.zero;
            transform.rotation = Quaternion.Euler(originalRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LeftSwipeBox")
        {
            isTouchingLeftSwipe = true;
        }

        if(other.tag == "RightSwipeBox")
        {
            isTouchingRightSwipe = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LeftSwipeBox")
        {
            isTouchingLeftSwipe = false;
        }

        if (other.tag == "RightSwipeBox")
        {
            isTouchingRightSwipe = false;
        }
    }
}
