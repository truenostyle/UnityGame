using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{

    private float lastFeedingTime = 0f;
    void Start()
    {

       
        GameState.Subscribe(OnGameStateChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastFeedingTime <= 5f)
            {
               
                // Add a message indicating the cat is still full
                GameState.AddGameMessage(new() { Text = $"The cat's not hungry, no need for fish" });
               
                // Check if the player has fish

            }

            else
            {
                if (GameState.FishCount > 0)
                {
                    // Feed the cat (reduce fish count by 1)
                    GameState.FishCount--;
                    

                    // Add a success message
                    GameState.AddGameMessage(new() { Text = $"We fed the cat" });


                    // Update last feeding time
                    lastFeedingTime = Time.time;

                }
                else
                {
                    // Player has no fish, display the "hungry cat" message
                    GameState.AddGameMessage(new() { Text = $"The cat's hungry, bring the fish" });
                }
            }

           
        }

        // Check if cat is hungry (after feeding check)
      
    }

 


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           

            // ƒобавьте здесь любую другую логику, св€занную с окончанием взаимодействи€ с котом
        }
    }

    private void OnGameStateChange(string propName)
    {
       
    }
}
