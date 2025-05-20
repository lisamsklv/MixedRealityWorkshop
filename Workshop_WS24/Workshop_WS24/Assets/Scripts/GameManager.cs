using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int customersServedCorrectly = 0;
    public int customersMissedOrIncorrect = 0;
    public float gameDuration = 120f; // e.g. 2 minutes
    private float timer;

    public GameObject gameOverUI;
    public TMPro.TextMeshProUGUI resultText;

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
            resultText.text = $"Customers Served Correctly: {customersServedCorrectly}\n" +
                              $"Incorrect/Missed: {customersMissedOrIncorrect}";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
