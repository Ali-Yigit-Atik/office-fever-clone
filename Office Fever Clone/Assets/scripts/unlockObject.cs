using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class unlockObject : MonoBehaviour
{
    public Image unlockBar;    
    public int priceOfObject;
    private int moneyPaid = 0;
    private int remainedMoney;
    public GameObject objectOnSelling;
    public TextMeshProUGUI remainedMoneyText;
    private bool runOneTime = true;
    public NavMeshSurface buildSurface;

    void Start()
    {
        objectOnSelling.SetActive(false);
        //buildSurface = gameObject.GetComponent<NavMeshSurface>();
    }

    
    void Update()
    {
        remainedMoney = priceOfObject - moneyPaid;
        remainedMoneyText.text = "$ " + (remainedMoney).ToString();
        unlockBar.fillAmount = (float)moneyPaid / priceOfObject;

        if (moneyPaid >= priceOfObject && runOneTime)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            objectOnSelling.SetActive(true);

            buildSurface.BuildNavMesh();

            
            runOneTime = false;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player") && (PlayerPrefs.GetInt("money") > 0))
        {

           
            if ((PlayerPrefs.GetInt("money")) <= remainedMoney)
            {
               
                moneyPaid = moneyPaid + PlayerPrefs.GetInt("money");
                
                PlayerPrefs.SetInt("money", 0);
            }
            else
            {
                moneyPaid = priceOfObject;
                PlayerPrefs.SetInt("money",(PlayerPrefs.GetInt("money") - remainedMoney));

            }
        
        }
        
    }
    



}
