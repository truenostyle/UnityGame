using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayScript : MonoBehaviour
{
    void Start()
    {
        GameState.Subscribe(OnGameStateChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            GameState.AddGameMessage(new() { Text = $"Got the hay" });
            GameState.HayCount++;
            Debug.Log($"Hay count updated: {GameState.HayCount}");
            Destroy(gameObject);
            // ”ничтожаем объект еды
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           


        }
    }

    private void OnGameStateChange(string propName)
    {

    }
}
