using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private static Transform shootCountUI;
    public static int remainingShootCount;

    private static Button restartButton;
    private static Button menuButton;

    private static Transform levelEnd;

    private void Awake() {
        Instance = this;

        shootCountUI = GameObject.Find("Shoots").transform;
        remainingShootCount = shootCountUI.childCount;

        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();

        restartButton.onClick.AddListener(() => {
            Restart();
        });

        menuButton.onClick.AddListener(() => {
            BackToMenu();
        });

        levelEnd = GameObject.Find("LevelEnd").transform;
        levelEnd.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            BackToMenu();
        }
    }

    public void NextLevel() {
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation() {
        levelEnd.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        if (PlayerPrefs.GetInt("LastLevel") != 10) {
            PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else {
            SceneManager.LoadScene("Menu");
        }
    }

    public static void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }

    public static void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void DecreaseShootCount() {
        remainingShootCount--;
        shootCountUI.GetChild(remainingShootCount).GetComponent<Animator>().Play("ShootUI");
    }
}
