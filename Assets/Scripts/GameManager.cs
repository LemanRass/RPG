using UI.Game;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake() => instance = this;

    public PlayerUnit playerUnit;
}