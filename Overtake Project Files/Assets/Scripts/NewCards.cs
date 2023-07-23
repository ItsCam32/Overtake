using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewCards : MonoBehaviour
{
    public Sprite purpleBackground;
    public GameObject cardPrefab, scripts;
    public Transform p1Parent, p2Parent, gridsParent;
    public Transform[] p1CardPositions, p2CardPositions;
    List<int> usedSpawnPositionIndexes = new List<int>();
    TextMeshProUGUI numText;
    int[] numbers = new int[] { 0, 0, 0, 0 };

    public void CreateNewCards()
    {
      usedSpawnPositionIndexes.Clear();
      foreach (Transform cardTransform in p1Parent)
      {
        Destroy(cardTransform.gameObject);
      }

      for (int i = 0; i < 12; i++)
      {
        GameObject card = Instantiate(cardPrefab, p1CardPositions[i].position, Quaternion.identity);
        card.transform.SetParent(p1Parent);
        card.transform.position = p1CardPositions[i].position;
        card.transform.localScale += new Vector3(12.79762f, 12.79762f, -6f);
        card.name = "Card" + (i + 1).ToString();
        card.tag = "P1Card";
      }

      foreach (Transform grid in gridsParent)
      {
        grid.gameObject.GetComponent<Collider2D>().enabled = true;
      }

      foreach (Transform p2Child in p2Parent)
      {
        if (p2Child.gameObject.layer == 9)
        {
          p2Child.localScale += new Vector3(-26.25f, -26.25f, 0f);
        }

        p2Child.gameObject.layer = 0;
        p2Child.gameObject.tag = "AICard";
        p2Child.gameObject.GetComponent<SpriteRenderer>().sprite = purpleBackground;

        int i = 0;
        foreach (Transform textChild in p2Child)
        {
          if (i > 3)
          {
            continue;
          }

          numText = textChild.gameObject.GetComponent<TextMeshProUGUI>();

          int randomNumber = Random.Range(2, 10);
          numbers[i] = randomNumber;
          numText.text = randomNumber.ToString();
          i++;
        }

        int randomPercentile = Random.Range(1, 100);
        if (randomPercentile >= 1 && randomPercentile <= 20)
        {
          // Gold
          p2Child.Find("GoldBorder").GetComponent<Image>().enabled = true;
          p2Child.Find("BlueBorder").GetComponent<Image>().enabled = false;
          p2Child.Find("GreyBorder").GetComponent<Image>().enabled = false;
        }

        else if (randomPercentile >= 21 && randomPercentile <= 51)
        {
          // Blue
          p2Child.Find("GoldBorder").GetComponent<Image>().enabled = false;
          p2Child.Find("BlueBorder").GetComponent<Image>().enabled = true;
          p2Child.Find("GreyBorder").GetComponent<Image>().enabled = false;
        }

        else if (randomPercentile >= 52 && randomPercentile <= 100)
        {
          // Grey
          p2Child.Find("GoldBorder").GetComponent<Image>().enabled = false;
          p2Child.Find("BlueBorder").GetComponent<Image>().enabled = false;
          p2Child.Find("GreyBorder").GetComponent<Image>().enabled = true;
        }

        ChooseRandomPos(p2Child);
      }
    }

    public void ChooseRandomPos(Transform p2Card)
    {
      int randIndex = Random.Range(0, p2CardPositions.Length);

      if (usedSpawnPositionIndexes.Contains(randIndex))
      {
        ChooseRandomPos(p2Card);
        return;
      }
      else
      {
        p2Card.position = p2CardPositions[randIndex].position;
        usedSpawnPositionIndexes.Add(randIndex);
      }
    }
}
