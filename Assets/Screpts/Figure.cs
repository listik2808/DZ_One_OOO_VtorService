using UnityEngine;

public class Figure : MonoBehaviour
{
    private GameCell _cell;

    public GameCell Cell => _cell;

    public void SetCell(GameCell cell)
    {
        _cell = cell;
    }
}
