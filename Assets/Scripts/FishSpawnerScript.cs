using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerScript : MonoBehaviour
{
    public GameObject fishPrefab; // ������ �� ������ ������
    public Terrain terrain; // ������ �� ��� �������
    public int numberOfFishes = 10; // ���������� �����, ������� ����� �������

    void Start()
    {
        // ������� �������� ���������� �����
        for (int i = 0; i < numberOfFishes; i++)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
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
            Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // ���� ������ ������� ������, ��������� ������� ������
            SpawnFish();
        }
    }
}
