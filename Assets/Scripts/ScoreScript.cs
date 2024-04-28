using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI scoreText;
    private TMPro.TextMeshProUGUI fishCount;
    private TMPro.TextMeshProUGUI hayCount;
    void Start()
    {
        scoreText = GameObject
            .Find("GameScoreText")
            .GetComponent<TMPro.TextMeshProUGUI>();

        fishCount = GameObject
                .Find("FishCountText")
                .GetComponent<TMPro.TextMeshProUGUI>();
        fishCount.text = string.Empty;
        GameState.Subscribe(OnFishCountChanged);

        hayCount = GameObject
                .Find("HayCountText")
                .GetComponent<TMPro.TextMeshProUGUI>();
        hayCount.text = string.Empty;
        GameState.Subscribe(OnHayCountChanged);
        GameState.Subscribe(OnGameStateChanged);
    }

    private void OnGameStateChanged(string propName)
    {
        if (propName == nameof(GameState.Score))
        {
            scoreText.text = GameState.Score.ToString("0");
        }
    }

    private void OnFishCountChanged(string propName)
    {
        if (propName == nameof(GameState.FishCount))
        {
            fishCount.text = GameState.FishCount.ToString("0");
        }
    }

    private void OnHayCountChanged(string propName)
    {
        if (propName == nameof(GameState.HayCount))
        {
            hayCount.text = GameState.HayCount.ToString("0");
        }
    }
    private void OnDestroy()
    {
        GameState.Unsubscribe(OnGameStateChanged);
    }

}
