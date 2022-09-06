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
    private CinemachineVirtualCamera virtualCamera;
    private Transform playerTransform;
    [SerializeField] private string savedData;
    private float defaultFov;

    private void Awake()
    {
        

    }

    private void Start()
    {

    }

    private void SetCamera()
    {
        if (Camera.main == null) return;
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        virtualCamera = cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject
            .GetComponent<CinemachineVirtualCamera>();
        defaultFov = virtualCamera.m_Lens
            .FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (cinemachineBrain == null || virtualCamera == null)
        {
            SetCamera();
        }
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
            .FieldOfView = 40f;
    }

    private void FollowPlayer()
    {
        cinemachineBrain.ActiveVirtualCamera.Follow = playerTransform;
        cinemachineBrain.ActiveVirtualCamera.LookAt = playerTransform;
        cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().m_Lens
            .FieldOfView = defaultFov;
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
    