using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Time")]
    public float gameTime;
    public float maxGameTime = 5 * 10f;
    [Space]
    [Header("# User's Money")]
    public int vitamin = 0;
    public int mineral = 0;
    public int dietaryFiber = 0;
    [Space]
    [Header("# Game Object")]
    public PoolManager pool;
    public MainTower mainTower;
    [Space]
    [Header("# Player info")]
    public float health;
    public float maxHealth = 100f;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetMineral()
    {

    }
}
