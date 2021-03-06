using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public enum DirectionContact
    {
        UP, RIGHT, DOWN, LEFT, NONE,
    };

    public DirectionContact directionContact = DirectionContact.NONE;

    private Player_Move player_Move;

    private void Start()
    {
        player_Move = this.GetComponent<Player_Move>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Plateform")
        {
            Vector2 contactPosition = collision.GetContact(0).point;
            BoxCollider2D platformCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            Vector2 normalContact = collision.GetContact(0).normal;

            // CompareAxisCoordoonee(platformCollider, contactPosition);

            CheckNormalContactPoint(contactPosition, normalContact);

        }


    }

    // Find the direction with the normal
    void CheckNormalContactPoint(Vector2 contactPosition, Vector2 normalContact)
    {
        Debug.Log("Debug normal " + normalContact.normalized);
        Debug.DrawRay(contactPosition, normalContact.normalized * 10.0f, Color.green);

        if (normalContact.y == 1)
        {
            directionContact = DirectionContact.UP;


        }
        if (normalContact.y == -1)
        {
            directionContact = DirectionContact.DOWN;
            player_Move.ReturnForce(normalContact,0.5f);


        }
        if (normalContact.x == 1)
        {
            directionContact = DirectionContact.RIGHT;
            player_Move.grounded = false;
            if (!player_Move.grounded)
            {
                player_Move.ReturnForce(normalContact,1);

                //Debug.Break();
            }
        }
        if (normalContact.x == -1)
        {
            player_Move.grounded = false;
            directionContact = DirectionContact.LEFT;
            if (!player_Move.grounded)
            {
                player_Move.ReturnForce(normalContact,1);

            }

        }
         return;


    }

    // Find the direction by comparing coordonate 
    void CompareAxisCoordoonee(BoxCollider2D platformCollider, Vector2 contactPosition)
    {
        Debug.Log("Contact Point = " + contactPosition);
        float higherPoint = platformCollider.bounds.center.y + platformCollider.bounds.extents.y;
        Debug.Log("Higher Point = " + higherPoint);
        float lowerPoint = platformCollider.bounds.center.y - platformCollider.bounds.extents.y;

        float righterPoint = platformCollider.bounds.center.x + platformCollider.bounds.extents.x;
        float lefterPoint = platformCollider.bounds.center.x - platformCollider.bounds.extents.x;

        if (contactPosition.y >= higherPoint)
        {
            directionContact = DirectionContact.UP;
            return;
        }
        if (contactPosition.y <= lowerPoint)
        {
            directionContact = DirectionContact.DOWN;
            return;
        }

        if (contactPosition.x >= righterPoint)
        {
            directionContact = DirectionContact.RIGHT;
            return;
        }

        if (contactPosition.x <= lefterPoint)
        {
            directionContact = DirectionContact.LEFT;
            return;
        }
        directionContact = DirectionContact.NONE;
    }
}

