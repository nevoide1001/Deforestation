using UnityEngine;
using System;
using System.Collections.Generic;

namespace Deforestation.Recolectables
{
    public class Inventory : MonoBehaviour
    {
        #region Properties

        // Guardar el inventario de recursos recolectables por tipo y cantidad
        public Dictionary<RecolectableType, int> InventoryStack = new Dictionary<RecolectableType, int>();

        // Evento que se dispara cuando el inventario se actualiza
        public Action OnInventoryUpdated;

        #endregion


        #region Fields

        #endregion


        #region Unity Callbacks

        #endregion


        #region Public Methods

        // Añade recursos al inventario
        public void AddRecolectable(RecolectableType type, int count)
        {
            if (InventoryStack.ContainsKey(type))
                InventoryStack[type] += count;
            else
                InventoryStack.Add(type, count);

            // Actualiza la UI
            OnInventoryUpdated?.Invoke();
        }


        // Gestiona el uso de recursos del inventario
        public bool UseResource(RecolectableType type, int count = 1)
        {
            // Comprueba si hay suficiente cantidad
            if (HasResource(type, count))
            {
                InventoryStack[type] -= count;

                // Actualiza la UI
                OnInventoryUpdated?.Invoke();

                return true;
            }

            return false;
        }


        // Comprueba si existen recursos suficientes
        public bool HasResource(RecolectableType type, int count = 1)
        {
            // Revisa si existe y tiene cantidad suficiente
            if (InventoryStack.ContainsKey(type) && InventoryStack[type] >= count)
                return true;

            return false;
        }


        #endregion


        #region Private Methods

        #endregion
    }
}
