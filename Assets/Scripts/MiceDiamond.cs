using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiceDiamond : Mineral
{
    private bool movingToTargetPos;
    private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float moveSpeed;
    private SpriteRenderer sr;
    
    
    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        this.Value = 600;
        this.WeightRate = 0.1f;
        this.followDistant = this.gameObject.GetComponent<Collider2D>().bounds.size.y / 2;
        this.startPosition = transform.position;
        movingToTargetPos = true;
        isFollow = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isFollow)
        {
            MoveAround();
        }
       
        if (isFollow)
        {
            MoveFollow(target);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.target = collision.gameObject;
            this.target.GetComponent<Anchor>().MoveBack(this.WeightRate);
            isFollow = true;
        }
    }

    public override void MoveFollow(GameObject target)
    {
        followDirect = target.GetComponent<Rigidbody2D>().velocity.normalized;

        transform.position = target.transform.GetChild(0).transform.position - new Vector3(followDirect.x, followDirect.y) * followDistant;

        if (target.GetComponent<Anchor>().CheckFinishBack())
        {
            target.transform.parent.transform.parent.GetComponent<Player>().AddMoney(this.Value);
            Destroy(this.gameObject);
            this.isFollow = false;
        }
    }

    private void MoveAround()
    {
        if (movingToTargetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            sr.flipX = false;
            if (transform.position == targetPosition)
            {
                movingToTargetPos = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            sr.flipX = true;
            if(transform.position == startPosition)
            {
                movingToTargetPos = true;
            }
        }
    }

}
