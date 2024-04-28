using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject coin;

    private GameObject arrow;
    private GameObject content;

    void Start()
    {
        arrow = GameObject.Find("CompassContentArrow");
        content = GameObject.Find("CompassContent");
        GameState.Subscribe(OnGameStateChanged);
        OnGameStateChanged(nameof(GameState.isCompassVisible));
    }

    void Update()
    {
        Vector3 toCoin = coin.transform.position - character.transform.position;
        toCoin.y = 0;  // ������� - ���������� ������� �� ���� �������
                       // ������ �� ���� �������� ��� � ����������� ������
                       // character.transform.forward ����������� �������������� � CharacterScript
        arrow.transform.eulerAngles = new Vector3(0, 0,
           Vector3.SignedAngle(
            character.transform.forward,
            toCoin,
            Vector3.down));
    }
    private void OnGameStateChanged(string propName)
    {
        if (propName == nameof(GameState.isCompassVisible))
        {
            content.SetActive(GameState.isCompassVisible);
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChanged);
    }
}
