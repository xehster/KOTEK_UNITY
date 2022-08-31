using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public PlayerMovement playerMovement;
    public ItemCollector itemCollector;
    public PlayerLife playerLife;
    public Weapon weapon;
    public PlayerSounds PlayerSounds;

    private void Awake()
    {
        Instance = this;
    }

    public void TeleportPlayerTo(Vector2 position2D)
    {
        playerMovement.transform.position = position2D;
    }
}
