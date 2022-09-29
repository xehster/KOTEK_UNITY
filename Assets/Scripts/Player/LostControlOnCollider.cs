using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

public class LostControlOnCollider : MonoBehaviour
{
    private bool isInteractedBefore = false;
    public NPC npcScript;
    public void ForcedInteraction()
    {
        if (isInteractedBefore == false)
        {
            GetComponent<PlayerMovement>().enabled = false;
            isInteractedBefore = true;

        }

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("hint collider");
        if(collider.gameObject.CompareTag("Hint"))
        {
            ForcedInteraction();
        }
    }

    void Update()
    {

    }
}
