using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private float SpeedNPC;
    [SerializeField] private Transform[] PatrollPoints;
    [SerializeField] private float MiniDistance;

    private int RandomNumber;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        RandomNumber = Random.Range(0, PatrollPoints.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Flip();

    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, PatrollPoints[RandomNumber].position, SpeedNPC * Time.deltaTime);

        if(Vector2.Distance(transform.position, PatrollPoints[RandomNumber].position) < MiniDistance)
        {
            RandomNumber = Random.Range(0, PatrollPoints.Length);
            Flip();
        }
    }

    //flip sprite render when hits patroll point
    private void Flip()
    {
        if (transform.position.x < PatrollPoints[RandomNumber].position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true ;
        }
    }


}
