using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Time")]
    public float gameTime;
    public float maxGameTime = 12 * 60f;
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
