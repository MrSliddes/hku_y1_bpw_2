using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ManagerUI : MonoBehaviour
{
    /// <summary>
    /// Reference
    /// </summary>
    public static ManagerUI Instance { get; set; }
    /// <summary>
    /// The highscore of the player
    /// </summary>
    public int highScore = 0;
    /// <summary>
    /// The score of the player
    /// </summary>
    public static int score = 0;
    /// <summary>
    /// UI element of highscore
    /// </summary>
    public TextMeshProUGUI highScoreText;
    /// <summary>
    /// UI element that displays score
    /// </summary>
    public TextMeshProUGUI scoreText;
    /// <summary>
    /// UI element that displays the endscore
    /// </summary>
    public TextMeshProUGUI endScoreText;
    /// <summary>
    /// UI element that displays current lvl
    /// </summary>
    public TextMeshProUGUI lvl;
    /// <summary>
    /// Player hart ui 0
    /// </summary>
    public GameObject hp_0;
    /// <summary>
    /// Player hart ui 0
    /// </summary>
    public GameObject hp_1;
    /// <summary>
    /// Player hart ui 0
    /// </summary>
    public GameObject hp_2;
    /// <summary>
    /// The gameover ui element
    /// </summary>
    public GameObject gameOver;
    /// <summary>
    /// inventory ui element gameobject
    /// </summary>
    public GameObject inventory;
    /// <summary>
    /// The gem prefab that gets placed in the inventory when the player collects a gem
    /// </summary>
    public GameObject gemInventory;
    /// <summary>
    /// The container where the gems are parented to
    /// </summary>
    public Transform inventoryContainer;
    /// <summary>
    /// The reference to the player
    /// </summary>
    private Player player;
    /// <summary>
    /// The ui element that displays the quest text
    /// </summary>
    public TextMeshProUGUI questText;
    /// <summary>
    /// The current gems the player has
    /// </summary>
    public int currentGems = 0;
    /// <summary>
    /// The max amount of gems the player has to collect for bonus points
    /// </summary>
    public int maxGems = 3;
    /// <summary>
    /// The current kills the player has
    /// </summary>
    public int currentKills = 0;
    /// <summary>
    /// The max kills the player has to get for bonus points
    /// </summary>
    public int maxKills = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        Instance = this;
        player = FindObjectOfType<Player>(); // Yes I know, not the best way to get components but the game runs fast and isnt big.
        gameOver.SetActive(false);
        inventory.SetActive(false);
        // Get highscore value that was saved
        highScore = PlayerPrefs.GetInt("hs", 0);
        score = 0;
        currentGems = 0;
        currentKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Hp but its calculated in a lazy way
        hp_0.SetActive(player.currentHP > 0);
        hp_1.SetActive(player.currentHP > 1);
        hp_2.SetActive(player.currentHP > 2);

        // Score
        highScoreText.text = "High Score: " + highScore.ToString();
        scoreText.text = "Score: " + score.ToString();
        endScoreText.text = "Final Score: " + score.ToString();

        if(score > highScore) highScore = score;

        lvl.text = "LvL: " + DungeonGeneration.currentLvl.ToString();

        // If the Player dead
        if(player.currentHP <= 0)
        {
            if(gameOver.activeSelf == false && score > highScore)
            {
                Debug.Log("Save");
                PlayerPrefs.SetInt("hs", highScore);
            }
            gameOver.SetActive(true);
        }

        // Inventory
        if(Input.GetButtonDown("Input Tab"))
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        // Quest
        questText.text = "Quest 4 Points\nGet Gems: " + currentGems + "/" + maxGems + "\n" + "Kill Evil: " + currentKills + "/" + maxKills;
    }

    /// <summary>
    /// Resets the game and saves the values
    /// </summary>
    public void ResetGame()
    {
        Debug.Log("Reset");
        print(score + " " + highScore);        
        PlayerPrefs.SetInt("hs", highScore);
        
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Gets called when the player collects a gem
    /// </summary>
    public static void GotGem()
    {
        GameObject a = Instantiate(Instance.gemInventory, Instance.inventory.transform.position, Quaternion.identity);
        a.transform.SetParent(Instance.inventoryContainer);
        a.transform.localScale = Vector3.one;
        Instance.CheckQuestCompleted();
        Instance.currentGems++;
    }

    /// <summary>
    /// Gets called when the player kills a enemy
    /// </summary>
    public static void KilledEnemy()
    {
        Instance.currentKills++;
        Instance.CheckQuestCompleted();
    }

    /// <summary>
    /// Checks if the player completed the quest for bonus points
    /// </summary>
    public void CheckQuestCompleted()
    {
        if(currentGems >= maxGems) currentGems = maxGems;
        if(currentKills >= maxKills) currentKills = maxKills;

        if(currentGems >= maxGems && currentKills >= maxKills)
        {
            score += 500;
            currentGems = 0;
            currentKills = 0;
        }
    }
}
