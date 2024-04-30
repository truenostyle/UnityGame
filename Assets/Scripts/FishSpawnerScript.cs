using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnerScript : MonoBehaviour
{
    public GameObject fishPrefab; // —сылка на префаб €блока
    public Terrain terrain; // —сылка на ваш террейн
    public int numberOfFishes = 10; //  оличество €блок, которые нужно создать

    void Start()
    {
        // —павним заданное количество €блок
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

        // ѕолучаем высоту поверхности террейна в выбранных координатах
        float y = terrain.SampleHeight(new Vector3(x, 0f, z));

        // —оздаем экземпл€р €блока только если его высота не слишком низка€ (меньше 0.1f, например)
        if (y > 0.1f)
        {
            Vector3 spawnPosition = new Vector3(x, y, z);
            Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // ≈сли высота слишком низка€, повтор€ем попытку спавна
            SpawnFish();
        }
    }
}
