using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_NPC : MonoBehaviour
{
    public static List<Transform> workDeskDestinations = new List<Transform>();
    public static List<Transform> printerDestinations = new List<Transform>();
    private NavMeshAgent NPC;
    //private int randomVarWorkDesk;
    //private int randomVarPrinter;
    private List<GameObject> papersCount;
    //private int maxPaperSize;
    //private bool targetTrue = false;

    

    public static List<workDesk> targetWorkDesksPapers =new List<workDesk>();
    public static List<paperPrint> targetPrinters = new List<paperPrint>();
    private int targetDesk;
    private int targetPrinter;
    public bool isStopping = true;

    

    void Start()
    {
        NPC = GetComponent<NavMeshAgent>();
        papersCount = GetComponent<paperCollectAndDrop>().papers;
        //maxPaperSize = GetComponent<paperCollectAndDrop>().maximumNumberOfCarryingPaper;
        
        //targetTrue = GetComponent<paperCollectAndDrop>().isWorkDeskTargetTrue;

        

        BuyAndImproveNPC.NPCWorkers.Add(gameObject);
        gameObject.GetComponent<AI_NPC>().enabled = false;
        

        
        
    }

    // Update is called once per frame
    void Update()
    {
        setDestination();
        NPC.speed = BuyAndImproveNPC.NPCSpeed;
    }

    private void setDestination()
    {
        if (papersCount.Count >= gameObject.GetComponent<paperCollectAndDrop>().maximumNumberOfCarryingPaper)
        {
            
            gameObject.GetComponent<paperCollectAndDrop>().isPrinterTargetTrue = false;
            
            
            NPC.SetDestination((workDeskDestinations[targetDesk].position));

            //transform.LookAt(workDeskDestinations[randomVarWorkDesk].position);
            var targetRotation = new Vector3(workDeskDestinations[targetDesk].transform.position.x, transform.position.y, workDeskDestinations[targetDesk].transform.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation, Vector3.up), 10f * Time.deltaTime);

            
            if (Vector3.Magnitude(gameObject.transform.position - workDeskDestinations[targetDesk].position) < 2.5f)
            {
                
                isStopping = true;
                
                gameObject.GetComponent<paperCollectAndDrop>().isWorkDeskTargetTrue = true;
                //randomVarPrinter = Random.Range(0, printerDestinations.Count);
                targetPrinter = targetPrinterCalculate();
                
                

            }
            else isStopping = false;


        }
        else if (papersCount.Count == 0)
        {
            

            gameObject.GetComponent<paperCollectAndDrop>().isWorkDeskTargetTrue = false;
            

            
            
            NPC.SetDestination((printerDestinations[targetPrinter].position));
            transform.LookAt(printerDestinations[targetPrinter].position);
            if (Vector3.Magnitude(gameObject.transform.position - printerDestinations[targetPrinter].position) < 2.5f)
            {
                
                
                isStopping = true;
                
                //randomVarWorkDesk = Random.Range(0, workDeskDestinations.Count);
                targetDesk = targetWorkDesk();
                gameObject.GetComponent<paperCollectAndDrop>().isPrinterTargetTrue = true;
            }
            else isStopping = false;
            
        }

        

    }


    private int targetWorkDesk()
    {
        int targetDesk=0;
        var minPaperCount = 0;
        for(int i = 0; i< targetWorkDesksPapers.Count; i++)
        {
            if(i == 0)
            {
                minPaperCount = targetWorkDesksPapers[i].workDeskPapers.Count;
                targetDesk = i;
            }
            if (minPaperCount > targetWorkDesksPapers[i].workDeskPapers.Count)
            {
                minPaperCount = targetWorkDesksPapers[i].workDeskPapers.Count;
                targetDesk = i;
            }
        }
        return targetDesk;
    }

    private int targetPrinterCalculate()
    {
        int targetPrinter_ = 0;
        int maxPaperCount = 0;
        int paperCount = 0;

        for (int i = 0; i < targetPrinters.Count; i++)
        {
            for(int j= 0; j < targetPrinters[i].papersOnTable.Count; j++) // Burada null olmayan eleman sayýsýna baklýyor
            {                                        // nedeni ise karakter yada npc'ler kaðýt topladýðýnda list'tedeki elemanlar 
                                                    // silinmek yerine null yapýlýp iþleme devam ediyor. Daha sonra null'lar
                                                    //tekrar dolduruluyor
                                                                  
                if(targetPrinters[i].papersOnTable[j] != null)
                {
                    paperCount++;
                }
            }
            if (i == 0)
            {
                maxPaperCount = paperCount;
                targetPrinter_ = i;
            }

            if (maxPaperCount < paperCount)
            {
                maxPaperCount = paperCount;
                targetPrinter_ = i;
            }
            paperCount = 0;
        }
        
        return targetPrinter_;

        
    }
}
