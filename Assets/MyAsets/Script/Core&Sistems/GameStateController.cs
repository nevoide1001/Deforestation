using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Deforestation.StateController
{
    public class GameStateController : MonoBehaviour
    {

        #region Fields

        [Header("UI")]
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _victoryPanel;
        
        #endregion

        #region Unity Callbacks

        // Start is called before the first frame update
        void Start()
        {

            // Ocultar paneles al iniciar
            _gameOverPanel.SetActive(false);
            _victoryPanel.SetActive(false);

            // Escuchar la muerte del jugador y de la máquina
            GameController.Instance.PlayerHealth.OnDeath += GameOver;

            GameController.Instance.MachineController.HealthSystem.OnDeath += GameOver;

        }

        #endregion

        #region Public Methods

        public void GameOver()
        {
            _gameOverPanel.SetActive(true);
            GameController.Instance.Player.gameObject.SetActive(false);
            GameController.Instance.MachineController.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Victory()
        {
            _victoryPanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        #endregion
    }
}
