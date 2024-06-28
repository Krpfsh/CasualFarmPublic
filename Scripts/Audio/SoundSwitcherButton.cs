using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundSwitcherButton : MonoBehaviour
{
    [SerializeField] private Toggle MusicButton;
    [SerializeField] private GameObject musicCheckMark;
    [SerializeField] private GameObject soundCheckMark;
    [SerializeField] private Toggle soundButton;
    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicIsOn", 1) == 1)
        {
            musicCheckMark.SetActive(true);
        }
        else
        {
            musicCheckMark.SetActive(false);
        }
        if (PlayerPrefs.GetInt("SoundIsOn", 1) == 1)
        {
            soundCheckMark.SetActive(true);
        }
        else
        {
            soundCheckMark.SetActive(false);
        }
    }
    public void OnButtonClickMusic()
    {
        if (PlayerPrefs.GetInt("MusicIsOn", 1) == 1)
        {
            PlayerPrefs.SetInt("MusicIsOn", 0);
            AudioManager.instance.Stop("Theme");
            musicCheckMark.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("MusicIsOn", 1);
            AudioManager.instance.Play("Theme");
            musicCheckMark.SetActive(true);
        }
    }
    public void OnButtonClickSound()
    {
        if (PlayerPrefs.GetInt("SoundIsOn", 1) == 1)
        {
            PlayerPrefs.SetInt("SoundIsOn", 0);
            AudioManager.instance.VolumeOff("Hit");
            soundCheckMark.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("SoundIsOn", 1);
            AudioManager.instance.VolumeOn("Hit");
            soundCheckMark.SetActive(true);
        }
    }
}
