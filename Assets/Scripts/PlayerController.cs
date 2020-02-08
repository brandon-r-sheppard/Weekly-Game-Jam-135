using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GUN { TMP }
public class PlayerController : MonoBehaviour
{
    public LayerMask playerLayer;
    private const float MOVE_SPEED = 8f;
    private const float BULLET_SPEED = 50f;
    public GameObject BulletPrefab;//Remove later
    public GameObject FireLocation;
    public GameObject ActiveGoggles;
    public GameObject InactiveGoggles;
    private GUN _activeWeapon;
    private Queue<GUN> _inventory;
    private Animator _anim;
    private Vector2 _lastIncrement;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = new Queue<GUN>();
        _inventory.Enqueue(GUN.TMP);
        _activeWeapon = _inventory.Dequeue();
        _anim = gameObject.GetComponent<Animator>();
        _anim.SetBool("HAS_GUN", true);
        _anim.SetBool("IS_IDLE", true);
    }

    /// <summary>
    /// Swaps the active weapon with the weapon at the top of the Inventory Queue, and puts the previously active weapon at the bottom of the queue.
    /// </summary>
    void CycleWeapons()
    {
        _inventory.Enqueue(_activeWeapon);
        _activeWeapon = _inventory.Dequeue();
    }
    IEnumerator Shoot()
    {
        _anim.SetBool("IS_SHOOTING", true);
        yield return new WaitForSeconds(0.2f);
        InactiveGoggles.GetComponent<SkinnedMeshRenderer>().enabled = false;
        ActiveGoggles.GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject bullet = null;
        if (_activeWeapon == GUN.TMP)
            bullet = GameObject.Instantiate(BulletPrefab);
        //Add new gun prefabs here as else if statements, make sure to add the appropriate GUN enum

        bullet.transform.position = FireLocation.transform.position;
        
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * BULLET_SPEED;
    }
    /// <summary>
    /// Function to determine how to move the player for a given frame.
    /// </summary>
    Vector2 MovePlayer()
    {
        _anim.SetBool("IS_IDLE", false);
        float x = Input.GetAxis("Horizontal") * MOVE_SPEED * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * MOVE_SPEED * Time.deltaTime;
        transform.position += new Vector3(x, 0, z);
        _anim.SetBool("IS_RUNNING", (x == 0 && z == 0 ? false : true));
        return new Vector2(x, z);
    }

    /// <summary>
    /// Function to rotate the player towards the mouse position.
    /// </summary>
    void RotateToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = -(Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(new Vector3(0, angle + 90, 0));
    }

    /// <summary>
    /// A collection of helpful keybinds to help the debugging process.
    /// </summary>
    void DebugInput()
    {
        if (Input.GetKey(KeyCode.F))
        {
            StartCoroutine(Shoot());
        }
        else
        {
            StopAllCoroutines();
            _anim.SetBool("IS_SHOOTING", false);
            InactiveGoggles.GetComponent<SkinnedMeshRenderer>().enabled = true;
            ActiveGoggles.GetComponent<SkinnedMeshRenderer>().enabled = false;
            if (_lastIncrement == Vector2.zero)
                _anim.SetBool("IS_IDLE", true);
            //Debug.Log("I got here...");
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
        _lastIncrement = MovePlayer();
        DebugInput(); //Remove Later
    }
}
