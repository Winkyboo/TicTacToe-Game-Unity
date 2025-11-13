using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public Button[] cells; 
    public TextMeshProUGUI resultText; 
    public GameObject winLine; 
    private string currentPlayer = "X";
    
    void Start()
    {
    winLine.SetActive(false); 
    resultText.gameObject.SetActive(false); 

    currentPlayer = "X"; 

    foreach (Button cell in cells)
        {
        cell.onClick.RemoveAllListeners(); 
        cell.onClick.AddListener(() => MakeMove(cell)); 
        cell.interactable = true; 
        cell.GetComponentInChildren<TextMeshProUGUI>().text = ""; 
        }
    }

    void MakeMove(Button cell)
    {
        TextMeshProUGUI cellText = cell.GetComponentInChildren<TextMeshProUGUI>();

        if (cellText.text == "")
        {
            cellText.text = currentPlayer;
            if (CheckWin(currentPlayer))
            {
                resultText.gameObject.SetActive(true);
                resultText.text = currentPlayer + " Wins!";
                DisableBoard();
            }
            else if (CheckDraw())
            {
                resultText.gameObject.SetActive(true);
                resultText.text = "Draw!";
            }
            else
            {
                currentPlayer = (currentPlayer == "X") ? "O" : "X"; 
            }
        }
    }

    bool CheckWin(string player)
    {
        int[,] winConditions = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, 
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, 
            {0, 4, 8}, {2, 4, 6}             
        };

        for(int i = 0; i < winConditions.GetLength(0); i++)
        {
            int index1 = winConditions[i, 0];
            int index2 = winConditions[i, 1];
            int index3 = winConditions[i, 2];
            if (cells[index1].GetComponentInChildren<TextMeshProUGUI>().text == player &&
                cells[index2].GetComponentInChildren<TextMeshProUGUI>().text == player &&
                cells[index3].GetComponentInChildren<TextMeshProUGUI>().text == player)
            {
                DrawWinLine(new int[] {index1, index2, index3});
                return true;
            }
        }
        return false;
    }

    bool CheckDraw()
    {
        foreach (Button cell in cells)
        {
            if (cell.GetComponentInChildren<TextMeshProUGUI>().text == "")
                return false;
        }
        return true;
    }

    void DisableBoard()
    {
        foreach (Button cell in cells)
        {
            cell.interactable = false;
        }
    }

    void DrawWinLine(int[] winPos)
    {
        LineRenderer line = winLine.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, cells[winPos[0]].transform.position);
        line.SetPosition(1, cells[winPos[2]].transform.position);
        winLine.SetActive(true);
    }
}