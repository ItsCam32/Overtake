using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDragging : MonoBehaviour
{
    public GameObject scripts;
    public AudioSource placeAudio;
    public Sprite greenBackground;
    GameObject selectedCard;
    Vector3 pickupPosition;
    public static bool cardInAir = false;

    public void Update()
    {
      MoveCard();

      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
        // Begin drag
        int placingBitMask = 1 << 0;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, placingBitMask);

        PickupCard(hit);
      }

      if (Input.GetKeyUp(KeyCode.Mouse0))
      {
        // End drag
        int gridBitMask = 1 << 8;
        RaycastHit2D placingHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, gridBitMask);

        DropCard(placingHit);
      }
    }

    public void PickupCard(RaycastHit2D hit)
    {
      if (hit.collider != null && selectedCard == null)
      {
        if (hit.collider.tag == "P1Card" && MatchUI.turn == true)
        {
          // Pickup card
          selectedCard = hit.collider.gameObject;
          pickupPosition = hit.collider.gameObject.transform.position;
          cardInAir = true;
        }
      }
    }

    public void DropCard(RaycastHit2D placingHit)
    {
      if (placingHit.collider != null && selectedCard != null)
      {
        // Drop card in slot
        selectedCard.transform.position = placingHit.collider.gameObject.transform.position;
        selectedCard.layer = 9;
        selectedCard.transform.localScale += new Vector3(26.25f, 26.25f, 0f);
        placingHit.collider.enabled = false;
        MatchUI.seconds = 0;
        cardInAir = false;
        Tracking.playerOnesCards++;
        placeAudio.Play(0);

        // Add the grid that the card was just placed in to the list
        int currentGridNumber = int.Parse(placingHit.collider.gameObject.name);
        Tracking.gridOccupationList[currentGridNumber - 1] = selectedCard;

        // Look at surrounding grids for cards
        int[] directions = new int[] { -3, 3, -1, 1 }; // Up down left right
        foreach (int direction in directions)
        {
          if (currentGridNumber == 1)
          {
            if (direction == -3 || direction == -1)
            {
              continue;
            }
          }
          else if (currentGridNumber == 2)
          {
            if (direction == -3)
            {
              continue;
            }
          }
          else if (currentGridNumber == 3)
          {
            if (direction == -3 || direction == 1)
            {
              continue;
            }
          }
          else if (currentGridNumber == 4)
          {
            if (direction == -1)
            {
              continue;
            }
          }
          else if (currentGridNumber == 6)
          {
            if (direction == 1)
            {
              continue;
            }
          }
          else if (currentGridNumber == 7)
          {
            if (direction == 3 || direction == -1)
            {
              continue;
            }
          }
          else if (currentGridNumber == 8)
          {
            if (direction == 3)
            {
              continue;
            }
          }
          else if (currentGridNumber == 9)
          {
            if (direction == 3 || direction == 1)
            {
              continue;
            }
          }

          GameObject gridToCheck = GameObject.Find((currentGridNumber + direction).ToString());

          // Check this grid's card and see if it should be flipped
          if (Tracking.gridOccupationList[int.Parse(gridToCheck.name) - 1] != null)
          {
            GameObject checkingGridCard = Tracking.gridOccupationList[int.Parse(gridToCheck.name) - 1];

            if (checkingGridCard.tag == "AICard")
            {
              // Cards are on same row
              if (selectedCard.transform.localPosition.y.ToString("0.00") == checkingGridCard.transform.localPosition.y.ToString("0.00"))
              {
                // Placed card is to the left of enemy card
                if (selectedCard.transform.localPosition.x < checkingGridCard.transform.localPosition.x + 1300)
                {
                  int myCardNum = int.Parse(selectedCard.transform.Find("RightNumber").gameObject.GetComponent<TextMeshProUGUI>().text);
                  int enemyCardNum = int.Parse(checkingGridCard.transform.Find("LeftNumber").gameObject.GetComponent<TextMeshProUGUI>().text);

                  if (myCardNum > enemyCardNum)
                  {
                    // Turn enemy card to mine
                    scripts.GetComponent<Stats>().UpdateStats("CardsTaken", 1);
                    checkingGridCard.GetComponent<SpriteRenderer>().sprite = greenBackground;
                    checkingGridCard.tag = "P1Card";
                    Tracking.playerOnesCards++;
                    Tracking.playerTwosCards--;
                  }
                }

                // Placed card is to the right of enemy card
                else
                {
                  int myCardNum = int.Parse(selectedCard.transform.Find("LeftNumber").gameObject.GetComponent<TextMeshProUGUI>().text);
                  int enemyCardNum = int.Parse(checkingGridCard.transform.Find("RightNumber").gameObject.GetComponent<TextMeshProUGUI>().text);

                  if (myCardNum > enemyCardNum)
                  {
                    // Turn enemy card to mine
                    scripts.GetComponent<Stats>().UpdateStats("CardsTaken", 1);
                    checkingGridCard.GetComponent<SpriteRenderer>().sprite = greenBackground;
                    checkingGridCard.tag = "P1Card";
                    Tracking.playerOnesCards++;
                    Tracking.playerTwosCards--;
                  }
                }
              }

              // Cards are on different rows (only by 1)
              if (selectedCard.transform.localPosition.y.ToString("0.00") != checkingGridCard.transform.localPosition.y.ToString("0.00"))
              {
                // Placed card is above enemy card
                if (selectedCard.transform.localPosition.y > checkingGridCard.transform.localPosition.y)
                {
                  int myCardNum = int.Parse(selectedCard.transform.Find("BottomNumber").gameObject.GetComponent<TextMeshProUGUI>().text);
                  int enemyCardNum = int.Parse(checkingGridCard.transform.Find("TopNumber").gameObject.GetComponent<TextMeshProUGUI>().text);

                  if (myCardNum > enemyCardNum)
                  {
                    // Turn enemy card to mine
                    scripts.GetComponent<Stats>().UpdateStats("CardsTaken", 1);
                    checkingGridCard.GetComponent<SpriteRenderer>().sprite = greenBackground;
                    checkingGridCard.tag = "P1Card";
                    Tracking.playerOnesCards++;
                    Tracking.playerTwosCards--;
                  }
                }

                // Placed card is below enemy card
                else
                {
                  int myCardNum = int.Parse(selectedCard.transform.Find("TopNumber").gameObject.GetComponent<TextMeshProUGUI>().text);
                  int enemyCardNum = int.Parse(checkingGridCard.transform.Find("BottomNumber").gameObject.GetComponent<TextMeshProUGUI>().text);

                  if (myCardNum > enemyCardNum)
                  {
                    // Turn enemy card to mine
                    scripts.GetComponent<Stats>().UpdateStats("CardsTaken", 1);
                    checkingGridCard.GetComponent<SpriteRenderer>().sprite = greenBackground;
                    checkingGridCard.tag = "P1Card";
                    Tracking.playerOnesCards++;
                    Tracking.playerTwosCards--;
                  }
                }
              }
            }
          }
        }
      }
      else
      {
        if (selectedCard != null)
        {
          // Drop card back at pickup point
          selectedCard.transform.position = pickupPosition;
          cardInAir = false;
        }
      }
      selectedCard = null;
    }

    public void DropCardTimeout()
    {
      // Drop card back at pickup point
      selectedCard.transform.position = pickupPosition;
      selectedCard = null;
      cardInAir = false;
    }

    public void MoveCard()
    {
      if (selectedCard != null)
      {
        // Move card
        selectedCard.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
      }
    }

    public void ResetVariables()
    {
      selectedCard = null;
      pickupPosition = new Vector3(0, 0, 0);
      cardInAir = false;
    }
}
