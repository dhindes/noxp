using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool alarmSounded;
    public SceneAsset nextLevel;
    public string firstLevelName;
    private float secondsBeforeNextLevel;
    public float graceTimeAtEndOfLevel;
    public float secondsBeforeShowingDeathMenu;
    bool deathMenuShown;

    private void Awake()
    {
        References.levelManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(firstLevelName);
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        deathMenuShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If all enemies are dead
        if (References.allEnemies.Count < 1)
        {
            secondsBeforeNextLevel -= Time.deltaTime;
            // Stop alarm
            References.alarmManager.StopTheAlarm();

            if (secondsBeforeNextLevel <= 0)
            {
                // Go to the next level
                SceneManager.LoadScene(References.levelGenerator.nextLevelName);
            }
        }
        else
        {
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }

        if (References.thePlayer == null)
        {
            secondsBeforeShowingDeathMenu -= Time.deltaTime;
            if (!deathMenuShown && secondsBeforeShowingDeathMenu <= 0)
            {
                References.canvas.ShowMainMenu();
                deathMenuShown = true;
            }
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Startup");
        Time.timeScale = 1;
    }
}
