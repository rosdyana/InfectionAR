using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{

    public Button fireButton;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private int power_bullet;
    [SerializeField]
    private AudioSource shootFx;
    public Animation anim;
    [SerializeField]
    private string goal;
    [SerializeField]
    private GameObject pnlGameOver;
    [SerializeField]
    private GameObject pnlPrize;
    [SerializeField]
    private GameObject pnlIntro;
    [SerializeField]
    private int showIntroFor;
    [SerializeField]
    private Text txtCountZombie;
    private bool checkZombie = true;
    private int countDeadZombie = 0;

    [SerializeField]
    private AudioSource deathFx;

    //public bool targetAchieved = false;

    void Start()
    {

        fireButton.onClick.AddListener(OnButtonDown);
        StartCoroutine(showPanelWithTime(showIntroFor, pnlIntro));
        txtCountZombie.text = "0";
    }

    IEnumerator showPanelWithTime(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkZombie)
        {
            if (countDeadZombie == 10)
            {
                PrizePanel(pnlPrize);
                //targetAchieved = true;
                checkZombie = false;
            }
        }

    }


    void OnButtonDown()
    {

        //GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
        //Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //bullet.transform.rotation = Camera.main.transform.rotation;
        //bullet.transform.position = Camera.main.transform.position;
        //rb.AddForce(Camera.main.transform.forward * power_bullet);
        //bullet.GetComponent<AudioSource>().Play();
        //Destroy(bullet, 3);
        RaycastHit _hit;
        Debug.Log("WE shoot ");
        shootFx.Play();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, Mathf.Infinity, mask))
        {
            Debug.Log("WE hit " + _hit.collider.tag);
            if(_hit.collider.tag == "zombie")
            {
                deathFx.Play();
                _hit.collider.gameObject.GetComponent<EnemyHandler>().StopAnimation("zombie_walk");
                _hit.collider.gameObject.GetComponent<EnemyHandler>().PlayAnimation("back_fall");
                Destroy(_hit.collider.gameObject);
                countDeadZombie += 1;
                txtCountZombie.text = countDeadZombie.ToString();
                Debug.Log("dead zombies : " + countDeadZombie);
            }
            if(_hit.collider.tag == goal)
            {
                Destroy(_hit.collider.gameObject);
                GameOver(pnlGameOver);
            }
        }
    }

    void GameOver(GameObject panel)
    {
        panel.SetActive(true);
    }

    void PrizePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

}//end of file
