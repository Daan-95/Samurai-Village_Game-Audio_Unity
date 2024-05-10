using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    private enum CURRENT_TERRAIN { GRASS, Water, DIRT, STONE, SAND, STONE_TUNNEL, WOOD };


    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;

    private FMOD.Studio.EventInstance landing;

    private void Update()
    {
        DetermineTerrain();

    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, Vector3.down, 0.25f);

        foreach (RaycastHit rayhit in hit)
        {
            switch (LayerMask.LayerToName(rayhit.transform.gameObject.layer))
            {
                case "GRASS":
                    currentTerrain = CURRENT_TERRAIN.GRASS;
                    break;

                case "Water":
                    currentTerrain = CURRENT_TERRAIN.Water;
                    break;

                case "DIRT":
                    currentTerrain = CURRENT_TERRAIN.DIRT;
                    break;

                case "STONE":
                    currentTerrain = CURRENT_TERRAIN.STONE;
                    break;

                case "SAND":
                    currentTerrain = CURRENT_TERRAIN.SAND;
                    break;

                case "STONE_TUNNEL":
                    currentTerrain = CURRENT_TERRAIN.STONE_TUNNEL;
                    break;
                case "WOOD":
                    currentTerrain = CURRENT_TERRAIN.WOOD;
                    break;
            }
        }
    }

    private void SelectAndPlayLanding()
    {
        int terrainIndex = (int)currentTerrain;
        PlayLanding(terrainIndex);
    }

    private void PlayLanding(int terrain)
    {
        landing = FMODUnity.RuntimeManager.CreateInstance("event:/Land");
        landing.setParameterByName("Terrain", terrain);
        landing.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        landing.start();
        landing.release();
    }
}