using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseScript : MonoBehaviour
{
    // Start is called before the first frame update
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
                GameState.AddGameMessage(new() { Text = $"The horse not hungry, no need for apple" });

                // Check if the player has fish

            }

            else
            {
                if (GameState.HayCount > 0)
                {
                    // Feed the cat (reduce fish count by 1)
                    GameState.HayCount--;


                    // Add a success message
                    GameState.AddGameMessage(new() { Text = $"We fed the horse" });


                    // Update last feeding time
                    lastFeedingTime = Time.time;

                }
                else
                {
                    // Player has no fish, display the "hungry cat" message
                    GameState.AddGameMessage(new() { Text = $"The horse hungry, bring the apple" });
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
