using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameType _gameType;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetGameType(GameType x) {
        _gameType = x;
    }
    public GameType GetGameType() {
        return _gameType;
    }
}

public enum GameType { normal, pacifist}
