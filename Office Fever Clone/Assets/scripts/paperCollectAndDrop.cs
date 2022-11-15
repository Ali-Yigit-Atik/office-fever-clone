using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;


public class paperCollectAndDrop : MonoBehaviour
{
    public List<GameObject> papers = new List<GameObject>(); 
    public int maximumNumberOfCarryingPaper;
    private Transform papersCarriyingPosition;
    private int papersCount = 0;
    public bool isWorkDeskTargetTrue = false;
    public bool isPrinterTargetTrue = false;


    //private GameObject paperHoldingParentObject;


    void Start()
    {
        papersCarriyingPosition = transform.GetChild(2);
        if (gameObject.CompareTag("Player"))
        {
            isWorkDeskTargetTrue = true;
            isPrinterTargetTrue = true;
        }
        else
        {
            isWorkDeskTargetTrue = false;
            isPrinterTargetTrue = false;
        }

    }

    
    void Update()
    {
        paperCarriyingOrder();

        

    }

    private void paperCarriyingOrder()
    {

        if (papersCount != papers.Count) // gerek yok
        {
            

            for (int i = 0; i < papers.Count; i++)
            {
                if (i == 0)
                {
                    papers[i].transform.position = Vector3.Lerp(papers[i].transform.position, papersCarriyingPosition.position, Time.deltaTime * 10f);
                    if (papers[i].transform.parent != gameObject.transform.GetChild(3).transform)
                    {
                        papers[i].transform.parent = null;

                        papers[i].transform.parent = gameObject.transform.GetChild(3).transform;
                        papers[i].transform.localRotation = Quaternion.Euler(0, Random.Range(-5f, 5f), 0);
                    }

                }
                else if (i != 0)
                {
                    Vector3 target = new Vector3(0, 0, 0);
                    float Ysize = 0;
                    //Ysize = papers[i].GetComponent<Collider>().bounds.size.y;
                    Ysize = 0.05f;
                    target.x = Mathf.Lerp(papers[i].transform.position.x, papers[i - 1].transform.position.x, Time.deltaTime * 30);
                    target.y = Mathf.Lerp(papers[i].transform.position.y, papers[0].transform.position.y + Ysize*i, Time.deltaTime * 30);
                    target.z = papers[i - 1].transform.position.z;

                    papers[i].transform.position = target;


                    if (papers[i].transform.parent != gameObject.transform.GetChild(3).transform)
                    {
                        papers[i].transform.parent = null;

                        papers[i].transform.parent = gameObject.transform.GetChild(3).transform;
                        papers[i].transform.localRotation = Quaternion.Euler(0, Random.Range(-5f, 5f), 0);

                    }

                }
            }
            
        }
    }

    private void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.CompareTag("printTable") && papers.Count<= maximumNumberOfCarryingPaper  && isPrinterTargetTrue)
        {
            for (int i = collision.gameObject.GetComponent<paperPrint>().papersOnTable.Count-1; i >=0; i--)
            {
                if(collision.gameObject.GetComponent<paperPrint>().papersOnTable[i] == null)
                {
                    continue;
                }
                if(papers.Count >= maximumNumberOfCarryingPaper)
                {
                    break;
                }
                papers.Add(collision.gameObject.GetComponent<paperPrint>().papersOnTable[i]);
                //collision.gameObject.GetComponent<paperPrint>().papersOnTable.RemoveAt(i);
                collision.gameObject.GetComponent<paperPrint>().papersOnTable[i] =null;

                
            }
                

        }

        if(collision.gameObject.CompareTag("workDesk") && papers.Count > 0 && isWorkDeskTargetTrue)
        {
            Vector3 target = collision.gameObject.GetComponent<Collider>().bounds.center;
            
            if (collision.gameObject.GetComponent<workDesk>().maxWorkDeskPaperSize > collision.gameObject.GetComponent<workDesk>().workDeskPapers.Count)
            {
                
                for (int i = papers.Count - 1; i >= 0; i--)
                {
                    
                    if (collision.gameObject.GetComponent<workDesk>().maxWorkDeskPaperSize <= collision.gameObject.GetComponent<workDesk>().workDeskPapers.Count) break;
                    target.y = 0.5f + collision.gameObject.GetComponent<Collider>().bounds.size.y + collision.gameObject.GetComponent<workDesk>().workDeskPapers.Count * 0.05f;
                    papers[i].transform.parent = null;
                    papers[i].transform.DOJump(target, 1.5f, 1, 0.5f).SetEase(Ease.OutQuad);
                    collision.gameObject.GetComponent<workDesk>().workDeskPapers.Add(papers[i].gameObject);
                    papers[i].transform.localRotation = Quaternion.Euler(0, Random.Range(85f, 95f), 0);
                    papers.RemoveAt(i);

                }
            }
        }
    }

   

}
