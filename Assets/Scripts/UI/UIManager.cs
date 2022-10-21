using System;
using TMPro;
using UnityEngine;

namespace Dev.MikeQ.SpaceShooter.GameManagement
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject _pacifistText;
        [SerializeField] TMP_Text _ammoText;

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
        private void OnEnable()
        {
            UpdateAmmo += HandleUpdateAmmo;
        }
        private void OnDisable()
        {
            UpdateAmmo -= HandleUpdateAmmo;
        }

        private void HandleUpdateAmmo(int ammo) {
            _ammoText.text = ammo.ToString();
            if (ammo > 5) { }
            else if (ammo > 0)
                _ammoText.color = Color.yellow;
            else
                _ammoText.color = Color.red;
        }
    }

}
