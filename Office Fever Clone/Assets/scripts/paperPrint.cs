using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class paperPrint : MonoBehaviour
{
    public int maximumPaperSize;
    public GameObject paper;
    public List<GameObject> papersOnTable = new List<GameObject>();
    public Transform[] paperPlaces =new Transform[10];
    private int paperPlaceIndex=0;
    private float Yaxis = 0;
    public float instantiateTime;
    bool onOperation = false;

    private void Start()
    {
        AI_NPC.printerDestinations.Add(gameObject.transform.GetChild(1).transform);
        AI_NPC.targetPrinters.Add(GetComponent<paperPrint>());
        for(int i =0; i< transform.GetChild(0).transform.childCount; i++)
        {
            paperPlaces[i] = transform.GetChild(0).transform.GetChild(i).gameObject.transform;
        }
    }
    private void Update()
    {
        StartCoroutine(printPaper(instantiateTime));
    }

    IEnumerator printPaper(float time_)
    {
        
        if((papersOnTable.Count < maximumPaperSize || papersOnTable.Contains(null)) && onOperation == false)
        {

            onOperation = true;
            Vector3 target;

            var instPaper = Instantiate(paper, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z + 0.1f), Quaternion.identity, transform.GetChild(0));

            if (papersOnTable.Contains(null))
            {
                int pp_index = (papersOnTable.IndexOf(null) % 10);
                //Debug.Log("papersOnTable.IndexOf(null): " + papersOnTable.IndexOf(null) + "  (papersOnTable.IndexOf(null) % 10):  " + (papersOnTable.IndexOf(null) % 10));
                float YAxis = ((papersOnTable.IndexOf(null) - (papersOnTable.IndexOf(null) % 10f))/10)*0.1f;
                //Debug.Log("YAxis: " + YAxis);
            
                papersOnTable[papersOnTable.IndexOf(null)] = instPaper;
                target = new Vector3(paperPlaces[pp_index].position.x, paperPlaces[pp_index].position.y + 0.01f + YAxis, paperPlaces[pp_index].position.z);
            
            }
            else
            {
                papersOnTable.Add(instPaper);
                target = new Vector3(paperPlaces[paperPlaceIndex].position.x, paperPlaces[paperPlaceIndex].position.y + 0.01f + Yaxis, paperPlaces[paperPlaceIndex].position.z);
                if (paperPlaceIndex < 9) paperPlaceIndex++;
                else
                {
                    paperPlaceIndex = 0;
                    Yaxis += 0.1f;
                }

            }

            //papersOnTable.Add(instPaper);
            //Vector3 target = new Vector3(paperPlaces[paperPlaceIndex].position.x, paperPlaces[paperPlaceIndex].position.y+0.01f + Yaxis, paperPlaces[paperPlaceIndex].position.z);

            instPaper.transform.DOJump(target, 1.5f, 1, 0.5f).SetEase(Ease.OutQuad);

            
            yield return new WaitForSeconds(time_);
            onOperation = false;
        }
        
    }

    //if (papersOnTable.Contains(null))
    //{
    //    int pp_index = (papersOnTable.IndexOf(null) % 10);
    //    var YAxis = (((papersOnTable.IndexOf(null) - papersOnTable.IndexOf(null)) % 10f)/10)*0.1f;
    //
    //    papersOnTable[papersOnTable.IndexOf(null)] = instPaper;
    //    target = new Vector3(paperPlaces[pp_index].position.x, paperPlaces[pp_index].position.y + 0.01f + YAxis, paperPlaces[pp_index].position.z);
    //
    //}
    //else
    //{
    //    papersOnTable.Add(instPaper);
    //    target = new Vector3(paperPlaces[paperPlaceIndex].position.x, paperPlaces[paperPlaceIndex].position.y + 0.01f + Yaxis, paperPlaces[paperPlaceIndex].position.z);
    //
    //}
}
