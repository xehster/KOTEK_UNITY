using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
   private const string startGameLevel = "Level1";
   [SerializeField] private Button newGame;
   [SerializeField] private Button resumeGame;

   private void Awake()
   {
      SubscribeButtons();
   }

   private void SubscribeButtons()
   {
      newGame.onClick.AddListener(StartNewGame);
      resumeGame.onClick.AddListener(ResumeGame);
   }

   private void ResumeGame()
   {
      SceneManager.LoadScene(startGameLevel);
   }

   private void StartNewGame()
   { 
      PlayerPrefs.DeleteAll();
      SceneManager.LoadScene(startGameLevel);
   }
}
