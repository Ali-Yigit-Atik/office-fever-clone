using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BuyAndImproveNPC : MonoBehaviour
{
    public static List<GameObject> NPCWorkers =new List<GameObject>();
    



    private int SelledNPC = 0;
    public int CostOfNPC;
    public TextMeshProUGUI CostOfNPCText;

    
    public float maxNPCSpeed = 7;
    public static float NPCSpeed = 3f;
    public int CostOfNPCSpeed;
    public TextMeshProUGUI CostOfNPCSpeedText;


    public float maxNPCPaperCapacity;    
    public int CostOfNPCPaperCapacity;
    public TextMeshProUGUI CostOfNPCPaperCapacityText;

    public Canvas Canvas_;


    void Start()
    {
        

        Canvas_.gameObject.SetActive(false);

        CostOfNPCText.text = "$ " + CostOfNPC.ToString();
        CostOfNPCSpeedText.text = "$ " + CostOfNPCSpeed.ToString();
        CostOfNPCPaperCapacityText.text = "$ " + CostOfNPCPaperCapacity.ToString();
    }

    

    public void BuyNPC()
    {
        
        if (NPCWorkers.Count > SelledNPC)
        {
            if (PlayerPrefs.GetInt("money") >= CostOfNPC)
            {
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - CostOfNPC);
                NPCWorkers[SelledNPC].GetComponent<AI_NPC>().enabled = true;
                SelledNPC++;
                //NPCWorkers.RemoveAt(0);
                CostOfNPC = CostOfNPC * 2;
                CostOfNPCText.text = "$ " + CostOfNPC.ToString();

                if(NPCWorkers.Count <= SelledNPC) CostOfNPCText.text = "- MAX -";
            }
        }
        else CostOfNPCText.text = "- MAX -";



    }

    public void IncreaseSpeedOfNPC()
    {
        if (NPCSpeed < maxNPCSpeed)
        {
            if (PlayerPrefs.GetInt("money") >= CostOfNPCSpeed)
            {
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - CostOfNPCSpeed);
                NPCSpeed = NPCSpeed + 2;
                CostOfNPCSpeed = CostOfNPCSpeed * 2;
                CostOfNPCSpeedText.text = "$ " + CostOfNPCSpeed.ToString();

                if (NPCSpeed >= maxNPCSpeed) CostOfNPCSpeedText.text = "- MAX -";
            }
        }
        else CostOfNPCSpeedText.text = "- MAX -";
    }

    public void IncreasePaperHandlingCapacity()
    {
        var NPCCurrentHandlingCapacity = NPCWorkers[0].GetComponent<paperCollectAndDrop>().maximumNumberOfCarryingPaper;

        if (NPCCurrentHandlingCapacity < maxNPCPaperCapacity)
        {
            if (PlayerPrefs.GetInt("money") >= CostOfNPCPaperCapacity)
            {
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - CostOfNPCPaperCapacity);

                for(int i=0; i< NPCWorkers.Count; i++)
                {
                    NPCWorkers[i].GetComponent<paperCollectAndDrop>().maximumNumberOfCarryingPaper += 20;
                }

                CostOfNPCPaperCapacity = CostOfNPCPaperCapacity * 2;
                CostOfNPCPaperCapacityText.text = "$ " + CostOfNPCPaperCapacity.ToString();

                if ((NPCCurrentHandlingCapacity+20) >= maxNPCPaperCapacity) CostOfNPCPaperCapacityText.text = "- MAX -";
            }
        }
        else CostOfNPCPaperCapacityText.text = "- MAX -";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Canvas_.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Canvas_.gameObject.SetActive(false);
        }
    }
}
