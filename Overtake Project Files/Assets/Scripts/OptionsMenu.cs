using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public GameObject videoTitleButton, audioTitleButton, accessibilityTitleButton;
    public TextMeshProUGUI fpsCounterText, volumeText;
    public Color defaultColour, selectedColour, disabledColour;
    public TMP_Dropdown windowDropdown, resolutionDropdown, refreshRateDropdown, vsyncDropdown, aaDropdown, colourblindDropdown;
    public Slider volumeSlider;
    public AudioSource[] audioSources;
    List<string> resolutionsList = new List<string>();
    List<string> refreshRatesList = new List<string>();

    public void Start()
    {
      Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen, Screen.currentResolution.refreshRate);
      StartCoroutine(FPSCounterTimer());
      GetResolutions();
      GetRefreshRates();
    }

    public void Update()
    {
      if (Input.GetKeyDown(KeyCode.F6))
      {
        fpsCounterText.gameObject.SetActive(!fpsCounterText.gameObject.activeSelf);
      }
    }

    public IEnumerator FPSCounterTimer()
    {
      while (true)
      {
        if (fpsCounterText.gameObject.activeSelf == true)
        {
          fpsCounterText.text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
        }

        yield return new WaitForSeconds(0.1f);
      }
    }

    public void TitleClicked(GameObject clickedButton)
    {
      videoTitleButton.GetComponent<Image>().color = defaultColour;
      audioTitleButton.GetComponent<Image>().color = defaultColour;
      accessibilityTitleButton.GetComponent<Image>().color = defaultColour;
      clickedButton.GetComponent<Image>().color = selectedColour;
    }

    public void GetResolutions()
    {
      Resolution[] resolutions = Screen.resolutions;

      foreach (var res in resolutions)
      {
        string resToCheck = res.width + "x" + res.height;

        if (!resolutionsList.Contains(resToCheck))
        {
          resolutionsList.Add(res.width + "x" + res.height);
        }
      }

      resolutionsList.Reverse();
      resolutionDropdown.AddOptions(resolutionsList);
    }

    public void GetRefreshRates()
    {
      int refreshRate = Screen.currentResolution.refreshRate;

      if (refreshRate == 60)
      {
        refreshRatesList.Add("60hz");
      }

      else if (refreshRate == 75)
      {
        refreshRatesList.Add("75hz");
        refreshRatesList.Add("60hz");
      }

      else if (refreshRate == 144)
      {
        refreshRatesList.Add("144hz");
        refreshRatesList.Add("75hz");
        refreshRatesList.Add("60hz");
      }

      else if (refreshRate == 240)
      {
        refreshRatesList.Add("240hz");
        refreshRatesList.Add("144hz");
        refreshRatesList.Add("75hz");
        refreshRatesList.Add("60hz");
      }

      else if (refreshRate > 245)
      {
        refreshRatesList.Add("360hz");
        refreshRatesList.Add("240hz");
        refreshRatesList.Add("144hz");
        refreshRatesList.Add("75hz");
        refreshRatesList.Add("60hz");
      }

      refreshRateDropdown.AddOptions(refreshRatesList);
    }

    public void WindowModeChanged()
    {
      if (windowDropdown.value == 0)
      {
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen, int.Parse(refreshRateDropdown.options[refreshRateDropdown.value].text.Split('h')[0]));
        refreshRateDropdown.enabled = true;
        refreshRateDropdown.GetComponent<Image>().color = selectedColour;
      }

      else if (windowDropdown.value == 1)
      {
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.Windowed);
        refreshRateDropdown.enabled = false;
        refreshRateDropdown.GetComponent<Image>().color = disabledColour;
      }
    }

    public void ResolutionChanged()
    {
      int width = int.Parse(resolutionDropdown.options[resolutionDropdown.value].text.Split('x')[0]);
      int height = int.Parse(resolutionDropdown.options[resolutionDropdown.value].text.Split('x')[1]);
      Screen.SetResolution(width, height, Screen.fullScreen, int.Parse(refreshRateDropdown.value.ToString().Split('h')[0]));
    }

    public void RefreshRateChanged()
    {
      Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.ExclusiveFullScreen, int.Parse(refreshRateDropdown.options[refreshRateDropdown.value].text.Split('h')[0]));
    }

    public void VsyncChanged()
    {
      if (vsyncDropdown.value == 0)
      {
        QualitySettings.vSyncCount = 1;
      }

      else if (vsyncDropdown.value == 1)
      {
        QualitySettings.vSyncCount = 0;
      }
    }

    public void AAChanged()
    {
      if (aaDropdown.value == 0)
      {
        QualitySettings.antiAliasing = 2;
      }

      else if (aaDropdown.value == 1)
      {
        QualitySettings.antiAliasing = 4;
      }

      else if (aaDropdown.value == 2)
      {
        QualitySettings.antiAliasing = 8;
      }

      else if (aaDropdown.value == 3)
      {
        QualitySettings.antiAliasing = 16;
      }
    }

    public void VolumeSliderChanged()
    {
      volumeText.text = volumeSlider.value.ToString();

      foreach (AudioSource audioSrc in audioSources)
      {
        audioSrc.volume = volumeSlider.value / 100;
      }
    }

    public void ColourblindModeChanged()
    {
      GameObject.Find("CVDFilter").GetComponent<CVDFilter>().ChangeMode(colourblindDropdown.value);
    }
}
