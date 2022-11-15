using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    private List<GameObject> papersCount;
    private Animator animator_;
    private bool isStop = false; 

    


    void Start()
    {
        papersCount = GetComponent<paperCollectAndDrop>().papers;
        animator_ = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animation();
        
    }


    private void animation()
    {
        isStop = GetComponent<AI_NPC>().isStopping;

        if (!isStop && papersCount.Count <= 0)
        {

            animator_.SetBool("runWithOutPaper", true);
            animator_.SetBool("isCarrying", false);
            animator_.SetBool("runWithPaper", false);
        }
        else if (!isStop && papersCount.Count > 0)
        {
            animator_.SetBool("runWithOutPaper", false);
            animator_.SetBool("isCarrying", true);
            animator_.SetBool("runWithPaper", true);
        }
        else if (isStop && papersCount.Count <= 0)
        {
            animator_.SetBool("runWithOutPaper", false);
            animator_.SetBool("isCarrying", false);
            animator_.SetBool("runWithPaper", false);

        }
        else if (isStop && papersCount.Count > 0)
        {
            animator_.SetBool("runWithOutPaper", false);
            animator_.SetBool("isCarrying", true);
            animator_.SetBool("runWithPaper", false);
        }
    }
}
