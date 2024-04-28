using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    // Start is called before the first frame update
   


    void Start()
    {
        GameState.Subscribe(OnGameStateChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Подошли к еде!");
            GameState.AddGameMessage(new() { Text = $"Got the fish" }); // Сообщение
            GameState.FishCount++;
            Debug.Log($"Fish count updated: {GameState.FishCount}");
            Destroy(gameObject);
            // Уничтожаем объект еды
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Отошли от кота!");
 
            
            // Добавьте здесь любую другую логику, связанную с окончанием взаимодействия с котом
        }
    }

    private void OnGameStateChange(string propName)
    {

    }
}
