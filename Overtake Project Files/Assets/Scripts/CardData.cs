using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardData : MonoBehaviour
{
    TextMeshProUGUI numText;
    int[] numbers = new int[] { 0, 0, 0, 0 };

    public void Start()
    {
      GenerateRandomValues();
    }

    public void GenerateRandomValues()
    {
      int i = 0;
      foreach (Transform textChild in gameObject.transform)
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
    }
}
