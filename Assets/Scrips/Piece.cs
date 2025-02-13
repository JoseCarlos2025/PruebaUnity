using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    public bool hasBomb = false;

    void OnMouseDown()
    {
        if (hasBomb)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("Game Over! Hiciste clic en una bomba.");
        }
        else
        {
            int bombCount = Generator.instance.CheckBombsAround(x, y);
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = bombCount.ToString();
            Debug.Log("Número de bombas cercanas: " + bombCount);
        }
    }
}
