using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMaker : MonoBehaviour {

    private float timer;
    public Transform trailPrefab;

    private void Update() {
        timer += Time.deltaTime;

        if (timer > 0.1f) {
            Transform trail = Instantiate(trailPrefab, transform.position, Quaternion.identity);
            trail.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
            timer = 0;
        }

        if (Mathf.Abs(transform.position.x) > 24 || Mathf.Abs(transform.position.y) > 24) {
            Destroy(gameObject);
        }
    }

}
