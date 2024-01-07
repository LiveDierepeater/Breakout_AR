using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private BrickManager brickManager;
    private Player player;
    
    private Button startBtn;

    private Transform startButton;
    private Transform quitButton;
    private Transform leaderboard;

    private bool isStartButtonRestartButton;

    private void Awake()
    {
        brickManager = GameObject.Find("BrickManager").GetComponent<BrickManager>();
        player = GameObject.Find("Player").GetComponentInChildren<Player>();

        startButton = transform.Find("Start");
        quitButton = transform.Find("Quit");
        leaderboard = transform.Find("Leaderboard");

        AddStartButtonListener();           // Add Listener to Start-Button
    }

    private void Update()
    {
        if (!isStartButtonRestartButton) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchMenuOnOff();
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        player.gameObject.SetActive(true);
        brickManager.GenerateNextWave();

        ChangeStartButtonToRestartGame();

        SwitchMenuOnOff();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        player.gameObject.SetActive(false); // Disabling player while in Menu
    }

    private void AddStartButtonListener()
    {
        startBtn = startButton.GetComponentInChildren<Button>();
        startBtn.onClick.AddListener(StartGame);
    }

    private void ChangeStartButtonToRestartGame()   // Start Button Changes to Restart Button functionality.
    {
        startBtn = startButton.GetComponentInChildren<Button>();
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(RestartGame);

        startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Restart";
        isStartButtonRestartButton = true;                                        // Start Button is now Restart Button.
    }

    private void SwitchMenuOnOff()
    {
        startButton.gameObject.SetActive(!startButton.gameObject.activeSelf);
        quitButton.gameObject.SetActive(!quitButton.gameObject.activeSelf);
        leaderboard.gameObject.SetActive(!leaderboard.gameObject.activeSelf);
        
        Time.timeScale = startButton.gameObject.activeSelf ? 0 : 1;               // Timescale set to zero when in Menu.
    }
}
