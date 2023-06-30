using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private FollowerCreator followerCreator;
    private GameManager gm;
    [SerializeField] private LayerMask Wall;
    public float minXBound = -5.75f;
    public float maxXBound = 5.75f;
    public float speed = 23;
    public float swipeSpeed;
    private Animator animator;

    
    public void Awake()
    {
        followerCreator = GameObject.FindGameObjectWithTag("PlayerBase").GetComponent<FollowerCreator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        animator=GameObject.FindGameObjectWithTag("SkipperCharacter").GetComponent<Animator>();
    }

    private void Update()
    {
        if (gm.gameStart)
        {
            if (Input.GetMouseButton(0))
            {
                Move();
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXBound, maxXBound), transform.position.y, transform.position.z);
            CheckXBound();  
        }
    }

    private void FixedUpdate()
    {
        
       

        if (gm.gameStart)
        {
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
            animator.SetBool("Run",true);
            animator.SetBool("idle",false);

        }  
        else{
        animator.SetBool("idle",true);
        
        animator.SetBool("Run",false);
        }      
    }

    public void Move()
    {
        swipeSpeed = 25f;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.localPosition.z;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray,out RaycastHit hit, 50))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = transform.position.y;
            hitPoint.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, hitPoint, Time.deltaTime * swipeSpeed);
        }
    }

    private void CheckXBound()
    {
        float minX = transform.position.x;
        float maxX = transform.position.x;

        for (int i = 0; i < followerCreator.players.Count; i++)
        {
            if (followerCreator.players[i].transform.position.x < minX)
            {
                minX = followerCreator.players[i].transform.position.x;
            }
            if (followerCreator.players[i].transform.position.x > maxX)
            {
                maxX = followerCreator.players[i].transform.position.x;
            }
        }

        Vector3 LeftControl = new Vector3(minX, transform.position.y, transform.position.z);
        Vector3 RightControl = new Vector3(maxX, transform.position.y, transform.position.z);

        if (Physics.Raycast(LeftControl, Vector3.left, 0.5f, Wall))
        {
            minXBound = transform.position.x;
        }
        else
        {
            minXBound = -5.75f;
        }

        if (Physics.Raycast(RightControl, Vector3.right, 0.5f, Wall))
        {
            maxXBound = transform.position.x;
        }
        else
        {
            maxXBound = 5.75f;
        }
    }
}
