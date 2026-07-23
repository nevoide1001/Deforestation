using UnityEngine;
using System;
using Deforestation.Machine.Weapon;


namespace Deforestation.Machine
{

    // Controlador principal de la máquina
    [RequireComponent(typeof(HealthSystem))]
    public class MachineController : MonoBehaviour
    {


        #region Properties


        public HealthSystem HealthSystem => _health;


        public WeaponController WeaponController;


        public Action<bool> OnMachineDriveChange;


        #endregion



        #region Fields


        private HealthSystem _health;


        private MachineMovement _movement;


        private Animator _anim;


        #endregion



        #region Unity Callbacks


        // Obtiene componentes necesarios
        private void Awake()
        {

            _health = GetComponent<HealthSystem>();

            _movement = GetComponent<MachineMovement>();

            _anim = GetComponent<Animator>();

        }



        // Configuración inicial
        void Start()
        {

            _movement.enabled = false;

        }



        void Update()
        {

            // Salir del control de máquina
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                StopDriving();
            }

        }


        #endregion



        #region Public Methods


        // Sale del modo máquina
        public void StopDriving()
        {

            GameController.Instance.MachineMode(false);


            StopMoving();


            OnMachineDriveChange?.Invoke(false);

        }



        // Activa conducción
        public void StartDriving(bool machineMode)
        {

            enabled = machineMode;


            _movement.enabled = machineMode;


            _anim.SetTrigger("WakeUp");


            _anim.SetBool("Move", machineMode);



            OnMachineDriveChange?.Invoke(true);

        }



        // Detiene movimiento
        public void StopMoving()
        {

            _movement.enabled = false;


            _anim.SetBool("Move", false);

        }


        #endregion



        #region Private Methods

        #endregion


    }

}