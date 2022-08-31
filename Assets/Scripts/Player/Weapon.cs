using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    public void Shoot()
    {
        PlayerManager.Instance.PlayerSounds.Shoot();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
