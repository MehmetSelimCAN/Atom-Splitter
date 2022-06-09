using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCircle : MonoBehaviour {

    private Transform gridSystem;
    private int closestPoint;
    private float minimumDistance;
    private Vector3 newPosition;
    private LayerMask gridLayer;

    private void Awake() {
        gridSystem = FindObjectOfType<GridSystem>().transform;
        gridLayer = LayerMask.GetMask("Grid");
    }

    private void OnMouseDown() {
        minimumDistance = 100;
        Collider2D gridPoint = Physics2D.OverlapCircle(transform.position, 0.1f, gridLayer);
        gridPoint.GetComponent<GridPoint>().taken = false;
    }

    private void OnMouseDrag() {
        minimumDistance = 100;
        for (int i = 0; i < gridSystem.childCount; i++) {
            if (!gridSystem.GetChild(i).GetComponent<GridPoint>().taken) {
                if (Vector2.Distance(gridSystem.GetChild(i).position, GetMousePosition()) < minimumDistance && Vector2.Distance(gridSystem.GetChild(i).position, GetMousePosition()) < 2.5f) {
                    minimumDistance = Vector2.Distance(gridSystem.GetChild(i).position, GetMousePosition());
                    closestPoint = i;
                }
            }
    }
        newPosition = gridSystem.GetChild(closestPoint).transform.position;
        transform.position = new Vector3(newPosition.x, newPosition.y, 0f);
    }

    private void OnMouseUp() {
        gridSystem.GetComponent<GridSystem>().RefreshGrid();
    }

    private Vector3 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
