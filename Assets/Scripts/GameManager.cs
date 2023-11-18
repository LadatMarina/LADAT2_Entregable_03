using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region CHESSBOARD CREATION VARIABLES
    private int value = 8;
    public Sprite tileSprite;
    public Sprite queenSprite;
    public Sprite horseSprite;
    private GameObject queenGO;
    private GameObject horseGO;

    bool chessboardIsCreated;

    private int blackValue;
    private int whiteValue;

    public GameObject tilesParent;
    public GameObject mainMenuPanel;
    #endregion

    /*SCENE BUILT
     * Main Menu = 0
     * Queen Scene = 1
     * Horse Scene = 2
     */

    private Vector3Int currentPosition = new Vector3Int(5, 5, 0);

    private void Start()
    {
        if((SceneManager.GetActiveScene().buildIndex == 1) || (SceneManager.GetActiveScene().buildIndex == 2))
        {
            CreateChessBoard();

            CreateChessPiece();
        }
    }

    private void CreateChessBoard()
    {
        if (value != 0 && chessboardIsCreated == false) //si hi ha un valor de tampany per es chessboard...
        {
            //set camera in the center of the chessBoard
            if (value <= 8)
            {
                Camera.main.transform.position = new Vector3(value / 2f, value / 2f, -10);
            }
            else
            {
                Camera.main.transform.position = new Vector3(value / 2f, value / 2f, -30);
            }

            for (int y = value; y > 0; y--)
            {
                //if it's pair start with black tile, else, with 
                if (y % 2 == 0)
                {
                    blackValue = value;
                    whiteValue = value - 1;
                }
                else
                {
                    blackValue = value - 1;
                    whiteValue = value;
                }

                for (int x = blackValue; x > 0; x -= 2)
                {
                    //BLACK TILE
                    GameObject blackTile = new GameObject("black Tile");
                    SpriteRenderer blackSpriteRenderer = blackTile.AddComponent<SpriteRenderer>();
                    blackSpriteRenderer.sprite = tileSprite;
                    blackSpriteRenderer.color = Color.black;
                    blackTile.AddComponent<BoxCollider2D>();
                    blackTile.AddComponent<TileBehaviour>();

                    //set tile position
                    blackTile.transform.position = new Vector3Int(x, y, 0);

                    //set the GO to an empty parent to organize
                    blackTile.transform.SetParent(tilesParent.transform);
                }
                for (int x = whiteValue; x > 0; x -= 2)
                {
                    //WHITE TILE
                    GameObject whiteTile = new GameObject("white Tile");
                    SpriteRenderer whiteSpriteRenderer = whiteTile.AddComponent<SpriteRenderer>();
                    whiteSpriteRenderer.sprite = tileSprite;
                    whiteTile.AddComponent<BoxCollider2D>();
                    whiteTile.AddComponent<TileBehaviour>();

                    //set tile position
                    whiteTile.transform.position = new Vector3Int(x, y, 0);

                    //set the GO to an empty parent to organize
                    whiteTile.transform.SetParent(tilesParent.transform);
                }
                chessboardIsCreated = true;
            }
        }
    }
    private void CreateChessPiece()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                break;
            case 1:
                CreateQueen();
                break;
            case 2:
                CreateHorse();
                break;

        }
    }
    private void CreateQueen()
    {
        queenGO = new GameObject("Queen");
        SpriteRenderer queenSpriteRenderer = queenGO.AddComponent<SpriteRenderer>();
        queenSpriteRenderer.sprite = queenSprite;
        queenSpriteRenderer.sortingOrder = 10;
        queenGO.transform.position = currentPosition;

    }
    private void CreateHorse()
    {
        horseGO = new GameObject("Horse");
        SpriteRenderer horseSpriteRenderer = horseGO.AddComponent<SpriteRenderer>();
        horseSpriteRenderer.sprite = horseSprite;
        horseSpriteRenderer.sortingOrder = 10;
        horseGO.transform.position = currentPosition;
    }


    /*mouseIsOn és una variable que determina si sa funció ha estat cridada des de OnMouseDown o des de OnMouseOver
     * mouseIsOn = 0 --> cridada des de OnMouseOver
     * mouseIsOn = 1 --> cridada des de OnMouseDown
     * mouseIsOn = 2 --> cridada des de Key Y
     */

    public void CheckDestination(Vector3Int destination, int mouseIsOn, SpriteRenderer _spriteRenderer)
    {
        Vector3Int DCvector = destination - currentPosition;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                break;

            case 1:
                if (((Mathf.Abs(DCvector.x) == Mathf.Abs(DCvector.y)) || (destination.y == currentPosition.y) || (destination.x == currentPosition.x)))
                {
                    if (mouseIsOn == 0)
                    {
                        _spriteRenderer.color = Color.green;
                    }
                    else if (mouseIsOn == 1)
                    {
                        MoveChessPiece(destination, queenGO);
                    }
                    else if (mouseIsOn == 2)
                    {
                        _spriteRenderer.color = Color.green;
                    }
                }
                break;

            case 2:
                if (((Mathf.Abs(DCvector.x)== 1) && (Mathf.Abs(DCvector.y) == 2)) || ((Mathf.Abs(DCvector.x) == 2) && (Mathf.Abs(DCvector.y) == 1)))
                {
                    if (mouseIsOn == 0)
                    {
                        _spriteRenderer.color = Color.green;
                    }
                    else if (mouseIsOn == 1)
                    {
                        MoveChessPiece(destination, horseGO);
                    }
                    else if(mouseIsOn == 2)
                    {
                        _spriteRenderer.color = Color.green;
                    }
                }
                else 
                {
                    if (mouseIsOn != 2)
                    {
                        _spriteRenderer.color = Color.red;
                    }
                }
                break;
        }
    }

    public void MoveChessPiece(Vector3Int destination, GameObject chessPiece)
    {
        chessPiece.transform.position = destination;
        currentPosition = destination;
    }

    public void PlayButtonMM()
    {
        mainMenuPanel.SetActive(false);
    }

    public void ChangeSceneTo(int i)
    {
        SceneManager.LoadScene(i);
    }
}