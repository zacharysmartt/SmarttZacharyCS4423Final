using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    public GameObject FruitPrefab;
    /*public GameObject ApplePrefab;
    public GameObject OrangePrefab;
    public GameObject CherriesPrefab;
    public GameObject BananasPrefab;
    public GameObject GrapesPrefab;
    public GameObject MangoPrefab;*/

    public List <GameObject> fruit;
    // Start is called before the first frame update

    void Awake() {
        fruit = new List<GameObject>(Resources.LoadAll<GameObject>("FruitObjects"));
    }
    void Start()
    {
        CreateFruit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerHand") {
            CreateFruit();
        }
    }

    void CreateFruit() {
        StartCoroutine(CreateFruitRoutine());
        IEnumerator CreateFruitRoutine() {
            GameObject newFruit;
            //int fruitType = Random.Range(0, fruit.Count - 1);
            //float spawnTime = Random.Range(10f, 30f);
            Vector2 FruitPosition = new Vector2((transform.position.x),(transform.position.y - 1.5f));
            while(true) {
                int fruitType = Random.Range(0, fruit.Count - 1);
                float spawnTime = Random.Range(10f, 30f);
                yield return new WaitForSeconds(spawnTime);
                newFruit = Instantiate(fruit[fruitType], FruitPosition, Quaternion.identity);
                Destroy(newFruit,20f);
            }
        }

        /*GameObject newFruit;
        Vector2 FruitPosition = new Vector2((transform.position.x),(transform.position.y - 1.5f));
        newFruit = Instantiate(FruitPrefab, FruitPosition, Quaternion.identity);
        Destroy(newFruit,30f);*/

    }
}
