using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plant : MonoBehaviour
{
    public float timeToHarvest = 5f;
    public Sprite[] stages;
    public SpriteRenderer mainSprite;
    private float growTime = 0;
    
    public GameObject coinPf;
    public Vector2Int countRange;
    public float spawnRad;
    private Target _target;

    private Interactable _interactable;

    private GameObject plantManager;

    private GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        owner = GetClosest();
        plantManager = GameObject.Find("PlantManager");
        PlantManager pManager = plantManager.GetComponent<PlantManager>();
        pManager.AddPlant(this.gameObject);
        _target = GetComponent<Target>();
        _interactable = GetComponentInChildren<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        growTime += Time.deltaTime;
        
        float dt = timeToHarvest / (stages.Length - 1);

        if (growTime <= timeToHarvest+Time.maximumDeltaTime)
        {
            int stage = Math.Min(stages.Length - 1, Mathf.FloorToInt(growTime / dt));
            mainSprite.sprite = stages[stage];
        }

        _interactable.hint = growTime >= timeToHarvest ? "Harvest!" : "Growing...";
    }

    public void OnInteract(Player player)
    {
        if (growTime >= timeToHarvest)
        {
            for (int i = 0; i < Random.Range(countRange.x, countRange.y); i++)
            {
                var coin = Instantiate(coinPf, (Vector2)transform.position + Random.insideUnitCircle * spawnRad,
                    Quaternion.identity);
            }
            growTime = 0;
        }
    }

    private GameObject GetClosest()
    {
        GameObject player_L = GameObject.Find("Player_L");
        GameObject player_R = GameObject.Find("Player_R");

        float distanceToPlayer_L = Vector3.Distance(transform.position, player_L.transform.position);
        float distanceToPlayer_R = Vector3.Distance(transform.position, player_R.transform.position);

        if (distanceToPlayer_L <= distanceToPlayer_R)
        {
            return player_L;
        }
        else
        {
            return player_R;
        }
    }

    public GameObject getOwner()
    {
        return owner;
    }

}
