using UnityEngine;
using TMPro;
using System;


namespace Deforestation.UI
{

    // Controla el panel de texto de interacción
    public class InteractionPanel : MonoBehaviour
    {


        #region Properties

        #endregion



        #region Fields


        [SerializeField] private TextMeshProUGUI _textPanel;


        #endregion



        #region Unity Callbacks


        void Start()
        {

            // Oculta el panel al iniciar
            gameObject.SetActive(false);

        }


        #endregion



        #region Public Methods


        // Muestra mensaje de interacción
        public void Show(string message)
        {

            gameObject.SetActive(true);


            _textPanel.text = message;

        }



        // Oculta el panel
        internal void Hide()
        {

            gameObject.SetActive(false);

        }


        #endregion



        #region Private Methods

        #endregion


    }

}