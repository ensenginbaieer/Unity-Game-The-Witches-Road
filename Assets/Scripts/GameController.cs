using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    private List<FighterStats> fighterStats;

    private GameObject battleMenu;

    public TextMeshProUGUI battleText;
    public TextMeshProUGUI battleText1;
    public TextMeshProUGUI gameOverText;
    public Button returnToMenuButton;
    public AudioSource battleMusic;

    private bool isGameOver = false;
    private void Awake()
    {
        battleMenu = GameObject.Find("ActionMenu");
        gameOverText.gameObject.SetActive(false);
        returnToMenuButton.gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fighterStats = new List<FighterStats>();
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        FighterStats currentFighterStats = hero.GetComponent<FighterStats>();
        currentFighterStats.CalculateNextTurn(0);
        fighterStats.Add(currentFighterStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort();
        this.battleMenu.SetActive(false);

        battleMusic.Play();

        NextTurn();
    }

    public void NextTurn()
    {
        if (isGameOver)
        {
            return;
        }

        battleText.gameObject.SetActive(false);
        battleText1.gameObject.SetActive(false);

        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);

        if (!currentFighterStats.GetDead())
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn);
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort();

            if (currentUnit.tag == "Hero")
            {
                battleText1.text = "Your Turn";
                battleText1.gameObject.SetActive(true);
                this.battleMenu.SetActive(true);
            }
            else
            {
                string attackType = Random.Range(0, 2) == 1 ? "melee" : "range";
                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }
        }
        else
        {
            CheckGameOver();  
            if (!isGameOver)
            {
                NextTurn();
            }
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
    void CheckGameOver()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        bool heroDead = hero != null && hero.GetComponent<FighterStats>().GetDead();
        bool enemyDead = enemy != null && enemy.GetComponent<FighterStats>().GetDead();

        if (heroDead || enemyDead)
        {
            isGameOver = true;
            if (heroDead)
            {
                gameOverText.text = "You Lose!";
            }
            else if (enemyDead)
            {
                gameOverText.text = "You Win!";
            }

            gameOverText.gameObject.SetActive(true);
            returnToMenuButton.gameObject.SetActive(true);
            battleMenu.SetActive(false);

            if (isGameOver)
            {
                isGameOver = true;
                if (heroDead)
                {
                    gameOverText.text = "You Lose!";
                }
                else if (enemyDead)
                {
                    gameOverText.text = "You Win!";
                }

                gameOverText.gameObject.SetActive(true);
                returnToMenuButton.gameObject.SetActive(true);
                battleMenu.SetActive(false);
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartMenu"); 
    }
}
