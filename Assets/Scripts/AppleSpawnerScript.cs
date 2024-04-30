using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject applePrefab; // ������ �� ������ ������
    public Terrain terrain; // ������ �� ��� �������
    public int numberOfApples = 10; // ���������� �����, ������� ����� �������

    void Start()
    {
        // ������� �������� ���������� �����
        for (int i = 0; i < numberOfApples; i++)
        {
            SpawnApple();
        }
    }

    void SpawnApple()
    {
        float minX = terrain.transform.position.x;
        float maxX = minX + terrain.terrainData.size.x;
        float minZ = terrain.transform.position.z;
        float maxZ = minZ + terrain.terrainData.size.z;

        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);

        // �������� ������ ����������� �������� � ��������� �����������
        float y = terrain.SampleHeight(new Vector3(x, 0f, z));

        // ������� ��������� ������ ������ ���� ��� ������ �� ������� ������ (������ 0.1f, ��������)
        if (y > 0.1f)
        {
            Vector3 spawnPosition = new Vector3(x, y, z);
            Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // ���� ������ ������� ������, ��������� ������� ������
            SpawnApple();
        }
    }
}
