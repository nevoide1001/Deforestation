using UnityEngine;
using System;
using Deforestation.Recolectables;


namespace Deforestation.Interaction
{

    // Tipos de interacción del robot
    public enum MachineInteractionType
    {
        Door,
        Stairs,
        Machine,
        Delivery
    }



    // Interacción Con el robot (puerta, escalera, máquina)
    public class MachineInteraction : MonoBehaviour, IInteractable
    {


        #region Properties

        public MachineInteractionType Type => _type;

        #endregion



        #region Fields


        [SerializeField] protected MachineInteractionType _type;


        [SerializeField] protected Transform _target;


        [SerializeField] protected InteractableInfo _interactableInfo;

        [Header("Delivery")]

        [SerializeField] private int _requiredSuperCrystal = 20;
        [SerializeField] private int _requiredHyperCrystal = 5;



        #endregion



        #region Public Methods



        // Devuelve información para la UI
        public InteractableInfo GetInfo()
        {

            _interactableInfo.Type = _type.ToString();


            return _interactableInfo;
        }



        // Ejecuta la interacción
        public virtual void Interact()
        {

            if (_type == MachineInteractionType.Door)
            {

                transform.position = _target.position;

            }



            if (_type == MachineInteractionType.Stairs)
            {

                GameController.Instance.TeleportPlayer( _target.position);

            }



            if (_type == MachineInteractionType.Machine)
            {

                GameController.Instance.MachineMode(true);

            }


            if (_type == MachineInteractionType.Delivery)
            {
                Delivery();
            }

        }

        public void Delivery()
        {
            // Comprueba si hay suficientes recursos
            if (GameController.Instance.Inventory.HasResource(RecolectableType.SuperCrystal, _requiredSuperCrystal) &&
                GameController.Instance.Inventory.HasResource(RecolectableType.HyperCrystal, _requiredHyperCrystal))
            {
                GameController.Instance.Inventory.UseResource(RecolectableType.SuperCrystal, _requiredSuperCrystal);
                GameController.Instance.Inventory.UseResource(RecolectableType.HyperCrystal, _requiredHyperCrystal);

                GameController.Instance.GameStateController.Victory();
            }
        }


        #endregion

    }

}