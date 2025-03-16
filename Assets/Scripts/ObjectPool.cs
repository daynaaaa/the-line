using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledBlocks;
    public List<GameObject> pooledBoosters;

    public GameObject blockPrefab;
    public GameObject boosterPrefab;

    public int blockPoolSize; // depending on path size
    public int boosterPoolSize;

    public Transform parentTransform; // to have objects be generated within RoadGeneration script


    private void Awake() {
        SharedInstance = this;
    }

    private void Start() {
        // to populate the pool
        pooledBlocks = new List<GameObject>();
        pooledBoosters = new List<GameObject>();

        GameObject block;
        GameObject booster;

        for(int i = 0; i < blockPoolSize; i++)
        {
            block = Instantiate(blockPrefab);
            block.transform.SetParent(parentTransform);
            block.transform.localScale = Vector2.one;
            block.SetActive(false);
            pooledBlocks.Add(block);
        }
        for(int i = 0; i < boosterPoolSize; i++)
        {
            booster = Instantiate(boosterPrefab);
            booster.transform.SetParent(parentTransform);
            booster.SetActive(false);
            pooledBoosters.Add(booster);
        }
    } 

    // get a block from the pool
    public GameObject GetBlock() {
        GameObject block;
        // if enough blocks
        if (pooledBlocks.Count > 0) {
            block = pooledBlocks[0];
            pooledBlocks.RemoveAt(0);
            block.SetActive(true);
        }
        // expand pool if needed
        else {
            block = Instantiate(blockPrefab);
            block.transform.SetParent(parentTransform);
        }
        return block;
    }

    // return block back to the pool
    public void ReturnBlock(GameObject block) {
        block.SetActive(false);
        pooledBlocks.Add(block);
    }

    // get a booster from the pool
    public GameObject GetBooster() {
        GameObject booster;
        // if enough boosters
        if (pooledBoosters.Count > 0) {
            booster = pooledBoosters[0];
            pooledBoosters.RemoveAt(0);
            booster.SetActive(true);
        }
        // expand pool if needed
        else {
            booster = Instantiate(boosterPrefab);
            booster.transform.SetParent(parentTransform);
        }
        return booster;
    }

    // return booster back to the pool
    public void ReturnBooster(GameObject booster) {
        booster.SetActive(false);
        pooledBoosters.Add(booster);
    }
}
