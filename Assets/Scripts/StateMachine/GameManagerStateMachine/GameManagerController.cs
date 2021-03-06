using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManagerController : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject playerPrefab;
    public Transform spawnPoint;  
    private StatePlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start() {
        playerController = FindObjectOfType<StatePlayerController>();
    }

    //just teleports the player back to their original spawnpoint
    //make this function run a death animation on the player and have the player get teleported after the animation
    public void respawnPlayer() {
        playerController.gameObject.transform.position = spawnPoint.position;
    }

    public void endGame() {
            Destroy(playerController.gameObject);
            foreach (Rewired.Player player in Rewired.ReInput.players.Players) {
                player.controllers.ClearAllControllers();
            }
            SceneManager.LoadScene("Level1");
    }

}
