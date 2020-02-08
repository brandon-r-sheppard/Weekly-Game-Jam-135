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
    private GUN _activeWeapon;
    private Queue<GUN> _inventory;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = new Queue<GUN>();
        _inventory.Enqueue(GUN.TMP);
        _activeWeapon = _inventory.Dequeue();
    }

    /// <summary>
    /// Swaps the active weapon with the weapon at the top of the Inventory Queue, and puts the previously active weapon at the bottom of the queue.
    /// </summary>
    void CycleWeapons()
    {
        _inventory.Enqueue(_activeWeapon);
        _activeWeapon = _inventory.Dequeue();
    }
    void Shoot()
    {
        GameObject bullet = null;
        if (_activeWeapon == GUN.TMP)
            bullet = GameObject.Instantiate(BulletPrefab);
        //Add new gun prefabs here as else if statements, make sure to add the appropriate GUN enum

        if (bullet == null)
            return; //Chicken out so we don't get a null object error
        bullet.transform.position = FireLocation.transform.position;
        
        //Lazy fix to game object rotation be wrong. I am using the transform.right as the transform.forward in the velocity calculation
        bullet.GetComponent<Rigidbody>().velocity = transform.right * BULLET_SPEED;
    }
    /// <summary>
    /// Function to determine how to move the player for a given frame.
    /// </summary>
    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal") * MOVE_SPEED * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * MOVE_SPEED * Time.deltaTime;
        transform.position += new Vector3(x, 0, z);
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
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    /// <summary>
    /// A collection of helpful keybinds to help the debugging process.
    /// </summary>
    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }
    // Update is called once per frame
    void Update()
    {
        RotateToMouse();
        MovePlayer();
        DebugInput(); //Remove Later
    }
}
