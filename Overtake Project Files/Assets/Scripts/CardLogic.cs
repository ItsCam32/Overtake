using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardLogic : MonoBehaviour
{
    public GameObject scripts;
    public Sprite purpleBackground;
    public GameObject[] gridPositions, aiCardArray;
    public List<bool> availableAICards = new List<bool>();
    List<GameObject> possibleSlots = new List<GameObject>();
    GameObject chosenSlot;

    public void StartAIProcess()
    {
      StartCoroutine(PlaceAICard());
    }

    public IEnumerator PlaceAICard()
    {
      yield return new WaitForSeconds(Random.Range(1, 4));
      ChooseAICard();
    }

    public void ChooseAICard()
    {
      int randNum = Random.Range(0, aiCardArray.Length - 1);

      if (availableAICards[randNum] == false)
      {
        ChooseAICard();
        return;
      }

      availableAICards[randNum] = false;
      GameObject selectedCard = GameObject.Find(aiCardArray[randNum].name);

      possibleSlots.Clear();
      foreach (GameObject gridSquare in gridPositions)
      {
        if (gridSquare.GetComponent<BoxCollider2D>().enabled == true)
        {
          // Free square to place card
          possibleSlots.Add(gridSquare);
        }
      }

      // Place card
      chosenSlot = possibleSlots[Random.Range(0, possibleSlots.Count - 1)];
      selectedCard.transform.localScale += new Vector3(26.25f, 26.25f, 0f);
      selectedCard.transform.position = chosenSlot.transform.position;
      selectedCard.layer = 9;
      chosenSlot.GetComponent<BoxCollider2D>().enabled = false;
      MatchUI.seconds = 0;
      Tracking.playerTwosCards++;
      CardDragging.cardInAir = false;

      // Add the grid that the card was just placed in to the list
      int currentGridNumber = int.Parse(chosenSlot.name);
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

          if (checkingGridCard.tag == "P1Card")
          {
            // Cards are on same row
            if (selectedCard.transform.localPosition.y.ToString("0.00") == checkingGridCard.transform.localPosition.y.ToString("0.00"))
            {
              // Placed card is to the left of enemy card
              if (selectedCard.transform.localPosition.x + 1300 < checkingGridCard.transform.localPosition.x)
              {
                int myCardNum = int.Parse(selectedCard.transform.Find("RightNumber").gameObject.GetComponent<TextMeshProUGUI>().text);
                int enemyCardNum = int.Parse(checkingGridCard.transform.Find("LeftNumber").gameObject.GetComponent<TextMeshProUGUI>().text);

                if (myCardNum > enemyCardNum)
                {
                  // Turn enemy card to mine
                  scripts.GetComponent<Stats>().UpdateStats("CardsLost", 1);
                  checkingGridCard.GetComponent<SpriteRenderer>().sprite = purpleBackground;
                  checkingGridCard.tag = "AICard";
                  Tracking.playerOnesCards--;
                  Tracking.playerTwosCards++;
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
                  scripts.GetComponent<Stats>().UpdateStats("CardsLost", 1);
                  checkingGridCard.GetComponent<SpriteRenderer>().sprite = purpleBackground;
                  checkingGridCard.tag = "AICard";
                  Tracking.playerOnesCards--;
                  Tracking.playerTwosCards++;
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
                  scripts.GetComponent<Stats>().UpdateStats("CardsLost", 1);
                  checkingGridCard.GetComponent<SpriteRenderer>().sprite = purpleBackground;
                  checkingGridCard.tag = "AICard";
                  Tracking.playerOnesCards--;
                  Tracking.playerTwosCards++;
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
                  scripts.GetComponent<Stats>().UpdateStats("CardsLost", 1);
                  checkingGridCard.GetComponent<SpriteRenderer>().sprite = purpleBackground;
                  checkingGridCard.tag = "AICard";
                  Tracking.playerOnesCards--;
                  Tracking.playerTwosCards++;
                }
              }
            }
          }
        }
      }
    }

    public void ResetVariables()
    {
      for (int i = 0; i < availableAICards.Count; i++)
      {
        availableAICards[i] = true;
      }

      possibleSlots.Clear();
      chosenSlot = null;
    }
}
