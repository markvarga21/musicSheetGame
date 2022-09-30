using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    BallRotator ballRotator;   
    // Start is called before the first frame update
    void Start()
    {
        this.ballRotator = GameObject.FindWithTag("Circle").GetComponent<BallRotator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                this.ballRotator.changeDiretion();
            }
        }  
    }
}
