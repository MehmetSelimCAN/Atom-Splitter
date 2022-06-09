using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

    private LayerMask circleLayer;
    private bool canGoNextLevel;

    private void Awake() {
        circleLayer = LayerMask.GetMask("Circle");
        RefreshGrid();
    }

    public void RefreshGrid() {
        canGoNextLevel = true;
        for (int i = 0; i < transform.childCount; i++) {
            Collider2D collider = Physics2D.OverlapCircle(transform.GetChild(i).position, 0.1f, circleLayer);

            if (collider != null) {
                transform.GetChild(i).GetComponent<GridPoint>().taken = true;
                canGoNextLevel = false;
            }
            else {
                transform.GetChild(i).GetComponent<GridPoint>().taken = false;
            }
        }

        if (canGoNextLevel) {
            GameManager.Instance.NextLevel();
        }
    }
}
