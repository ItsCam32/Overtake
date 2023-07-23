using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public static int coins;

    public void Start()
    {
      PlayerPrefs.DeleteKey("Coins");
      coins = PlayerPrefs.GetInt("Coins", 1000);
      coinsText.text = PlayerPrefs.GetInt("Coins", 1000).ToString();
    }

    public void UpdateCoins()
    {
      PlayerPrefs.SetInt("Coins", coins);
      coinsText.text = coins.ToString();
    }
}
