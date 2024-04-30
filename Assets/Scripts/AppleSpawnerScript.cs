using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject applePrefab; // —сылка на префаб €блока
    public Terrain terrain; // —сылка на ваш террейн
    public int numberOfApples = 10; //  оличество €блок, которые нужно создать

    void Start()
    {
        // —павним заданное количество €блок
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

        // ѕолучаем высоту поверхности террейна в выбранных координатах
        float y = terrain.SampleHeight(new Vector3(x, 0f, z));

        // —оздаем экземпл€р €блока только если его высота не слишком низка€ (меньше 0.1f, например)
        if (y > 0.1f)
        {
            Vector3 spawnPosition = new Vector3(x, y, z);
            Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            // ≈сли высота слишком низка€, повтор€ем попытку спавна
            SpawnApple();
        }
    }
}
