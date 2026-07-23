using Cinemachine;
using Deforestation.Interaction;
using Deforestation.Machine;
using Deforestation.Recolectables;
using Deforestation.StateController;
using Deforestation.UI;
using System;
using Unity.VisualScripting;
using UnityEngine;


namespace Deforestation
{

    public class GameController : Singleton<GameController>
    {

        #region Properties


        // Acceso a la máquina principal
        public MachineController MachineController => _machine;

        public CharacterController Player => _player;
        public HealthSystem PlayerHealth => _playerHealth;


        // Acceso al inventario global
        public Inventory Inventory => _inventory;


        // Acceso al sistema de interacción
        public InteractionSystem InteractionSystem => _interactionSystem;


        // Controlador del terreno (árboles / mundo)
        public TreeTerrainController TerrainController => _terrainController;


        // Cámara principal del juego
        public Camera MainCamera;



        // Evento al cambiar modo máquina
        public Action<bool> OnMachineModeChange;

        // Controlador de estado del juego (Game Over / Victory)
        public GameStateController GameStateController => _gameStateController;


        // Estado actual: modo máquina activado/desactivado
        public bool MachineModeOn
        {
            get => _machineModeOn;

            private set
            {
                _machineModeOn = value;

                OnMachineModeChange?.Invoke(_machineModeOn);
            }
        }


        #endregion



        #region Fields

        // Propiedades del jugador, máquina, cámara y UI
        [Header("Player")]

        [SerializeField] protected CharacterController _player;


        [SerializeField] protected HealthSystem _playerHealth;


        [SerializeField] protected Inventory _inventory;


        [SerializeField] protected InteractionSystem _interactionSystem;



        [Header("Camera")]

        /*[SerializeField] protected CinemachineVirtualCamera _virtualCamera;


        [SerializeField] protected Transform _playerFollow;


        [SerializeField] protected Transform _machineFollow;*/

        [SerializeField] private Camera _playerCamera;
        [SerializeField] private Camera _machineCamera;



        [Header("Machine")]

        [SerializeField] protected MachineController _machine;



        [Header("UI")]

        [SerializeField] protected UIGameController _uiController;



        [Header("Trees Terrain")]

        [SerializeField] protected TreeTerrainController _terrainController;

        [Header("Game State")]

        [SerializeField] protected GameStateController _gameStateController;



        private bool _machineModeOn;


        #endregion



        #region Unity Callbacks


        void Start()
        {
            // Estado inicial del juego
            MachineModeOn = false;

            MainCamera = _playerCamera;

            //_playerCamera.enabled = true;
            //_machineCamera.enabled = false;

            //_playerCamera.gameObject.SetActive(true);
            //_machineCamera.gameObject.SetActive(false);

            Debug.Log("Antes: " + _machine.transform.eulerAngles);


            Debug.Log("Machine: " + _machine.transform.eulerAngles);
            Debug.Log("Bone009: " + GameObject.Find("Bone.009").transform.eulerAngles);
            Debug.Log("Tower: " + _machine.WeaponController.transform.eulerAngles);
            Debug.Log("TowerWeapon: " + _machine.WeaponController.TowerWeapon.eulerAngles);

        }


        void Update()
        {
            
        }


        #endregion



        #region Public Methods


        // Teleporta al jugador a una posición concreta
        public void TeleportPlayer(Vector3 target)
        {
            _player.enabled = false;
            _player.transform.position = target;
            _player.enabled = true;
        }


        // Cambia entre modo jugador y modo máquina
        internal void MachineMode(bool machineMode)
        {

            MachineModeOn = machineMode;



            _player.gameObject.SetActive(!machineMode);
            _player.enabled = !machineMode;



            // CAMBIO DE CONTROL
            if (machineMode)
            {
                Debug.Log("Machine: " + _machine.transform.eulerAngles);
                Debug.Log("Bone009: " + GameObject.Find("Bone.009").transform.eulerAngles);
                Debug.Log("Tower: " + _machine.WeaponController.transform.eulerAngles);
                Debug.Log("TowerWeapon: " + _machine.WeaponController.TowerWeapon.eulerAngles);

                if (Inventory.HasResource(RecolectableType.HyperCrystal))
                    _machine.StartDriving(machineMode);
                Debug.Log("Tras StartDriving: " + _machine.transform.eulerAngles);



                _machine.transform.position = _player.transform.position;
                Debug.Log("Tras mover posición: " + _machine.transform.eulerAngles);

                _machineCamera.enabled = true;
                _playerCamera.enabled = false;

                MainCamera = _machineCamera;

                _uiController.HideInteraction();


                Cursor.lockState = CursorLockMode.None;


                _machine.enabled = true;
                _machine.WeaponController.enabled = true;
                _machine.GetComponent<MachineMovement>().enabled = true;

                Debug.Log("Final MachineMode: " + _machine.transform.eulerAngles);

            }
            else
            {

                _machine.enabled = false;
                _machine.WeaponController.enabled = false;
                _machine.GetComponent<MachineMovement>().enabled = false;



                _player.transform.parent = null;



                _playerCamera.enabled = true;
                _machineCamera.enabled = false;

                MainCamera = _playerCamera;



                Cursor.lockState = CursorLockMode.Locked;
            }



            Cursor.visible = machineMode;
        }


        #endregion



        #region Private Methods

        #endregion

    }

}