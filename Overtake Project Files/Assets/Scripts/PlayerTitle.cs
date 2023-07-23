using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTitle : MonoBehaviour
{
    public GameObject scripts;
    public GameObject[] boxTextItems;
    public Button box1Button, box2Button, box3Button, box4Button;
    public TextMeshProUGUI playerOneTitleText;
    List<string> ownedTitles = new List<string>();

    public void Start()
    {
      PlayerPrefs.DeleteKey("Cracked");
      PlayerPrefs.DeleteKey("Cheese Expert");
      PlayerPrefs.DeleteKey("Radiant");
      PlayerPrefs.DeleteKey(":-)");
      if (PlayerPrefs.GetString("Cracked", "NA") == "Cracked")
      {
        box1Button.enabled = false;
        boxTextItems[0].SetActive(false);
        boxTextItems[1].SetActive(false);
        boxTextItems[2].SetActive(true);

        if (!ownedTitles.Contains("Cracked"))
        {
          ownedTitles.Add("Cracked");
        }
      }

      if (PlayerPrefs.GetString("Cheese Expert", "NA") == "Cheese Expert")
      {
        box2Button.enabled = false;
        boxTextItems[3].SetActive(false);
        boxTextItems[4].SetActive(false);
        boxTextItems[5].SetActive(true);

        if (!ownedTitles.Contains("Cheese Expert"))
        {
          ownedTitles.Add("Cheese Expert");
        }
      }

      if (PlayerPrefs.GetString("Radiant", "NA") == "Radiant")
      {
        box3Button.enabled = false;
        boxTextItems[6].SetActive(false);
        boxTextItems[7].SetActive(false);
        boxTextItems[8].SetActive(true);

        if (!ownedTitles.Contains("Radiant"))
        {
          ownedTitles.Add("Radiant");
        }
      }

      if (PlayerPrefs.GetString(":-)", "NA") == ":-)")
      {
        box4Button.enabled = false;
        boxTextItems[9].SetActive(false);
        boxTextItems[10].SetActive(false);
        boxTextItems[11].SetActive(true);

        if (!ownedTitles.Contains(":-)"))
        {
          ownedTitles.Add(":-)");
        }
      }
    }

    public void TitlePurchased(string titleName)
    {
      int price = 0;
      if (titleName == "Cracked")
      {
        price = 2000;
      }
      else if (titleName == "Cheese Expert")
      {
        price = 5000;
      }
      else if (titleName == "Radiant")
      {
        price = 10000;
      }
      else if (titleName == ":-)")
      {
        price = 100000;
      }

      if (Coins.coins >= price)
      {
        Coins.coins -= price;
        scripts.GetComponent<Coins>().UpdateCoins();
        PlayerPrefs.SetString(titleName, titleName);
        Start();
      }
    }

    public void SetPlayerOneTitle()
    {
      if (ownedTitles.Count > 0)
      {
        playerOneTitleText.text = ownedTitles[Random.Range(0, ownedTitles.Count)];
      }
      else
      {
        playerOneTitleText.text = "Beginner";
      }
    }

    public void ClearPlayerOneTitle()
    {
      playerOneTitleText.text = null;
    }
}
