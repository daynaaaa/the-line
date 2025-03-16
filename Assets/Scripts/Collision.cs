using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    // Checks if there is a collision between obj1 and obj2
    public static bool CheckCollision(GameObject obj1, GameObject obj2) {
        // x and y coords of obj1 and obj2
        Vector2 pos1 = obj1.transform.position;
        Vector2 pos2 = obj2.transform.position;
        // width and height of obj1 and obj2
        Vector2 size1 = obj1.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 size2 = obj2.GetComponent<SpriteRenderer>().bounds.size;

        // boolean logic: if the dist between the center x co-ords of obj1 and obj2 
        //   is <= the sum of their half-sizes, they are overlapping (likewise for y)
        return ((Mathf.Abs(pos1.x - pos2.x) <= (size1.x/2  + size2.x/2)) &&
                (Mathf.Abs(pos1.y - pos2.y) <= (size1.y/2 + size2.y/2)));
    }
}
