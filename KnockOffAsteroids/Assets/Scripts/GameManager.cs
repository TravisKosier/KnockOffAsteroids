using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject rocket;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI FinalScoreText;
    public GameObject LoseText;
    public int RocketHealth = 10;
    private int _startLevelAsteroidsNum;
    private bool _allAsteroidsOffScreen;
    private int levelAsteroidNum;
    private Camera mainCam;
    private int asteroidLife;
    private int _score = 0;

    private void Start()
    {
        asteroid.SetActive(false);
        LoseText.SetActive(false);
        mainCam = Camera.main;
        _startLevelAsteroidsNum = 2;
        CreateAsteroids(_startLevelAsteroidsNum);
        ScoreText.text = "Score: " + _score;
        FinalScoreText.text = "";
    }

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.8f);

        if (asteroidLife <= 0)
        {
            asteroidLife = 6;
            CreateAsteroids(1);
        }

        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;
        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        _allAsteroidsOffScreen = true;

    }

    private void CreateAsteroids(int asteroidsNum)
    {
        for (int i = 1; i <= asteroidsNum; i++)
        {
            GameObject AsteroidClone = Instantiate(asteroid, new Vector2(Random.Range(-10, 10), 6f), transform.rotation);
            AsteroidClone.GetComponent<Asteroid>().SetGeneration(1);
            AsteroidClone.SetActive(true);
        }
    }

    public void RocketHit(int damage)
    {
        RocketHealth -= damage;
        if (RocketHealth <= 0)
            RocketDestroyed();
    }

    public void RocketDestroyed()
    {
        Cursor.visible = true;
        rocket.GetComponent<Rocket>().enabled = false;
        rocket.GetComponent<Rigidbody>().freezeRotation = false;
        LoseText.SetActive(true);
        ScoreText.text = "";
        FinalScoreText.text = "Final Score: " + _score;
    }

    public void AddScore(int score)
    {
        _score += score;
        ScoreText.text = "Score: " + _score;
    }

    public void asteroidDestroyed()
    {
        asteroidLife--;
    }

    public int startLevelAsteroidsNum
    {
        get 
        { 
            return _startLevelAsteroidsNum;
        }
    }

    public bool allAsteroidsOffScreen
    {
        get
        { 
            return _allAsteroidsOffScreen;
        }
    }
}
