using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deforestation.UI
{
    public class UISettingsButtons : MonoBehaviour
    {
        #region Public Methods

        // Reinicia la escena actual
        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Cierra el juego
        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}
