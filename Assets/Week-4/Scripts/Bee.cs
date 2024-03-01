using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bee : MonoBehaviour
{

    private Hive myhive;

    private int searchTimeMax = 400;
    private float searchTimer = 400;

    private bool ready = true;

    public void Init(Hive hive)
    {
        myhive = hive;
    }

    private void CheckAnyFlower()
    {
        Flower[] flowers = FindObjectsByType<Flower>(FindObjectsSortMode.None);
        int randomIndex = Random.Range(0, flowers.Length);
        Flower chosenFlower = flowers[randomIndex];

        transform.DOMove(chosenFlower.transform.position, 1f)
        .OnComplete(() =>
        {
            bool hasNectar = chosenFlower.TakeNectar();

            if (hasNectar)
            {
                transform.DOMove(myhive.transform.position, 1f)
                .OnComplete(() =>
                {
                    myhive.GiveNectar();

                }).SetEase(Ease.Linear);

                Debug.Log("Testyyyyy");
            }
            else
            {
                
            }

            Debug.Log("Test");

        }).SetEase(Ease.Linear);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (searchTimer < searchTimeMax)
        {
            searchTimer += 1;
        }
        else
        {
            searchTimer = 0;
            ready = false;
            CheckAnyFlower();
        }
    }
}
