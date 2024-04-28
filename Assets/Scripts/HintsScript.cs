
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    private GameObject content;
    private GameObject leftArrow;
    private GameObject rightArrow;

    void Start()
    {
        content = GameObject.Find("HintsContent");
        leftArrow = GameObject.Find("HintsContentLeftArrow");
        rightArrow = GameObject.Find("HintsContentRightArrow");
        GameState.Subscribe(OnGameStateChanged);
        OnGameStateChanged(nameof(GameState.isHintsVisible));
    }

    void Update()
    {
        Vector3 point = Camera.main.
            WorldToViewportPoint(coin.transform.position);

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        if (point.z >= 0)
        {
            /* ��� point.z >= 0 ��������� WorldToViewportPoint �������� ���������, �� ��� <0 ��� ������ ����������
             */
            if (point.x < 0)
            {
                leftArrow.SetActive(true);
            }
            else if (point.x > 1)
            {
                rightArrow.SetActive(true);
            }
        }
        else
        {
            float angle = Vector3.SignedAngle(
                Camera.main.transform.forward,
                coin.transform.position -
                Camera.main.transform.position,
                Vector3.down);
            if (angle > 0)
            {
                leftArrow.SetActive(true);
            }
            else
            {
                rightArrow.SetActive(true);
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // �������� ������� ��������� �� ������� ������
            //Debug.Log(Camera.main.WorldToScreenPoint(coin.transform.position));

            // ������������� ������� - �� 0 �� 1 �� ����� ������
            //Debug.Log(Camera.main.WorldToViewportPoint(coin.transform.position));

            Debug.Log(Vector3.SignedAngle(
                Camera.main.transform.forward,
                coin.transform.position -
                Camera.main.transform.position,
                Vector3.down));
        }
    }
    private void OnGameStateChanged(string propName)
    {
        if (propName == nameof(GameState.isHintsVisible))
        {
            content.SetActive(GameState.isHintsVisible);
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChanged);
    }
}
