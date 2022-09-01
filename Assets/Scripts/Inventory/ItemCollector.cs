using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
   public int kittensouls = 0;
   [SerializeField] private Text kittenSoulsText;
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag(("KittenSoul")))
      {
         PlayerManager.Instance.PlayerSounds.PlayCollectSound();
         Destroy(collision.gameObject);
         kittensouls++;
         kittenSoulsText.text = "" + kittensouls;
      }
   }

   public void KittenSoulDecrease()
   {
      if (kittensouls > 0)
      {
         kittensouls -= 1;
         kittenSoulsText.text = "" + kittensouls;
      }
   }
}
