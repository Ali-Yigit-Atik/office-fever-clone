using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController_ : MonoBehaviour
{
    private Vector3 target;
    public int runningSpeed;
    private Animator animator_;
    private List<GameObject> papersCount;

    private void Start()
    {
        animator_ = GetComponent<Animator>();
        papersCount = GetComponent<paperCollectAndDrop>().papers;
    }

    void Update()
    {
        
        move();
        animation();
    }

    private void move()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitPosition, 100))
            {
                target = hitPosition.point;
                target.y = 0;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, Time.deltaTime * runningSpeed);

                if(Vector3.Magnitude(transform.position - target) >0.3f )
                transform.LookAt(target);

            }
        }
    }

    private void animation()
    {
        if (Input.GetMouseButton(0) && papersCount.Count <= 0)
        {
            
            animator_.SetBool("runWithOutPaper", true);
            animator_.SetBool("isCarrying", false);
            animator_.SetBool("runWithPaper", false);
        }
        else if(Input.GetMouseButton(0) && papersCount.Count > 0)
        {
            animator_.SetBool("runWithOutPaper", false);
            animator_.SetBool("isCarrying", true);
            animator_.SetBool("runWithPaper", true);
        }
        else if(!Input.GetMouseButton(0) && papersCount.Count <= 0)
        {
            animator_.SetBool("runWithOutPaper", false);
            animator_.SetBool("isCarrying", false);
            animator_.SetBool("runWithPaper", false);

        }
        else if(!Input.GetMouseButton(0) && papersCount.Count > 0)
        {
            animator_.SetBool("runWithOutPaper", false);
            animator_.SetBool("isCarrying", true);
            animator_.SetBool("runWithPaper", false);
        }
    }
}


