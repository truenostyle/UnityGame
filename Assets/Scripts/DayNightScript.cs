using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    [SerializeField]
    private Light sun;  // джерело денного світла
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;

    private float nightFactor = .25f;   // у скільки разів ніч темніша дня
    private float dayPeriod = 20f;      // тривалість доби у секундах
    private float dayPhase;             // фаза доби [0..1]

    void Start()
    {
        dayPhase = 0;
        RenderSettings.skybox = daySkybox;
    }

    void Update()
    {
        dayPhase += Time.deltaTime / dayPeriod;
        if (dayPhase > 1)
        {
            dayPhase -= 1f;
        }
        this.transform.eulerAngles = new Vector3(0, 0, 360 * dayPhase);
        bool isNight = dayPhase > 0.25f && dayPhase < 0.75f;
        if (isNight)
        {
            if (RenderSettings.skybox != nightSkybox)
            {
                RenderSettings.skybox = nightSkybox;
            }
            
        }
        else
        {
            if (RenderSettings.skybox != daySkybox)
            {
                RenderSettings.skybox = daySkybox;
            }
        }
        float k = LuxFactor(dayPhase);
        sun.intensity = RenderSettings.ambientIntensity = isNight ? k * nightFactor : k;
        RenderSettings.skybox.SetFloat("_Exposure", k);

    }


    // Крива зміни добової освітленості t[0..1] 
    // 0 - південь, 0.25 - сутінки, 0.5 - північ, 0.75 - світанок
    float LuxFactor(float t)
    {
        return (1f + Mathf.Cos(4f * Mathf.PI * t)) / 2f;
    }

    private void OnDestroy()
    {
        sun.intensity = 1f;
        RenderSettings.skybox.SetFloat("_Exposure", 1f);
        RenderSettings.ambientIntensity = 1f;
    }
}
