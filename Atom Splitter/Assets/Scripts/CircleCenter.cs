using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCenter : MonoBehaviour {

    private Transform parent;
    private int ballCount;
    private Vector3 shootDirection;
    private float ballSpeed = 12f;

    private Transform gridSystem;

    private void Awake() {
        parent = transform.parent;
        gridSystem = FindObjectOfType<GridSystem>().transform;
    }

    private void OnMouseDown() {
        if (GameManager.remainingShootCount > 0) {
            ShootBalls();
            GameManager.DecreaseShootCount();
        }
    }

    private void ShootBalls() {
        ballCount = parent.childCount - 1;
        for (int i = 0; i < ballCount; i++) {
            shootDirection = parent.GetChild(i).position - transform.position;
            parent.GetChild(i).GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * ballSpeed;
            parent.GetChild(i).GetComponent<TrailMaker>().enabled = true;
        }

        for (int i = 0; i< ballCount; i++) {
            parent.GetChild(0).parent = null;
        }

        Destroy(parent.gameObject);
        gridSystem.GetComponent<GridSystem>().RefreshGrid();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            ShootBalls();
            Destroy(collision.gameObject);
        }
    }
}
