using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

    public GameObject FruitPrefab;
    // Start is called before the first frame update
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
            Vector2 FruitPosition = new Vector2((transform.position.x),(transform.position.y - 1.5f));
            while(true) {
                yield return new WaitForSeconds(30f);
                newFruit = Instantiate(FruitPrefab, FruitPosition, Quaternion.identity);
                Destroy(newFruit,30f);
            }
        }

        /*GameObject newFruit;
        Vector2 FruitPosition = new Vector2((transform.position.x),(transform.position.y - 1.5f));
        newFruit = Instantiate(FruitPrefab, FruitPosition, Quaternion.identity);
        Destroy(newFruit,30f);*/

    }
}
