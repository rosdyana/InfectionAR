using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandler : MonoBehaviour {
    [SerializeField]
    private float degrees;
    [SerializeField]
    private GameObject pnlGameOver;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 to = new Vector3(0, degrees, 0);

        //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime);
        transform.Rotate(degrees, 0, 0, Space.World);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "zombie")
        {
            Debug.Log("Game Over");
            GameOver(pnlGameOver);
        }
    }

    void GameOver(GameObject panel)
    {
        panel.SetActive(true);
    }
}
