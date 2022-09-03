using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlleyCatScene : Interactable

{
    private AudioSource finishSound;
    private bool levelCompleted = false;
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        finishSound.Play();
        levelCompleted = true;
        Invoke("EndLevel", 0f);
    }

    private void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MemoryManager.Instance.SavePlayer();
    }
}

