using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePool : MonoBehaviour
{

    public static TreePool instance;

    private List<GameObject> pooledTrees = new List<GameObject>();

    public int treeCount;
    public int treeLimit = 10;

    [SerializeField] private GameObject treePrefab;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        treeCount = GameObject.FindGameObjectsWithTag("Tree").Length;
        Debug.Log("Total trees in current scene: " + treeCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < treeLimit - treeCount; i++) {
            GameObject tree = Instantiate(treePrefab);
            tree.SetActive(false);
            pooledTrees.Add(tree);
        }
        Debug.Log("Total trees in pool:" + pooledTrees.Count);
    }

    public GameObject GetPooledTree() {
        for (int i = 0; i < pooledTrees.Count; i++) {
            if (!(pooledTrees[i].activeInHierarchy)) {
                treeCount++;
                Debug.Log("Current tree count:" + treeCount);
                return pooledTrees[i];
            }
        }

        return null;
    }
}
