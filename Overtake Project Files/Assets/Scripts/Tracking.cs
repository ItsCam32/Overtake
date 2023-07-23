using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tracking : MonoBehaviour
{
    public GameObject scripts;
    public TextMeshProUGUI winnerText;
    public Animator winnerAnim, winnerTextAnim;
    public TextMeshProUGUI playerOneName;
    //  index represents grid number, ie: [4] = grid number 5
    // GameObjects represent cards
    public static List<GameObject> gridOccupationList = new List<GameObject>();
    public static int playerOnesCards = 0;
    public static int playerTwosCards = 0;
    public static string playerTwoName;
    bool win = false;

    public void Start()
    {
      FillGridList();
    }

    public void FillGridList()
    {
      GameObject[] emptyRange = new GameObject[] { null, null, null, null, null, null, null, null, null };
      gridOccupationList.Clear();
      gridOccupationList.AddRange(emptyRange);
    }

    public void Update()
    {
      if (MatchUI.gameActive == true)
      {
        if (playerOnesCards + playerTwosCards == 9)
        {
          // Game finished
          MatchUI.gameActive = false;
          winnerAnim.Play("WinnerBackground");
          winnerTextAnim.Play("WinnerText");

          if (playerOnesCards > 4)
          {
            // Player 1 wins
            winnerText.text = playerOneName.text + " WINS!";
            win = true;
          }

          else if (playerTwosCards > 4)
          {
            // Player 2 wins
            winnerText.text = playerTwoName + " WINS!";
            win = false;
          }

          StartCoroutine(PauseTimer());
        }
      }
    }

    public IEnumerator PauseTimer()
    {
      yield return new WaitForSeconds(5.5f);
      ResetVariables();
    }

    public void ResetVariables()
    {
      scripts.GetComponent<ResetGame>().ResetCurrentGame(playerOneName.text, playerTwoName, win);

      FillGridList();
      win = false;
      winnerText.text = null;
      playerOneName.text = null;
      playerOnesCards = 0;
      playerTwosCards = 0;
      playerTwoName = null;
    }
}
