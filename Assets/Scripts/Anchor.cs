using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Anchor : MonoBehaviour
{
    private Vector3 startPosition;

    [SerializeField] private float maxAngle;

    [SerializeField] private float rotatSpeed;
    [SerializeField] GameObject targetPoint;
    [SerializeField] Animator ani;

    private Rigidbody2D rig;
    public Collider2D col;

    public Vector2 MoveDirect { get; set; }
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistant;

    private bool isMove;
    private bool isBack;
    

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        this.isMove = false;
        isBack = false;
        transform.localPosition = Vector3.zero;
        startPosition = transform.position;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isMove)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * rotatSpeed) * maxAngle);
        }
            
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && !isMove)
        {
            MoveToTarget();
        }

        if (Vector3.Distance(transform.position, startPosition) >= maxDistant)
        {
            rig.velocity = - MoveDirect.normalized * moveSpeed;
            isBack = true;
        }

        if (isBack)
        {
            CheckFinishBack();
        }

        ani.SetBool("ThaCau", this.isMove);
        ani.SetBool("KeoCau", this.isBack);
    }

    public void MoveToTarget()
    {
        isMove = true;
        this.col.isTrigger = false;
        this.MoveDirect = new Vector3(targetPoint.transform.position.x-transform.position.x, targetPoint.transform.position.y-transform.position.y);
        rig.velocity = MoveDirect.normalized * moveSpeed;  

    }
    
    public void MoveBack(float weightRate)
    {
        isBack = true;
        float backSpeed = moveSpeed * (1 - weightRate);
        rig.velocity = - MoveDirect.normalized * backSpeed;
        this.col.isTrigger = true;
        Debug.Log(backSpeed);
    }

    
    public bool CheckFinishBack()
    {
        if (this.transform.position.y >= startPosition.y)
        {
            isMove = false;
            isBack = false;
            rig.velocity = Vector3.zero;
            MoveDirect = Vector3.zero;
            transform.localPosition = Vector3.zero;
            return true;
        }
        else
            return false;
    }

}
