using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class SavePlayerName : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    public void SaveName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        startGameButton.interactable = true;
    }
}
