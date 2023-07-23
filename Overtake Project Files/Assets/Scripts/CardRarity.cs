using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardRarity : MonoBehaviour
{
    public void Start()
    {
      // Choose card rarity from probability
      int randomPercentile = Random.Range(1, 100);

      if (randomPercentile >= 1 && randomPercentile <= 20)
      {
        // Gold
        gameObject.transform.Find("GoldBorder").GetComponent<Image>().enabled = true;
        gameObject.transform.Find("BlueBorder").GetComponent<Image>().enabled = false;
        gameObject.transform.Find("GreyBorder").GetComponent<Image>().enabled = false;
      }

      else if (randomPercentile >= 21 && randomPercentile <= 51)
      {
        // Blue
        gameObject.transform.Find("GoldBorder").GetComponent<Image>().enabled = false;
        gameObject.transform.Find("BlueBorder").GetComponent<Image>().enabled = true;
        gameObject.transform.Find("GreyBorder").GetComponent<Image>().enabled = false;
      }

      else if (randomPercentile >= 52 && randomPercentile <= 100)
      {
        // Grey
        gameObject.transform.Find("GoldBorder").GetComponent<Image>().enabled = false;
        gameObject.transform.Find("BlueBorder").GetComponent<Image>().enabled = false;
        gameObject.transform.Find("GreyBorder").GetComponent<Image>().enabled = true;
      }
    }
}
