using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour
{

    public bool playerInRange;
    public NPCConversation Conversation;
    private CinemachineBrain cinemachineBrain;
    private Transform playerTransform;
    [SerializeField] private string savedData;

    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            StartConverstion();
        }
    }

    private void StartConverstion()
    {
        ConversationManager.Instance.StartConversation(Conversation);
        ConversationManager.OnConversationEnded += OnConversationEnded;
        FollowToNPCCamera();
    }

    private void OnConversationEnded()
    {
        FollowPlayer();
    }

    private void FollowToNPCCamera()
    {
        cinemachineBrain.ActiveVirtualCamera.Follow = transform;
        cinemachineBrain.ActiveVirtualCamera.LookAt = transform;
        cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens
            .FieldOfView = 30f;
    }

    private void FollowPlayer()
    {
        cinemachineBrain.ActiveVirtualCamera.Follow = playerTransform;
        cinemachineBrain.ActiveVirtualCamera.LookAt = playerTransform;
        cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens
            .FieldOfView = 60f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            FollowPlayer();
            playerInRange = false;
            ConversationManager.Instance.EndConversation();
        }
    }
    
}
    