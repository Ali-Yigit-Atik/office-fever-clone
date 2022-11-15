using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class workDesk : MonoBehaviour
{
    public int maxWorkDeskPaperSize;
    public float processTime;
    public GameObject money;
    private bool onProcess =false;
    public List<GameObject> workDeskPapers = new List<GameObject>();
    public List<Transform> moneyPlaces = new List<Transform>();
    private int moneyPlaceIndex=0;
    private float Yaxis;
    private Animator deskWorker;
    private List<GameObject> moneys = new List<GameObject>();
    private Collider moneyCollectArea;
    private Transform player;
    

    void Start()
    {
        AI_NPC.workDeskDestinations.Add(gameObject.transform);
        AI_NPC.targetWorkDesksPapers.Add(GetComponent<workDesk>());
        
        moneyCollectArea = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        for (int i=0; i < gameObject.transform.GetChild(0).transform.childCount; i++)
        {
            moneyPlaces.Add(gameObject.transform.GetChild(0).transform.GetChild(i));
        }

        for(int i=0;i< gameObject.transform.childCount;i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.CompareTag("deskWorker"))
            {
                deskWorker = gameObject.transform.GetChild(i).gameObject.GetComponent<Animator>();
            }
        }
    }

    
    void Update()
    {
        

        StartCoroutine(paperWorkAndMoney(processTime));
        deskWorkerAnimation();
        moneyCollect();

    }

    IEnumerator paperWorkAndMoney(float time_)
    {
        if(onProcess==false && workDeskPapers.Count > 0)
        {

            
            onProcess = true;
            workDeskPapers[workDeskPapers.Count - 1].gameObject.SetActive(false);
            workDeskPapers.RemoveAt(workDeskPapers.Count - 1);

            if (moneys.Count <= 0)
            {
                moneyPlaceIndex = 0;
                Yaxis = 0;
            }

            Vector3 target = moneyPlaces[moneyPlaceIndex].transform.position;
            target.y = moneyPlaces[moneyPlaceIndex].transform.position.y + Yaxis;

            GameObject instMoney = Instantiate(money, target, Quaternion.Euler(-90,90,0));
            moneys.Add(instMoney);
            if(moneyPlaceIndex < (moneyPlaces.Count -1))
            {
                moneyPlaceIndex++;
            }
            else
            {
                moneyPlaceIndex = 0;
                Yaxis += 0.05f;
            }
            yield return new WaitForSeconds(time_);
            onProcess = false;
        }
        
    }

    private void moneyCollect()
    {
        if(Vector3.Magnitude(moneyCollectArea.bounds.center - player.position) < 2f )
        {
            for(int i=moneys.Count-1; i>=0; i--)
            {
                moneys[i].GetComponent<MoneyCollect>().isMoneyCollected = true;
                moneys.RemoveAt(i);
            }
        }
    }

    private void deskWorkerAnimation()
    {
        if (workDeskPapers.Count > 0)
        {
            deskWorker.SetBool("isWorking", true);
        }
        else
        {
            deskWorker.SetBool("isWorking", false);
        }
    }
}
