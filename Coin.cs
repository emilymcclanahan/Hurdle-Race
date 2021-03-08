using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
       if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
           Destroy(gameObject);
           PlayerController.instance.numCoins++;
           PlayerController.instance.audioSource.PlayOneShot(PlayerController.instance.collectCoinSound);
       }
    }
}
