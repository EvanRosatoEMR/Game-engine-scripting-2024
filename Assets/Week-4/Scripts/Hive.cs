using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    private float honeyTimeMax = 100;
    private float honeyTimer = 0;

    private int honeyCount = 0;
    private int nectarCount = 0;
    private int beeCount = 3;

    private float beeTimer = 0;
    private float beeTimeMax = 1000;

    [SerializeField] Transform hivey;

    [SerializeField] Bee beePrefab;

    public void GiveNectar()
    {
        nectarCount++;
    }

    private void Awake()
    {
        for (int i = 0; i < beeCount; i++)
        {
            Bee bee = Instantiate(beePrefab, hivey);
            bee.Init(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (honeyTimer < honeyTimeMax && nectarCount > 0)
        {
            honeyTimer += 1;

        } else if (honeyTimer >= honeyTimeMax)
        {
            honeyCount++;
            nectarCount--;

            honeyTimer = 0;
        }

        if (beeTimer < beeTimeMax)
        {
            beeTimer += 1;

        } else if (beeTimer >= beeTimeMax && honeyCount > 0)
        {
            Bee bee = Instantiate(beePrefab, hivey);
            bee.Init(this);

            honeyCount--;
            beeTimer = 0;
        }
    }
}
