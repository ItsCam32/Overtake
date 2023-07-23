using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetGame : MonoBehaviour
{
    public GameObject scripts;
    public GameObject resultsMenu, homeMenu, gameCanvas, menuCanvas, bottomButtons;
    public TextMeshProUGUI p1Name, p2Name, winText, newBalanceText;
    public Color winColor, loseColor;

    public void ResetCurrentGame(string pOneName, string pTwoName, bool win)
    {
      scripts.GetComponent<MatchUI>().ResetVariables();
      scripts.GetComponent<CardLogic>().ResetVariables();
      scripts.GetComponent<CardDragging>().ResetVariables();
      scripts.GetComponent<NewCards>().CreateNewCards();

      gameCanvas.SetActive(false);
      menuCanvas.SetActive(true);
      homeMenu.SetActive(false);
      bottomButtons.SetActive(false);
      resultsMenu.SetActive(true);

      p1Name.text = pOneName;
      p2Name.text = pTwoName;

      if (win == true)
      {
        winText.text = "WIN";
        winText.color = winColor;
        newBalanceText.text = (Coins.coins + MainMenu.wagerAmount * 2).ToString() + " (+" + MainMenu.wagerAmount + ")";
        Coins.coins += (MainMenu.wagerAmount * 2);
        scripts.GetComponent<Coins>().UpdateCoins();
        scripts.GetComponent<Stats>().UpdateStats("GamesWon", 1);
      }
      else
      {
        winText.text = "LOSS";
        winText.color = loseColor;

        if (Coins.coins == 0)
        {
          Coins.coins = 5;
          scripts.GetComponent<Coins>().UpdateCoins();
        }

        newBalanceText.text = Coins.coins.ToString();
        scripts.GetComponent<Stats>().UpdateStats("GamesLost", 1);
      }
    }
}
