using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadGeneration : MonoBehaviour
{
    private float canvasWidth;
    private float canvasHeight;
    public int gridWidth; // can be 3 - 11
    public int gridHeight; // can be 3 - 11
    private float blockWidth;
    private float blockHeight;
    public Transform parentTransform; // to have blocks be generated within RoadGeneration script
    public GameObject player;
    // slow down updates
    public float updateInterval;
    private float updateTimer = 0f;

    public static int score; // keep track of the score
    private int iterator; // reset to 0 after the blocks move the length of blockHeight

    // for random road generation
    private List<GameObject> activeBlocks = new List<GameObject>();
    private int currentRow;
    private int currentCol;
    private int nextCol;
    // boosters
    public static bool invincibility;
    public static bool smallSize;
    public static float playerSize;
    private float boosterTimer = 0f;
    private float boosterInterval = 5;
    public Color red;
    public Color blue;
    public Color purple;
    public Color yellow;

    void Start() {
        canvasHeight = Camera.main.orthographicSize * 2;
        canvasWidth = Camera.main.aspect * canvasHeight;
        blockWidth = canvasWidth / gridWidth;
        blockHeight = canvasHeight / gridHeight;
        score = 0;
        iterator = 0;
        invincibility = false;
        smallSize = false;
        playerSize = 1;
        player.transform.localScale = new Vector3(playerSize, playerSize);
        player.GetComponent<SpriteRenderer>().material.color = blue;
        GenerateInitialRoad();
    }
 
    void Update() {
        if (MoveByTouch.start) {
            // slow down the framerate
            updateTimer += Time.deltaTime;
            boosterTimer += Time.deltaTime;
            if (updateTimer >= updateInterval) {
                UpdateRoad();
                updateTimer = 0f;
                score++;
                iterator++;
            }

            // booster timer
            if (boosterTimer >= boosterInterval) {
                if (invincibility) { // turn off invincibility
                    invincibility = false;
                }
                if (smallSize) { // turn off small size
                    playerSize = 1f;
                    smallSize = false;
                    player.transform.localScale = new Vector2(playerSize, playerSize);
                }
                boosterTimer = 0f;
                // set player colour back to normal
                player.GetComponent<SpriteRenderer>().material.color = blue;
            }

        }
    }

    // create block at position
    void GenerateBlock(Vector2 position) {
        GameObject block = ObjectPool.SharedInstance.GetBlock();
        block.transform.position = position;
        // scale block to blockWidth x blockHeight
        Vector2 currentScale = (block.GetComponent<SpriteRenderer>()).sprite.bounds.size;
        block.transform.localScale = new Vector2(blockWidth / currentScale.x, blockHeight / currentScale.y);
        block.GetComponent<SpriteRenderer>().material.color = red;        
        activeBlocks.Add(block);
    }

    // generate the road on the starting screen
    void GenerateInitialRoad() {
        currentRow = 0;
        currentCol = gridWidth / 2;
        nextCol = currentCol;
        if (gridWidth % 2 == 0) --currentCol; // if even, widen the first path
        for (int i = 0; i <= gridHeight + 1; i++) {
            for (int j = 0; j < gridWidth; j++) {
                if (j != currentCol && j != nextCol) {
                    Vector2 position = new Vector2(j * blockWidth + blockWidth / 2, currentRow * blockHeight + canvasHeight / 3);
                    GenerateBlock(position);
                }
            }
            currentRow++; // up
            GenerateRandomRow();
        }
        currentRow--;
    }

    // set currentCol and nextCol to random adjacent road
    void GenerateRandomRow() {
        // 0 to go left, 1 to go right, 2 - 3 to go straight
        int dir = Random.Range(0, 4);
        currentCol = nextCol;

        if (dir == 0 && currentCol > 0) {
            nextCol--; // left
        }
        else if (dir == 0) {
            nextCol++; // right
        }
        else if (dir == 1 && currentCol < gridWidth - 1) {
            nextCol++; // right
        }
        else if (dir == 1) {
            nextCol--; // left
        }
    }

    // generate a new row at the top
    void GenerateRow() {
        for (int j = 0; j < gridWidth; j++) {
            Vector2 position = new Vector2(j * blockWidth + blockWidth / 2, currentRow * blockHeight + canvasHeight / 3);
            if (j != currentCol && j != nextCol) {
                GenerateBlock(position);
            }
            else { // generate random boosters
                int rand = Random.Range(0, 20);
                if (rand == 0) { // smallSize
                    GameObject booster = ObjectPool.SharedInstance.GetBooster();
                    booster.transform.position = position;
                    booster.GetComponent<SpriteRenderer>().material.color = purple;
                    activeBlocks.Add(booster);
                }
                else if (rand == 1) { // invincibility
                    GameObject booster = ObjectPool.SharedInstance.GetBooster();
                    booster.transform.position = position;
                    booster.GetComponent<SpriteRenderer>().material.color = yellow;
                    activeBlocks.Add(booster);
                }
            }
        }
        GenerateRandomRow();
    }

    // move blocks down and delete blocks that are out of frame
    void UpdateRoad() {
        List<GameObject> blocksToRemove = new List<GameObject>();
        foreach (GameObject block in activeBlocks) { // could be block or booster
            // remove row when out of view
            if (block.transform.position.y <= - blockHeight) {
                blocksToRemove.Add(block);
            }
            // check collision with player -> optimized as it loops through the blocks once to update their position and check for collisions simultaneously
            else if (Collision.CheckCollision(block, player)) {
                // collision detected
                // block collision
                if (block.GetComponent<SpriteRenderer>().material.color == red) {
                    if(invincibility) {
                        blocksToRemove.Add(block);
                    }
                    else {
                        SceneManager.LoadSceneAsync(2);
                    }
                }
                else if(block.GetComponent<SpriteRenderer>().material.color == purple){// smallSize collision
                    boosterTimer = 0f;
                    smallSize = true;
                    playerSize = 0.5f;
                    player.transform.localScale = new Vector2(playerSize, playerSize);
                    player.GetComponent<SpriteRenderer>().material.color = purple;
                    // remove booster 
                    blocksToRemove.Add(block);
                }
                else if(block.GetComponent<SpriteRenderer>().material.color == yellow){// invincibility collision
                    boosterTimer = 0f;
                    invincibility = true;
                    player.GetComponent<SpriteRenderer>().material.color = yellow;
                    // remove booster 
                    blocksToRemove.Add(block);
                }
            }
            // move other blocks & boosters down
            else {
                block.transform.position = new Vector2(block.transform.position.x, block.transform.position.y - 1);
            }
        }

        // generate new row
        if (iterator > blockHeight) {
            GenerateRow();
            iterator = 0;
        }

        // remove blocks/boosters in blocksToRemove
        foreach (GameObject block in blocksToRemove) {
            activeBlocks.Remove(block);
            if (block.GetComponent<SpriteRenderer>().material.color == red)
                ObjectPool.SharedInstance.ReturnBlock(block);
            else
                ObjectPool.SharedInstance.ReturnBooster(block);
        }
    }
}
