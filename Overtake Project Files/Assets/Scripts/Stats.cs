using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI gamesWonText, gamesLostText, winRateText, cardsTakenText, cardsLostText;

    public void Start()
    {
      PlayerPrefs.DeleteKey("GamesWon");
      PlayerPrefs.DeleteKey("GamesLost");
      PlayerPrefs.DeleteKey("CardsTaken");
      PlayerPrefs.DeleteKey("CardsLost");
      PlayerPrefs.DeleteKey("WinRate");

      UpdateStats("FromStartMethod", 0);
    }

    public void UpdateStats(string key, int valueToAdd)
    {
      if (key != "FromStartMethod")
      {
        int currentValue = PlayerPrefs.GetInt(key, 0);
        int newValue = currentValue + valueToAdd;
        PlayerPrefs.SetInt(key, newValue);
      }

      gamesWonText.text = "Games Won   " + PlayerPrefs.GetInt("GamesWon", 0).ToString();
      gamesLostText.text = "Games Lost   " + PlayerPrefs.GetInt("GamesLost", 0).ToString();
      cardsTakenText.text = "Cards Taken   " + PlayerPrefs.GetInt("CardsTaken", 0).ToString();
      cardsLostText.text = "Cards Lost   " + PlayerPrefs.GetInt("CardsLost", 0).ToString();

      float totalGamesPlayed = PlayerPrefs.GetInt("GamesWon", 0) + PlayerPrefs.GetInt("GamesLost", 0);

      if (totalGamesPlayed > 0)
      {
        float winPercentage = ((float)PlayerPrefs.GetInt("GamesWon", 0) / totalGamesPlayed) * 100f;
        PlayerPrefs.SetFloat("WinRate", winPercentage);
      }
      else
      {
        PlayerPrefs.SetFloat("WinRate", 0);
      }

      winRateText.text = "Win Rate   " + PlayerPrefs.GetFloat("WinRate", 0f).ToString("F2") + "%";
    }
}
