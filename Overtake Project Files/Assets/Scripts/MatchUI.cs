using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchUI : MonoBehaviour
{
    public TextMeshProUGUI playerOneText, playerTwoText, playerTwoTitle, turnText, timerText, wagerText;
    public Image turnBackground;
    public Color playerOneColour, playerTwoColour, redColor, normalColor;
    public Animator P1Pulse, P2Pulse;
    public AudioSource tickingAudio, switchAudio;
    public GameObject scripts;
    public static bool gameActive = false;
    public static bool turn = true; // true = P1, false = P2
    public static int seconds;

    public void MatchStart()
    {
      playerOneText.text = MainMenu.playerOneName;
      MainMenu.playerOneName = null;

      Tracking.playerTwoName = NamesAndTitles.aiNames[Random.Range(0, NamesAndTitles.aiNames.Length)];
      playerTwoText.text = Tracking.playerTwoName;
      playerTwoTitle.text = NamesAndTitles.aiTitles[Random.Range(0, NamesAndTitles.aiTitles.Length)];
      scripts.GetComponent<PlayerTitle>().SetPlayerOneTitle();
      wagerText.text = MainMenu.wagerAmount.ToString();

      StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
      gameActive = true;
      while (gameActive == true)
      {
        if (turn == true)
        {
          // Player One's turn
          turnText.text = playerOneText.text + "'S TURN";
          turnBackground.color = playerOneColour;

          P2Pulse.speed = 0f;
          P2Pulse.Play("PlayerTwoPulse", 0, 0);
          P1Pulse.speed = 1f;
          P1Pulse.Play("PlayerOnePulse");
        }
        else
        {
          // Player Two's turn
          turnText.text = playerTwoText.text + "'S TURN";
          turnBackground.color = playerTwoColour;

          P1Pulse.speed = 0f;
          P1Pulse.Play("PlayerOnePulse", 0, 0);
          P2Pulse.speed = 1f;
          P2Pulse.Play("PlayerTwoPulse");

          scripts.GetComponent<CardLogic>().StartAIProcess();
        }

        for (seconds = 30; seconds > 0; seconds--)
        {
          timerText.text = seconds.ToString();

          if (seconds < 11)
          {
            timerText.color = redColor;
            tickingAudio.Play(0);
          }
          else
          {
            timerText.color = normalColor;
          }

          yield return new WaitForSeconds(1);
        }

        // Check if card in air
        if (CardDragging.cardInAir == true)
        {
          scripts.GetComponent<CardDragging>().DropCardTimeout();
        }

        switchAudio.Play(0);
        turn = !turn;
      }
    }

    public void ResetVariables()
    {
      gameActive = false;
      playerOneText.text = null;
      playerTwoText.text = null;
      playerTwoTitle.text = null;
      scripts.GetComponent<PlayerTitle>().ClearPlayerOneTitle();
      turnText.text = null;
      timerText.text = null;
      timerText.color = normalColor;
      wagerText.text = null;
      turnBackground.color = playerOneColour;
      turn = true;
      seconds = 0;
    }
}
