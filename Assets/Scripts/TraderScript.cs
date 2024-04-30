using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TraderScript : MonoBehaviour
{

    public GameObject traderCanvas;
    public Button FishButton;
    public Button HayButton;
    public TMPro.TextMeshProUGUI NeedMoney;

    private bool isCanvasActive = false;

    void Start()
    {
        traderCanvas.SetActive(false);
        GameState.Subscribe(OnGameStateChange);

        FishButton = traderCanvas.transform.Find("FishButton").GetComponent<Button>();
        HayButton = traderCanvas.transform.Find("HayButton").GetComponent<Button>();

        // Добавляем слушатели нажатия кнопок
        FishButton.onClick.AddListener(OnFishButtonClick);
        HayButton.onClick.AddListener(OnHayButtonClick);
    }

    private void OnFishButtonClick()
    {
        if (GameState.Score >= 2)
        {
            GameState.FishCount++;
            GameState.Score -= 2;
            Debug.Log(GameState.Score);
            NeedMoney.text = "Successfully purchased";
        }
        else
        {
            Debug.Log(GameState.Score);
            NeedMoney.text = "Not enough coins!";
        }
    }

    private void OnHayButtonClick()
    {
        if (GameState.Score >= 1)
        {
            GameState.HayCount++;
            GameState.Score -= 1;
            Debug.Log(GameState.Score);
            NeedMoney.text = "Successfully purchased";
        }
        else
        {
            NeedMoney.text = "Not enough coins!";
            Debug.Log(GameState.Score);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("podoshel");
           
        }
    }

    public void DeactivateCanvas()
    {
        traderCanvas.SetActive(false);
        Time.timeScale = false ? 0.0f : 1.0f;
        Cursor.lockState = false ? CursorLockMode.None : CursorLockMode.Locked;
        isCanvasActive = false;
    }

    public void ActivateCanvas()
    {
        traderCanvas.SetActive(true);
        Time.timeScale = true ? 0.0f : 1.0f;
        Cursor.lockState = true ? CursorLockMode.None : CursorLockMode.Locked;
        isCanvasActive = true;
    }

    public bool IsCanvasActive()
    {
        return isCanvasActive;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }

    private void OnGameStateChange(string propName)
    {

    }
}
