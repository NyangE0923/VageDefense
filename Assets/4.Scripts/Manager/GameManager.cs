using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Time")]
    public float gameTime;
    public float maxGameTime = 10 * 60f;
    [Space]
    [Header("# User's Money")]
    public int vitamin = 0;
    public int mineral = 0;
    public int dietaryFiber = 0;
    [Space]
    [Header("# Game Object")]
    public PoolManager pool;
    public SubTower subTower;
    [Space]
    [Header("# Player info")]
    public float health;
    public float maxHealth = 100f;
    [Header("# Tower Select Info")]
    public SelectTower towerSelect;
    public TowerTypeSelector typeSelect;
    public MouseSelect mouse;
    public Spawner spawner;

    public Result resultUI;

    public CreateTower.TowerStats[] towerStats;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        health = maxHealth;
        Resume();

        AudioManager.instance.PlayBgm(true, 1);
    }

    private void Update()
    {
        if (health < 0)
        {
            GameOver();
        }

        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void SetTowerStats(CreateTower.TowerStats[] stats)
    {
        towerStats = stats;
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    #region 게임 시간 관리
    public void Stop()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    #endregion

    IEnumerator GameOverRoutine()
    {
        
        yield return new WaitForSeconds(0.5f);
        resultUI.gameObject.SetActive(true);
        resultUI.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false, 1);
    }
    IEnumerator GameVictoryRoutine()
    {
        yield return new WaitForSeconds(.5f);
        resultUI.gameObject.SetActive(true);
        resultUI.Win();
        Stop();

        AudioManager.instance.PlayBgm(false, 1);

    }
}
