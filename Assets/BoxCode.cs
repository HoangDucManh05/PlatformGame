using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCode : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("player"))
        {
            StartCoroutine(DisappearAndRespawn());
        }
    }

    private IEnumerator DisappearAndRespawn()
    {
        // Bien mat box sau n giây
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

        // ??i n giây và hi?n l?i box
        Invoke("RespawnBox", 3f);
    }
    private void RespawnBox()
    {
        gameObject.SetActive(true); // Hi?n l?i box
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
