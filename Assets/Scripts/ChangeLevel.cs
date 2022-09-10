using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : Interactable

{
    [Scene]
    public string newLevel; // scene name
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
        Invoke("EndLevel", 1f);
    }

    private void EndLevel()
    {
        MemoryManager.Instance.SavePlayer();
        SceneManager.LoadScene(newLevel);
    }
}
