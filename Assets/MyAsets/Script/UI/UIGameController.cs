using UnityEngine;
using TMPro;
using Deforestation.Recolectables;
using System;
using Deforestation.Interaction;
using UnityEngine.UI;
using UnityEngine.Audio;


namespace Deforestation.UI
{

    // Controlador principal de toda la UI del juego
    public class UIGameController : MonoBehaviour
    {

        #region Properties

        #endregion



        #region Fields


        // Acceso global al inventario (desde GameController)
        private Inventory _inventory => GameController.Instance.Inventory;


        // Acceso al sistema de interacción
        private InteractionSystem _interactionSystem => GameController.Instance.InteractionSystem;


        // Acceso al sistema Settings
        [Header("Settings")]

        [SerializeField] private AudioMixer _mixer;


        [SerializeField] private Button _settingsButton;


        [SerializeField] private GameObject _settingsPanel;


        [SerializeField] private Slider _musicSlider;


        [SerializeField] private Slider _fxSlider;


        // Acceso al Inventory
        [Header("Inventory")]

        [SerializeField] private TextMeshProUGUI _crystal1Text;


        [SerializeField] private TextMeshProUGUI _crystal2Text;

        [SerializeField] private TextMeshProUGUI _crystal3Text;


        // Panel de Interaccion
        [Header("Interacytion")]

        [SerializeField] private InteractionPanel _interactionPanel;


        // Barras de vida
        [Header("Live")]

        [SerializeField] private Slider _machineSlider;


        [SerializeField] private Slider _playerSlider;


        // Panel de entrega
        [Header("Delivery")]

        [SerializeField] private DeliveryPanel _deliveryPanel;



        // Estado del panel de settings
        private bool _settingsOn = false;


        #endregion



        #region Unity Callbacks


        void Start()
        {

            // Oculta settings al iniciar
            _settingsPanel.SetActive(false);



            // Suscripción a eventos del sistema
            _inventory.OnInventoryUpdated += UpdateUIInventory;
            _interactionSystem.OnShowInteraction += ShowInteraction;
            _interactionSystem.OnHideInteraction += HideInteraction;
            _interactionSystem.OnShowDelivery += ShowDelivery;
            _interactionSystem.OnHideDelivery += HideDelivery;



            // Botón settings
            _settingsButton.onClick.AddListener(SwitchSettings);


            // Sliders de audio
            _musicSlider.onValueChanged.AddListener(MusicVolumeChange);
            _fxSlider.onValueChanged.AddListener(FXVolumeChange);

        }



        #endregion



        #region Public Methods


        // Muestra texto de interacción
        public void ShowInteraction(string message)
        {
            _interactionPanel.Show(message);
        }


        // Oculta texto de interacción
        public void HideInteraction()
        {
            _interactionPanel.Hide();
        }



        // Actualiza barra de vida de máquina
        internal void UpdateMachineHealth(float value)
        {
            _machineSlider.value = value;
        }



        // Actualiza barra de vida del jugador
        internal void UpdatePlayerHealth(float value)
        {
            _playerSlider.value = value;
        }

        public void ShowDelivery()
        {
            _deliveryPanel.Show();
        }

        public void HideDelivery()
        {
            _deliveryPanel.Hide();
        }

        #endregion



        #region Private Methods

        // Abre o cierra el panel de configuración
        private void SwitchSettings()
        {
            _settingsOn = !_settingsOn;
            _settingsPanel.SetActive(_settingsOn);
        }

        // Actualiza UI del inventario
        private void UpdateUIInventory()
        {

            
            if (_inventory.InventoryStack.ContainsKey(RecolectableType.SuperCrystal))
                _crystal1Text.text = _inventory.InventoryStack[RecolectableType.SuperCrystal].ToString();
            else
                _crystal1Text.text = "0";


            
            if (_inventory.InventoryStack.ContainsKey(RecolectableType.HyperCrystal))
                _crystal2Text.text = _inventory.InventoryStack[RecolectableType.HyperCrystal].ToString();
            else
                _crystal2Text.text = "0";


            if (_inventory.InventoryStack.ContainsKey(RecolectableType.JumpCrystal))
                _crystal3Text.text = _inventory.InventoryStack[RecolectableType.JumpCrystal].ToString();
            else
                _crystal3Text.text = "0";
        }



        // Ajuste de volumen FX
        private void FXVolumeChange(float value)
        {
            _mixer.SetFloat("FXVolume", Mathf.Lerp(-60f, 0f, value));
        }



        // Ajuste de volumen música
        private void MusicVolumeChange(float value)
        {
            _mixer.SetFloat("MusicVolume", Mathf.Lerp(-60f, 0f, value));
        }



        #endregion

    }

}