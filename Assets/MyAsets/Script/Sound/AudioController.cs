using UnityEngine;
using DG.Tweening;
using System;

namespace Deforestation.Audio
{
    public class AudioController : MonoBehaviour
    {
        const float MAX_VOLUME = 0.1f;

        #region Fields

        [Header("FX")]

        [SerializeField] private AudioSource _steps;

        [SerializeField] private AudioSource _machineOn;

        [SerializeField] private AudioSource _machineOff;

        [SerializeField] private AudioSource _shoot;


        [Space(10)]


        [Header("Music")]

        [SerializeField] private AudioSource _musicMachine;

        [SerializeField] private AudioSource _musicHuman;

        #endregion

        #region Properties
        #endregion

        #region Unity Callbacks

        // Se suscribe a los eventos principales del juego
        private void Awake()
        {
            GameController.Instance.OnMachineModeChange += SetMachineMusicState;

            GameController.Instance.MachineController.OnMachineDriveChange += SetMachineDriveEffect;

            GameController.Instance.MachineController.WeaponController.OnMachineShoot += ShootFX;
        }

        // Comienza la música del jugador
        private void Start()
        {
            _musicHuman.Play();
        }

        // Reproduce los pasos según el movimiento
        private void Update()
        {

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (!_steps.isPlaying) _steps.Play();
            }
            else
            {
                if (_steps.isPlaying) _steps.Stop();
            }
        }

        #endregion

        #region Private Methods

        // Cambia la música según el modo de juego
        private void SetMachineMusicState(bool machineMode)
        {
            if (machineMode)
            {
                _musicHuman.DOFade(0, 3);

                _musicMachine.DOFade(MAX_VOLUME, 3);

                _musicMachine.Play();
            }
            else
            {
                _musicHuman.DOFade(MAX_VOLUME, 3);

                _musicMachine.DOFade(0, 3);
            }
        }

        // Sonido al entrar o salir de la máquina
        private void SetMachineDriveEffect(bool startDriving)
        {
            if (startDriving)
                _machineOn.Play();
            else
                _machineOff.Play();
        }

        // Sonido del disparo
        private void ShootFX()
        {
            _shoot.Play();
        }

        #endregion

        #region Public Methods
        #endregion
    }
}