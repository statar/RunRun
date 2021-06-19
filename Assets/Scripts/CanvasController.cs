using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    
    [SerializeField] private GameObject gameEndPanel;
    [SerializeField] private GameObject startPanel;
    
    [SerializeField] private TextMeshProUGUI gameStartTimerText;
    [SerializeField] private TextMeshProUGUI gameEndScoreText;
    [SerializeField] private TextMeshProUGUI startPanelBestScoreText;
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private Transform student;
    public int bestScore;
    private float _timerTime;
    private bool _isTimerStarted;

    private void Awake()
    {
        
        instance = this;
        _timerTime = 3f;
    }

    private void Start()
    {
        StartPanelSetActive(true);
        StartPanelBestScore();
        _isTimerStarted = false;
    }
    
    private void LateUpdate()
    {
        UpdateStartTimer();
        LevelScoreMetreText();
    }



    public void StartPanelToTimer()
    {
        StartPanelSetActive(false);
        GameStartTimerTextSetActive(true);
    }

    public void StartPanelSetActive(bool setActive)
    {
        startPanel.SetActive(setActive);
    }
    public void StartPanelBestScore()
    {
        startPanelBestScoreText.text = ("BEST SCORE  " + bestScore + " M");
    }

    public void GameEndPanelSetActive(bool setActive)
    {
        gameEndPanel.SetActive(setActive);
    }

    public void GameEndPanelScoreText()
    {
        gameEndScoreText.text = "Score = " + (int)student.position.z + " meters";
    }
    
    public void LevelScoreMetreTextSetActive(bool setActive)
    {
        levelScoreText.gameObject.SetActive(setActive);
    }
    
    public void LevelScoreMetreText()
    {
        if(levelScoreText.gameObject.activeInHierarchy)
            levelScoreText.text = (int) student.position.z + "M";
    }
    
    public void GameStartTimerTextSetActive(bool setActive)
    {
        gameStartTimerText.gameObject.SetActive(setActive);
        if (setActive)
            _isTimerStarted = true;
        else
            _timerTime = 3f;
    }
    
    public void GameStartTimerText(int time)
    {
        gameStartTimerText.text = time.ToString();
    }
    
    private void UpdateStartTimer()
    {
        if (_isTimerStarted && _timerTime > -1f)
        {
            _timerTime -= Time.deltaTime;
            GameStartTimerText((int)_timerTime);

            if (_timerTime < -0.1f)
            {
                GameStartTimerTextSetActive(false);
                StudentMovementController.startTimerFinish = true;
            }
        }
    }
    

    private void WriteBestScoreData()
    {
        if(bestScore < (int)student.position.z)
            bestScore = (int)student.position.z;
    }

    public void ResetLevelButton()
    {
        GameEndPanelSetActive(false);
        WriteBestScoreData();
        _isTimerStarted = false;
        GameManager.instance.ResetLevel();
        StartPanelSetActive(true);
    }
}
