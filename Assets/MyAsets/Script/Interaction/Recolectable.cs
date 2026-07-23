using UnityEngine;
using System;
using Deforestation.Interaction;

namespace Deforestation.Recolectables
{

    // Tipos de recursos que pueden recogerse
    public enum RecolectableType
    {
        //Cambiar mas adelante los nombres y añadir un nuevo tipo de recurso
        SuperCrystal,
        HyperCrystal,
        JumpCrystal
    }


    public class Recolectable : MonoBehaviour, IInteractable
    {

        #region Properties

        // Cantidad que entrega al recogerlo
        [field: SerializeField] public int Count { get; set; }

        // Tipo de recurso
        [field: SerializeField] public RecolectableType Type { get; set; }


        #endregion


        #region Fields

        [SerializeField] private InteractableInfo _interactableInfo;


        #endregion


        #region Unity Callbacks


        void Start()
        {

        }


        void Update()
        {

        }


        #endregion


        #region Public Methods


        public InteractableInfo GetInfo()
        {
            _interactableInfo.Type = Type.ToString();

            return _interactableInfo;
        }


        // Destruye el objeto al interactuar con él
        public void Interact()
        {
            Destroy(gameObject);
        }


        #endregion


        #region Private Methods

        #endregion
    }
}