using System.Collections;
using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour
{

    public bool playerInRange;
    public NPCConversation Conversation;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
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
    