using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    //Private Lives Variable
    private int _lives = 10;

    //public variable for getting and setting lives
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            //all lives lost (zero counts as a life due to the check)
            if (value < 0)
            {
                GameOver();
                return;
            }

            //lost a life
            if (value < _lives)
            {
                Respawn();
            }

            //cannot roll over max lives
            if (value > maxLives)
            {
                value = maxLives;
            }

            _lives = value;

            Debug.Log($"Lives value on {gameObject.name} has changed to {lives}");
        }
    }

    //max lives that are possible
    [SerializeField] private int maxLives = 10;
    [SerializeField] private Playermovement playerPrefab;

    [HideInInspector] public Playermovement PlayerInstance => playerInstance;
    private Playermovement playerInstance;
    private Transform currentCheckpoint;

    private void Awake()
    {
        //if we are the first instance of the gamemanager object - ensure that our instance variable is filled and we cannot be destroyed when loading new levels.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return; // early exit out of the function
        }

        //if we are down here in execution - that means that the above if statement didn't run - which means we are a clone
        Destroy(gameObject);
    }

    void Respawn()
    {
        playerInstance.transform.position = currentCheckpoint.position;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "Level")
                SceneManager.LoadScene("Title");
            else
                SceneManager.LoadScene("Level");
        }

    }

    void GameOver()
    {
        Debug.Log("Game Over, change it to move to a specific scene called Game Over");
        SceneManager.LoadScene("GameOver");
    }



}
