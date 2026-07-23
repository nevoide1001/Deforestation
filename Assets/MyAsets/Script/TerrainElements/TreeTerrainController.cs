using UnityEngine;
using System;
using System.Collections.Generic;

namespace Deforestation
{
    // Controla los árboles del Terrain y su destrucción
    public class TreeTerrainController : MonoBehaviour
    {
        #region Properties

        public TreeInstance[] Trees => _trees;

        #endregion

        #region Fields

        [SerializeField] private Tree _treeDetectionPrefab;

        [SerializeField] private Tree _treePrefab;

        private TreeInstance[] _trees;

        Terrain _terrain;

        #endregion

        #region Unity Callbacks

        // Inicializa referencias y crea detectores
        void Start()
        {
            _terrain = Terrain.activeTerrain;
            _trees = _terrain.terrainData.treeInstances;

            InitializeTrees();
        }

        // Crea un detector para cada árbol del Terrain
        private void InitializeTrees()
        {
            for (int i = _trees.Length - 1; i >= 0; i--)
            {
                TreeInstance tree = _trees[i];

                Vector3 treeWorldPos = TreeToWorldPosition(tree);

                Tree treeDetector =
                    Instantiate( _treeDetectionPrefab, treeWorldPos, Quaternion.identity );

                treeDetector.transform.parent = transform;

                treeDetector.Index = i;
            }
        }

        // Convierte un árbol del Terrain en uno físico
        public GameObject DestroyTree(int i, Vector3 treeWorldPos)
        {
            Tree newTree = Instantiate( _treePrefab, treeWorldPos, Quaternion.identity );

            RemoveTreeFromTerrain(i);

            return newTree.gameObject;
        }

        // Restaura los árboles al salir del juego
        void OnDestroy()
        {
            _terrain.terrainData.treeInstances = _trees;
        }

        #endregion

        #region Public Methods

        // Convierte coordenadas del Terrain a mundo
        public Vector3 TreeToWorldPosition(TreeInstance tree)
        {
            return Vector3.Scale( tree.position, _terrain.terrainData.size ) + _terrain.transform.position;
        }

        // Elimina un árbol del Terrain
        public void RemoveTreeFromTerrain(int index)
        {
            // TODO: Actualizar índices de los detectores

            List<TreeInstance> trees = new List<TreeInstance>( _terrain.terrainData.treeInstances );

            trees.RemoveAt(index);

            _terrain.terrainData.treeInstances = trees.ToArray();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
