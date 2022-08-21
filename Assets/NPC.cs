using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogueBar;
    public TMP_Text dialogueText;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerInRange;
    public Button continueButton;
    public TMP_Text continueText;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialogueBar.activeInHierarchy)
            {
                ResetText();
            }
            else
            {
                dialogueBar.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        ResetText();
    }

    public void ResetText()
    {
        continueText.text = "Continue...";
        dialogueText.text = "";
        index = 0;
        dialogueBar.SetActive(false);
        StopAllCoroutines();
    }

    private IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.gameObject.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ResetText();
        }
        
        if (index == dialogue.Length - 1)
        {
            continueText.text = "Close";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        else
        {
            ResetText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
    