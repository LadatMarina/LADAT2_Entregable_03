using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileBehaviour : MonoBehaviour
{
    public Color defaultColor;
    public SpriteRenderer _spriteRenderer;
    public GameManager gameManagerScript;
    public Vector3Int tilePositon;

    private void Awake()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        tilePositon = Vector3Int.RoundToInt(this.transform.position);
        defaultColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnMouseOver()
    {
        gameManagerScript.CheckDestination(tilePositon, 0, _spriteRenderer);
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
    }

    private void OnMouseDown()
    {
        gameManagerScript.CheckDestination(tilePositon, 1, _spriteRenderer);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            gameManagerScript.CheckDestination(tilePositon, 2, _spriteRenderer);
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }

}
