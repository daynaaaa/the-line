// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RoadGeneration : MonoBehaviour
// {
//     public float canvasWidth;
//     public float canvasHeight;
//     public int gridWidth; // can be 3 - 11
//     public int gridHeight; // can be 3 - 11
//     public float blockWidth;
//     public float blockHeight;
//     public Transform parentTransform; // to have blocks be generated within RoadGeneration script

//     // slow down updates
//     public float updateInterval; // Adjust timing as needed
//     private float updateTimer = 0f;

//     public static int score;

//     // for random path generation
//     private List<GameObject> activeBlocks = new List<GameObject>();
//     private int currentRow;
    
//     private int currentCol;
//     private int nextCol;
//     //private float changeY = 0; // how much upwards each row needs to be adjusted

//     void Start() {
//         canvasHeight = Camera.main.orthographicSize * 2;
//         canvasWidth = Camera.main.aspect * canvasHeight;
//         blockWidth = canvasWidth / gridWidth;
//         blockHeight = canvasHeight / gridHeight;
//         score = 0;
//         GenerateInitialRoad();
//     }
 
//     void Update() {
//         if (MoveByTouch.start) {
//             score++;
//             updateTimer += Time.deltaTime;
//             if (updateTimer >= updateInterval) {
//                 UpdateRoad();
//                 updateTimer = 0f;
//             }
//         }
//     }

//     // create block at position
//     void GenerateBlock(Vector2 position) {
//         GameObject block = BlockPool.SharedInstance.GetBlock();
//         block.transform.position = position;
//         //Vector2 currentScale = block.transform.localScale;
//         Vector2 currentScale = (block.GetComponent<SpriteRenderer>()).sprite.bounds.size;
//         block.transform.localScale = new Vector2(blockWidth / currentScale.x, blockHeight / currentScale.y);
//         activeBlocks.Add(block);
//     }

//     void GenerateInitialRoad() {
//         currentRow = 0;
//         currentCol = gridWidth / 2;
//         nextCol = currentCol;
//         for (int i = 0; i <= gridHeight; i++) {
//             for (int j = 0; j < gridWidth; j++) {
//                 //if (block != null) {
//                 if (j != currentCol && j != nextCol) {
//                     Vector2 position = new Vector2(j * blockWidth + blockWidth / 2, currentRow * blockHeight + canvasHeight / 3);
//                     GenerateBlock(position);
//                 }
//                 //}
//             }
//             currentRow++; // up
//             //Vector2 position = new Vector2(currentCol * blockWidth + blockWidth / 2, currentRow * blockHeight + blockHeight / 2);
//             //GenerateBlock(position);
//             //changeY += blockHeight;

//             GenerateRandomRow();
//         }
//         currentRow--;
//     }

//     void GenerateRandomRow() {
//         // 0 to go left, 1 to go right, 2 - 3 to go straight
//         int dir = Random.Range(0, 4);
//         currentCol = nextCol;

//         if (dir == 0 && currentCol > 0) {
//             nextCol--; // left
//         }
//         else if (dir == 0) {
//             nextCol++; // right
//         }
//         else if (dir == 1 && currentCol < gridWidth - 1) {
//             nextCol++; // right
//         }
//         else if (dir == 1) {
//             nextCol--; // left
//         }
//         //currentRow--; // up
//     }

//     void UpdateRoad() {
//         List<GameObject> blocksToRemove = new List<GameObject>();

//         foreach (GameObject block in activeBlocks) {
//             // remove the bottom row
//             if (block.transform.position.y <= canvasHeight / 3) {
//                 blocksToRemove.Add(block);
//             }
//             // move other blocks down
//             else {
//                 block.transform.position = new Vector2(block.transform.position.x, block.transform.position.y - blockHeight);

//             }
//         }

//         // remove blocks in blocksToRemove
//         foreach (GameObject block in blocksToRemove) {
//             activeBlocks.Remove(block);
//             BlockPool.SharedInstance.ReturnBlock(block);
//         }

//         // generate new row
//         for (int j = 0; j < gridWidth; j++) {
//             //if (block != null) {
//             if (j != currentCol && j != nextCol) {
//                 Vector2 position = new Vector2(j * blockWidth + blockWidth / 2, currentRow * blockHeight + canvasHeight / 3);
//                 GenerateBlock(position);
//             }
//             //}
//         }
//         //currentRow++;
//         GenerateRandomRow();
//             /*
//         if (changeY >= blockHeight * (gridHeight)) {
//             Vector2 position = new Vector2(currentCol * blockWidth, currentRow * blockHeight);
//             GenerateBlock(position);
//             changeY += blockHeight;
//             GenerateRandomRow();
//         }*/
//     }
// }
