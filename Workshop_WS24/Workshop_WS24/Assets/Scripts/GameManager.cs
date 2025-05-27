using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int customersServedCorrectly = 0;
    public int customersMissedOrIncorrect = 0;
    public float gameDuration = 120f;
    private float timer;

    public GameObject gameOverUI;

    public TextMeshProUGUI servedText;
    public TextMeshProUGUI missedText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = gameDuration;
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EndGame();
        }
    }

    public void RegisterServed(bool correct)
    {
        if (correct)
            customersServedCorrectly++;
        else
            customersMissedOrIncorrect++;
    }

    void EndGame()
    {
        Time.timeScale = 0f; // Freeze time
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);

            if (servedText != null)
                servedText.text = $"Served: {customersServedCorrectly}";

            if (missedText != null)
                missedText.text = $"Missed: {customersMissedOrIncorrect}";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
