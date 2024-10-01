using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    private int xp = 0;
    private bool _isActive = false;

    public int Level => _level;
    public bool IsActive => _isActive;

    public void PlayerActive(bool active) => _isActive = active;

    public void AddXp(int value)
    {
        xp += value;
        if (xp >= _level * (_level * 5)) LevelUp();
    }

    public void LevelUp() 
    {
        _level++;

        GameManager.Instance.PlayerLevelUP();
    } 

}


