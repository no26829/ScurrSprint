using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Diagnostics.Contracts;

public class ScurrySprint : MonoBehaviour
{
    Camera cam;
    private float coinTimer;
    private float obstacleTimer;

    [Header("Inscribed")]
    public GameObject[] obstacles; // Obstacles array
    public GameObject obstacle;
    public GameObject[] coins; // coins array
    public GameObject coin;
    public Vector3 obstaclePos;
    public Vector3 coinPos;
    public float spawnChance = 0.01f;
    public float spawnDelayObstacle = 2f;
    public float spawnDelayCoin = 3f;

    public static GameObject SelectedAvatar; // Persisted avatar between scenes
    public Transform SpawnPoint; // Where the avatar spawns in the game
    public CinemachineVirtualCamera virtualCamera; // Reference to the Virtual Camera

    private void Awake()
    {
        cam = Camera.main;

        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameObject spawnedAvatar = null;

            // Check if an avatar has been selected
            if (SelectedAvatar != null && SpawnPoint != null)
            {
                Debug.Log("Spawning selected avatar: " + SelectedAvatar.name);
                spawnedAvatar = Instantiate(SelectedAvatar, SpawnPoint.position, SpawnPoint.rotation);
            }
            else if (SelectedAvatar == null)
            {
                Debug.LogWarning("No avatar selected. Defaulting to Chicken_001.");
                GameObject chicken = GameObject.Find("Chicken_001");
                if (chicken != null)
                {
                    spawnedAvatar = Instantiate(chicken, SpawnPoint.position, SpawnPoint.rotation);
                }
                else
                {
                    Debug.LogError("Chicken_001 not found in the scene!");
                }
            }
            else
            {
                Debug.LogWarning("SpawnPoint is not assigned!");
            }

            // Assign spawned avatar to the Virtual Camera's Follow property
            if (spawnedAvatar != null && virtualCamera != null)
            {
                virtualCamera.Follow = spawnedAvatar.transform;
                Debug.Log("Virtual Camera is now following: " + spawnedAvatar.name);
            }
            else if (virtualCamera == null)
            {
                Debug.LogWarning("Virtual Camera reference is not assigned in the Inspector.");
            }
        }
    }

    private void Update()
    {
        // Timer logic only runs in the Main Scene
        if (SceneManager.GetActiveScene().name == "Main")
        {
            coinTimer += Time.deltaTime;
            obstacleTimer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // Obstacle spawning logic only runs in the Main Scene
        if (SceneManager.GetActiveScene().name != "Main") return;

        if (obstacles == null || obstacles.Length == 0)
        {
            Debug.LogWarning("Obstacles array is empty or not assigned!");
            return;
        }

        float camX = cam.transform.position.x;
        float randomFloat = Random.Range(0.00f, 1.00f);
        int obstacleType = Random.Range(0, obstacles.Length);
        int coinType = Random.Range(0, coins.Length);

        // Spawn obstacles
        if (randomFloat < spawnChance && obstacleTimer >= spawnDelayObstacle)
        {
            obstacle = Instantiate(obstacles[obstacleType]);
            obstacle.transform.position = obstaclePos;
            Vector3 currentPos = obstaclePos;
            currentPos.x = camX + 25f; // Spawn obstacle slightly ahead of the camera
            obstacle.transform.position = currentPos;
            obstacleTimer = 0; // Reset the timer
        }

        // Spawn coins
        if (randomFloat < spawnChance && coinTimer >= spawnDelayCoin)
        {
            coin = Instantiate(coins[coinType]);
            coin.transform.position = coinPos;
            Vector3 currentPos = obstaclePos;
            currentPos.x = camX + 25f; // Spawn coin(s) slightly ahead of the camera
            currentPos.y += 5f;
            coin.transform.position = currentPos;
            coinTimer = 0; // Reset the timer
        }
    }

    public void LoadChoiceSelection()
    {
        SceneManager.LoadScene("ChoiceSelection");
    }

    public void LoadHighScores()
    {
        SceneManager.LoadScene("HighScores");
    }

    public void OnQuitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnLeaderboardButton()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void OnAvatarButton()
    {
        SceneManager.LoadScene("AvatarSelection");
    }

    public void SelectAvatar(GameObject avatar)
    {
        SelectedAvatar = avatar;
        Debug.Log("Avatar selected: " + avatar.name);
        SceneManager.LoadScene("OpeningScene"); 
    }
}