using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoider : MonoBehaviour
{
    // In order to not to fly behind an obstacle (for the bonus)
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            Destroy(collision.gameObject);
        }
    }
}
