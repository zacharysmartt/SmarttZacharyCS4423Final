using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpot : MonoBehaviour
{
   public bool hasTree = false;

   public void GetTree() {
        GameObject tree = TreePool.instance.GetPooledTree();
        Debug.Log("Tree retrieved from pool");

        if (tree == null) {
            Debug.Log("Can't spawn tree");
        }
        else {
            Debug.Log("Spawn tree");
            tree.transform.position = this.transform.position;
            tree.SetActive(true);
            hasTree = true;
            Debug.Log("Tree spawned");
        }
   }
}
