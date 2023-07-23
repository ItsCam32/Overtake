using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI titleText, coinsValueText, newCoinsValueText, wagerValueText, firstButtonText, secondButtonText;
    public Slider wagerSlider;
    public GameObject wagerTexts, scripts;
    public TMP_InputField playerOneField;
    public Button noStakesButton, stakesButton, startButton;
    public Color unselectedColor, selectedColor, blackTextColor, whiteTextColor;
    bool gamemodeSelected = false;
    public static string playerOneName;
    public static int wagerAmount;

    public void Update()
    {
      if (Input.GetKeyDown(KeyCode.F3))
      {
        Coins.coins += 10000;
        scripts.GetComponent<Coins>().UpdateCoins();
      }

      coinsValueText.text = Coins.coins.ToString();
      wagerSlider.maxValue = Coins.coins;
      VerifyGamemodeSelection();
    }

    public void ChangeTitle(string newText)
    {
      titleText.text = newText;
    }

    public void ExitToDesktop()
    {
      Application.Quit();
    }

    public void UpdateCoinText()
    {
      newCoinsValueText.text = Coins.coins.ToString();
    }

    public void WagerSliderValueChanged()
    {
      wagerSlider.maxValue = Coins.coins;
      wagerValueText.text = wagerSlider.value.ToString();
      newCoinsValueText.text = (Coins.coins - wagerSlider.value).ToString();
    }

    public void NoStakesClicked()
    {
      noStakesButton.gameObject.GetComponent<Image>().color = selectedColor;
      stakesButton.gameObject.GetComponent<Image>().color = unselectedColor;
      firstButtonText.color = blackTextColor;
      secondButtonText.color = whiteTextColor;
      wagerSlider.interactable = false;
      wagerSlider.value = 0;
      newCoinsValueText.text = Coins.coins.ToString();
      gamemodeSelected = true;
    }

    public void StakesClicked()
    {
      noStakesButton.gameObject.GetComponent<Image>().color = unselectedColor;
      stakesButton.gameObject.GetComponent<Image>().color = selectedColor;
      secondButtonText.color = blackTextColor;
      firstButtonText.color = whiteTextColor;
      wagerSlider.interactable = true;
      gamemodeSelected = true;
    }

    public void VerifyGamemodeSelection()
    {
      // Check name is valid
      if (playerOneField.text.Length > 2 && gamemodeSelected == true)
      {
        if (wagerSlider.value > 0 || wagerSlider.interactable == false)
        {
          startButton.interactable = true;
        }
        else
        {
          startButton.interactable = false;
        }
      }
      else
      {
        startButton.interactable = false;
      }
    }

    public void StartButtonClicked()
    {
      playerOneName = playerOneField.text;
      wagerAmount = (int)wagerSlider.value;

      // Playing for coins
      if (wagerSlider.value > 0)
      {
        Coins.coins -= (int)wagerSlider.value;
        scripts.GetComponent<Coins>().UpdateCoins();
        wagerTexts.SetActive(true);
      }

      // Cleanup
      wagerSlider.value = 0;
      wagerSlider.interactable = false;
      wagerValueText.text = "0";
      newCoinsValueText.text = "0";
      noStakesButton.gameObject.GetComponent<Image>().color = unselectedColor;
      stakesButton.gameObject.GetComponent<Image>().color = unselectedColor;
      gamemodeSelected = false;
      startButton.interactable = false;
    }
}
