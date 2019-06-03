using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Collect : MonoBehaviour
{

    private UI _Ui;

    // Start is called before the first frame update
    void Start()
    {
        _Ui = GameObject.Find("UI").GetComponent<UI>();
    }

    // Update is called once per fram  e
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _Ui.UpdateCoin();
            Destroy(this.gameObject);
        }
    }

}
