using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject titleScreen;


    private float zEnemySpawn = 12.0f;
    private float xSpawnRange = 13.0f;
    private float zPowerupRange = 5.0f;
    private float ySpawn = 0.75f;
    private float startDelay = 1.0f;
    private float enemySpawnTime = 1.0f;
    private float powerupSpawnTime = 5.0f;
    

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;


    private int score;
    public bool isGameActive;
    
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        score = 0;
        UpdateScore(0);
        isGameActive = true;

        enemySpawnTime /= difficulty;
        powerupSpawnTime /= difficulty;

        InvokeRepeating("Enemy", startDelay, enemySpawnTime);
        InvokeRepeating("Powerup", startDelay, powerupSpawnTime);
    }
    
    void Enemy()
    {
        if(isGameActive)
        {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomIndex = Random.Range(0, enemies.Length);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

            Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
        }       
    }

    void Powerup()
    {
        if(isGameActive)
        {
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            float randomZ = Random.Range(-zPowerupRange, zPowerupRange);

            int randomIndex = Random.Range(0, powerups.Length);

            Vector3 spawnPos = new Vector3(randomX, ySpawn, randomZ);

            Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].gameObject.transform.rotation);
        }       
    }
    
}
