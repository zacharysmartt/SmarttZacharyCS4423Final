using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTree : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite orange;
    public Sprite yellow;
    public Sprite red;
    public Sprite brown;
    public GameObject FruitPrefab;
    public List <GameObject> fruit;
    // Start is called before the first frame update

    void Awake() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        int color = Random.Range(0, 4);
        switch (color) {
            case 0:
                sr.sprite = orange;
                break;
            case 1:
                sr.sprite = yellow;
                break;
            case 2:
                sr.sprite = red;
                break;
            default:
                sr.sprite = brown;
                break;
        }
        fruit = new List<GameObject>(Resources.LoadAll<GameObject>("FruitObjects"));
    }
    void Start()
    {
        CreateFruit();
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
            Vector2 FruitPosition = new Vector2((transform.position.x),(transform.position.y - 1.5f));
            while(true) {
                int fruitType = Random.Range(0, fruit.Count);
                float spawnTime = Random.Range(10f, 30f);
                yield return new WaitForSeconds(spawnTime);
                newFruit = Instantiate(fruit[fruitType], FruitPosition, Quaternion.identity);
                Destroy(newFruit,20f);
            }
        }
    }

}