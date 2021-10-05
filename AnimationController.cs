using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;


    public float xAxis = 0.0f;
    public float yAxis = 0.0f;

    int xHash;
    int yHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        

        xHash = Animator.StringToHash("x");
        yHash = Animator.StringToHash("y");

    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool rightPressed = Input.GetKey(KeyCode.D);

        if (forwardPressed && xAxis < 1 )
        {
            xAxis += Time.deltaTime; 
        } else if (!forwardPressed && xAxis > 0 )
        {
            xAxis -= Time.deltaTime * 5 ;
        }
        if (backPressed && xAxis > - 1)
        {
            xAxis -= Time.deltaTime;
        }
        else if (!backPressed && xAxis < 0)
        {
            xAxis += Time.deltaTime * 5 ;
        }
        if (leftPressed && yAxis > -1)
        {
            yAxis -= Time.deltaTime;
        } else if (!leftPressed && yAxis < 0)
        {
            yAxis += Time.deltaTime *  5;

        }
        if (rightPressed && yAxis < 1)
        {
            yAxis += Time.deltaTime;
        } else if (!rightPressed &&  yAxis > 0)
        {
            yAxis -= Time.deltaTime * 5 ;
        }



        animator.SetFloat(xHash, xAxis);
        animator.SetFloat(yHash, yAxis);
    }
}
