using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController charController;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject ropeObject;
    [SerializeField]
    Transform ropeParent;
    [SerializeField]
    float moveSpeed = 3;
    [SerializeField]
    int maxRopes = 1;
    Vector3 currentMovement;

    public delegate void DeathAction();
    public static event DeathAction OnDeath;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootRope();
        }
#if UNITY_ANDROID
        MovePlayerFromButtons();
#endif
        MovePlayerFromInput();
    }

    void MovePlayerFromButtons()
    {
        if (gameManager.gamePaused)
        {
            return;
        }
        charController.Move(currentMovement * moveSpeed * Time.deltaTime);
        
    }

    /// <summary>
    /// used to move player with the unity input system. usable with keyboard if playing in editor
    /// </summary>
    void MovePlayerFromInput()
    {
        if (gameManager.gamePaused)
        {
            return;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            charController.Move(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            charController.Move(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
    /// <summary>
    /// the SetPlayerMove methods are used to set movement when touch button is pressed
    /// </summary>
    public void SetPlayerMoveLeft()
    {
        if (gameManager.gamePaused)
        {
            return;
        }
        currentMovement = Vector3.left;
    }
    public void SetPlayerMoveRight()
    {
        if (gameManager.gamePaused)
        {
            return;
        }
        currentMovement = Vector3.right;
    }
    public void SetPlayerMoveZero()
    {
        if (gameManager.gamePaused)
        {
            return;
        }
        currentMovement = Vector3.zero;
    }

    public void ShootRope()
    {
        //limit amount of ropes on screen, a sort of ammo system
        if (ropeParent.childCount >= maxRopes)
        {
            return;
        }
        GameObject newRope = Instantiate(ropeObject,ropeObject.transform.position,ropeObject.transform.rotation, ropeParent);
        newRope.SetActive(true);
    }

    public void Die()
    {
        OnDeath();
    }
}
