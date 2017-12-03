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
    // Use this for initialization
    void Start()
    {

        fireButton.onClick.AddListener(OnButtonDown);


    }
    // Update is called once per frame
    void Update()
    {



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
                _hit.collider.gameObject.GetComponent<EnemyHandler>().StopAnimation("zombie_walk");
                _hit.collider.gameObject.GetComponent<EnemyHandler>().PlayAnimation("back_fall");
                Destroy(_hit.collider.gameObject,2);
            }
        }
    }

}//end of file
