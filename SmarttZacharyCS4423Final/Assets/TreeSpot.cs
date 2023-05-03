using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpot : MonoBehaviour
{
   public bool hasTree = false;

   public void GetTree() {
        GameObject tree = TreePool.instance.GetPooledTree();

        if (tree != null) {
            tree.transform.position = this.transform.position;
            tree.SetActive(true);
            hasTree = true;
        }
   }
}
