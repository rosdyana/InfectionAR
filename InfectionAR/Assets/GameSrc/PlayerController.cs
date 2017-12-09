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
    [SerializeField]
    private GameObject pnlPrize;
    [SerializeField]
    private GameObject pnlIntro;
    private int countZombie = 0;
    private bool isWin = true;
    [SerializeField]
    private AudioSource deathSoundFx;


    void Start () {
        fireButton.onClick.AddListener(OnButtonDown);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        StartCoroutine(showPanelWithTime(4, pnlIntro));
    }

    IEnumerator showPanelWithTime(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
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

        countZombie = GameObject.FindGameObjectsWithTag("zombie").Length;
        if (countZombie == 0 && isWin)
        {
            showPanel(pnlPrize);
            isWin = false;
        }
        //Debug.Log("count zombie : " + countZombie);

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "zombie")
        {
            Debug.Log("Enemy Destroyed");
            Destroy(col.gameObject,0.3f);
            deathSoundFx.Play();
        }
    }

    void OnButtonDown()
    {
        anim.Play("infantry_combat_shoot");
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

    void showPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

}
