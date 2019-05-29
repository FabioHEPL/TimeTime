using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EnemySpawn enemySpawn;

    private void OnEnable()
    {
        enemySpawn.EnemyCollided += OnEnemyCollided;
    }

    private void OnDisable()
    {
        enemySpawn.EnemyCollided -= OnEnemyCollided;
    }

    private void OnEnemyCollided(GameObject enemy)
    {
        if (!bulletTimeMode)
        {
            Time.timeScale = bulletTimeScale;
            timeSinceLastScale = 0f;
            bulletTimeMode = true;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastScale = bulletTimeCooldown;
    }

    // Update is called once per frame
    void Update()
    {



        if (!paused)
        {
            timeSinceLastScale += Time.unscaledDeltaTime;
            if (timeSinceLastScale > bulletTimeCooldown)
            {
                Time.timeScale = baseTimeScale;
                bulletTimeMode = false;
            }
        }

        // Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    void Resume()
    {
        if (bulletTimeMode)
        {
            Time.timeScale = bulletTimeScale;
        }
        else
        {
            Time.timeScale = baseTimeScale;
        }
    
        paused = false;
    }



    [SerializeField]    
    private bool paused = false;

    public float baseTimeScale = 1f;

    [Header("Bullet Time")]
    [SerializeField]
    private bool bulletTimeMode = false;
    [SerializeField]
    private float bulletTimeScale = 0.5f;
    [SerializeField]
    public float bulletTimeCooldown = 2.5f;

    [Header("Debug")]
    [SerializeField]
    private float timeSinceLastScale = 0f;

}
