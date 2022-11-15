using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollect : MonoBehaviour
{
    public bool isMoneyCollected = false;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        if (isMoneyCollected == true)
        {
            Vector3 target = player.position;
            target.y = player.position.y + 1f;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, Time.deltaTime * 12f);
            if (Vector3.Magnitude(gameObject.transform.position - target) < 0.2f)
            {
                PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 10);
                Destroy(gameObject);
                
                
            }
        }
    }
}
