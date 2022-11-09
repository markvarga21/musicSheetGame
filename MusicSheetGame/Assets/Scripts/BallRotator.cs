using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotator : MonoBehaviour
{
    private float ballSpeed;

    void Start()
    {
        this.ballSpeed = 150.0F;
    }

    void FixedUpdate()
    {
        this.transform.Rotate(0, 0, this.ballSpeed * Time.deltaTime);
    }

    public void changeDiretion() {
        this.ballSpeed *= -1;
    }

    public void SetBallSpeed(float speed) {
        this.ballSpeed = speed;
    }
}
