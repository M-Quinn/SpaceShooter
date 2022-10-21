using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.GameManagement {
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject _pacifistText;

        public static Action<int> UpdateAmmo;

        void Start()
        {
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager == null)
                Debug.LogError($"{this.name} Couldn't find the GameManager");
            else
            {
                if (gameManager.GetGameType() == GameType.pacifist)
                {
                    _pacifistText.SetActive(true);
                }
            }
        }
    }

}
