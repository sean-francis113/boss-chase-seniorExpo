using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class biome{

    private static BIOME curBiome;
    private static int numOfBiomes = 3;

    public static BIOME GetCurrentBiome()
    {

        return curBiome;

    }

    public static int GetNumberofBiomes()
    {

        return numOfBiomes;

    }

    public static void SetCurrentBiome(BIOME set)
    {

        curBiome = set;

    }

}

public enum BIOME
{

    TEST,
    GRASS,
    METAL,
    HEAT

}
