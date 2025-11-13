using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeManager : MonoBehaviour
{
    public Button[] cells;
    private string currentPlayer = "X";

    void Start()
    {
        foreach(Button cell in cells){
            cell.onClick.AddListener(() => MakeMove(cell));
        }
    }

    void MakeMove(Button cell){
        if(cell.GetComponentInChildren<TextMeshProUGUI>().text == ""){
            cell.GetComponentInChildren<TextMeshProUGUI>().text = currentPlayer;
            currentPlayer = (currentPlayer == "X") ? "O" : "X";
        }
    }
}