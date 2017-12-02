using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    public Button fireButton;
    private Rigidbody rb;
    private Animation anim;
    [SerializeField]
    private float movement_speed;
    [SerializeField]
    private int power_bullet;
    [SerializeField]
    private AudioSource shootFx;

    // Use this for initialization
    void Start () {
        fireButton.onClick.AddListener(OnButtonDown);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {

        float x = CrossPlatformInputManager.GetAxis("Horizontal");
        float y = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, y);

        rb.velocity = movement * movement_speed;

        if(x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        if (x != 0 && y != 0)
        {
            //anim.Play("Walking_Trump");
            anim.Play("infantry_combat_run");
        }
        else
        {
            //anim.Play("Idle_Trump");
            anim.Play("infantry_combat_idle");
        }

    }

    void OnButtonDown()
    {
        Transform playerTransf = GameObject.FindGameObjectWithTag("player").transform;
        GameObject bullet = Instantiate(Resources.Load("m_bullet", typeof(GameObject))) as GameObject;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.rotation = playerTransf.rotation;
        bullet.transform.position = playerTransf.position;
        rb.AddForce(playerTransf.forward * power_bullet);
        shootFx.Play();
        Destroy(bullet, 3);
        Debug.Log("WE shoot ");
    }

}
