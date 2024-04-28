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
            Debug.Log("������� � ���!");
            GameState.AddGameMessage(new() { Text = $"Got the fish" }); // ���������
            GameState.FishCount++;
            Debug.Log($"Fish count updated: {GameState.FishCount}");
            Destroy(gameObject);
            // ���������� ������ ���
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("������ �� ����!");
 
            
            // �������� ����� ����� ������ ������, ��������� � ���������� �������������� � �����
        }
    }

    private void OnGameStateChange(string propName)
    {

    }
}
