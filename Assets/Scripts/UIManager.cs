using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    public TextMeshProUGUI storyText;
    public TextMeshProUGUI response1;

    public Button button1;
    public TextMeshProUGUI response2;
    public Button button2;
    public GameObject panel;

    public TextMeshProUGUI yourResolve;
    public TextMeshProUGUI enemyResolve;

    private PlayerManager player;
    private BossManager boss;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerManager>();
        boss = FindObjectOfType<BossManager>();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        yourResolve.text = "Your Resolve: " + player.GetStateInput().playerController.health;
        if (enemyResolve.gameObject.activeInHierarchy) {
            enemyResolve.text = "Enemy Resolve: " + boss.GetStateInput().bossController.resolve;
        }
    }

    public void clickedResponse(int responseNum) {
        ((GameManagerChoiceState)gameManager.GetState()).choiceChosen = responseNum;
    }

    public void showBeginningLine() {
        panel.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        storyText.text = "I'm not naive! I know I can save this victim... without hurting anyone. But first I have to get through these clones.";
    }

    public void showChoice1() {
        panel.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        storyText.text = "Enemy: No one cares about me, so I'll do whatever I want... Starting with getting rid of you!";
        response1.text = "That's not true! People do care about you!";
        response2.text = "I believe your pain is real, and we can work on it together!";
    }

    public void showEnding() {
        panel.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        storyText.text = "Ally: I don't know how anyone will forgive me after everything I've done... but if you're willing help me figure out how to move on, I think I can manage.";
    }

    public void hideAll() {
        panel.SetActive(false);
    }

    public void turnOnEnemyResolve()
    {
        enemyResolve.gameObject.SetActive(true);
    }
}
