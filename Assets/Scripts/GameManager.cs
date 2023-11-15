using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Anim fields
    [SerializeField] TheodoreAnimator anim;  
    [SerializeField] Animator statePanelAnim;
    //Sounds
    [SerializeField] AudioSource sound;
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gamePlayMusic;
    //UI
    [SerializeField] TMP_Text[] answerText = new TMP_Text[3];
    [SerializeField] TMP_Text[] topText = new TMP_Text[5];
    [SerializeField] TMP_Text task;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text infoText;
    [SerializeField] GameObject infoPanel;
    [SerializeField] TMP_InputField name;
    //Start GamePlay values
    [SerializeField] StartGamePlayValues startValues;


    //GamePlay variables
    private int correct = 0;
    private int correctButton = 0;
    private int level = 1;
    private int score = 0;
    private int scoreBonus = 5;
    private int timeBonus = 5;
    private int timeFine = 7;

    //Task string for user
    private string taskText;

    void Start()
    {
        GameTimer.TimerFinished += this.FinishGame;
        RefreshBest();
    }

    #region Game state methods

    //Method for finish actions
    public void FinishGame()
    {
        infoPanel.SetActive(true);
        infoText.text = "Конец игры!";
        ToMenu();
        resultText.text = "Ваш результат: " + score;
        for(int i = 0; i < topText.Length; i++)
        {
            if (PlayerPrefs.GetInt("Best" + i, 0) <= score)
            {
                for(int j = topText.Length; j > i; j--)
                {
                    PlayerPrefs.SetString("Name" + j, PlayerPrefs.GetString("Name" + (j - 1), ""));
                    PlayerPrefs.SetInt("Best" + j, PlayerPrefs.GetInt("Best" + (j - 1)));
                }
                PlayerPrefs.SetString("Name" + i, name.text == ""? "anonym": name.text);
                PlayerPrefs.SetInt("Best" + i, score);
                RefreshBest();
                break;
            }
        }
        
    }

    //Method change menu panel to gameplay panel
    public void ToGamePlay()
    {
        statePanelAnim.SetTrigger("GamePlay");
        RestartGamePlay();
    }

    //Method change gameplay panel to menu panel
    public void ToMenu()
    {
        statePanelAnim.SetTrigger("MainMenu");
        music.clip = menuMusic;
        music.Play();
    }

    //Method for gameplay pause
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    //Method for continue gameplay
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    //Method for quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Gameplay methods
    //Method that checks the user's answer
    public void CheckAnswer(int butName)
    {
        if (butName == correctButton)
            CorrectAction();

        else 
            MistakeAction();
    }

    //Action for correct user's answer
    void CorrectAction()
    {
        anim.Correct();
        level++;
        score += scoreBonus;
        infoText.text = "Верно! \n +" + timeBonus + " сек.";
        infoPanel.SetActive(true);
        GameTimer.timeRemaining += timeBonus;
        PlayerProgressAction();
        GenerateTask();
    }

    // Action for incorrect user's answer
    void MistakeAction()
    {
        anim.Mistake();
        infoText.text = "Неверно! \n -" + timeFine + " сек.";
        infoPanel.SetActive(true);
        GameTimer.timeRemaining -= timeFine;
        GenerateTask();
        
    }

    //Action for update gameplay rules, if answer was correct
    void PlayerProgressAction()
    {
        if(level%10 == 0)
        {
            scoreBonus += 2;
            timeBonus = timeBonus > 0 ? timeBonus - 1 : 0;
        }
    }

    //Method for call math generator
    void GenerateTask()
    {
        int mistake1, mistake2;
        new MathGenerator(level, out taskText, out correctButton, out correct, out mistake1, out mistake2);
        task.text = taskText;
        Debug.Log(taskText);

        UpdateUI(mistake1, mistake2);
    }

    //Method for refresh TOP 5 table
    void RefreshBest()
    {
        for(int i = 0; i < topText.Length; i++)
        {
            topText[i].text = PlayerPrefs.GetString("Name" + i, "") + "\t" + PlayerPrefs.GetInt("Best" + i, 0);
        }
    }

    //Method for updating the user interface
    void UpdateUI(int mistake1, int mistake2)
    {
        answerText[(correctButton + 1) % answerText.Length].text = mistake1 + "";
        answerText[(correctButton + 2) % answerText.Length].text = mistake2 + "";
        answerText[correctButton].text = correct + "";
        levelText.text = level + "";
        scoreText.text = score + "";
        
    }

    //Method for refresh start game values
    void RestartGamePlay()
    {
        music.clip = gamePlayMusic;
        music.Play();
        level = startValues.startLevel;
        score = startValues.startScore;
        scoreBonus = startValues.startScoreBonus;
        timeBonus = startValues.startTimeBonus;
        GenerateTask();
    }
    #endregion
}

//Class for start game values
[System.Serializable]
public class StartGamePlayValues
{
    public int startLevel;
    public int startScore;
    public int startScoreBonus;
    public int startTimeBonus;
    public int startTimeFine;
}
