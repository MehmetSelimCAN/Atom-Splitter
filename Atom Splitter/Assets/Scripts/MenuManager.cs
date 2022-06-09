using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    private Transform levels;
    private Button tempButton;

    private void Awake() {
        if (!PlayerPrefs.HasKey("LastLevel")) {
            PlayerPrefs.SetInt("LastLevel", 1);
        }

        levels = GameObject.Find("Levels").transform;
        for (int i = 0; i < PlayerPrefs.GetInt("LastLevel"); i++) {
            levels.GetChild(i).Find("circle").gameObject.SetActive(false);
            levels.GetChild(i).GetComponent<Button>().enabled = true;

            var i2 = i;
            tempButton = levels.GetChild(i2).GetComponent<Button>();
            tempButton.onClick.AddListener(() => {
                SceneManager.LoadScene(levels.GetChild(i2).transform.name);
            });
        }
    }

}
