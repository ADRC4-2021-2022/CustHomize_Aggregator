//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//                                                                            //Instances of tile should reference Voxel?

//class Tile
//{
//    public Vector3Int index;                                                 //Link this to Index of Prefabs?
//    public Grid grid;                                                        //Link this to voxel grid?
//    public List<TilePattern> possiblePatterns;
//}

//class TilePattern
//{
//    int TileIndex; //every tile index needs to be unique
//    GameObject TilePrefab;
//    List<Connection> Connections = new List<Connection>(6); //one connection for every axis direction
//}                                                           //Link this to PatternA,B,C etc?

//public class Connection
//{
//    //If a tile has a certain connection, it can connect to these possible Neighbours
//    List<Tile> possibleNeighbours;
//}

//public class WFC
//{
//    //I DON'T THINK WE HAVE THIS YET
//    void CreateGrid()
//    {

//    }

//    //IS THIS THE SELECT THE NEXT TILE?
//    public IEnumerator RunViaEnumerator(int seed, int limit, Action<bool> resultCallback, Action<bool[][]> iterationCallback)           // This should run through list and create possible       (1)        
//    {                                                                                                                                   // connection points within the system                    (2)
//        if (wave == null) Init();

//        Clear();
//        if (seed != 0)
//        {
//            random = new Random(seed);
//        }
//        else
//        {
//            random = new Random();
//        }


//        for (int l = 0; l < limit || limit == 0; l++)
//        {
//            bool? result = Observe();
//            if (result != null)
//            {
//                resultCallback(result.Value);
//                break;
//            }
//            Debug.Log("Propagate, iteration: " + l);
//            Propagate();
//            iterationCallback(wave);
//            yield return null;
//        }
//    }

//    private void Init()
//    {
//        wave = new bool[FMX * FMY * FMZ][];
//        compatible = new int[wave.Length][][];
//        for (int i = 0; i < wave.Length; i++)
//        {
//            wave[i] = new bool[T];
//            compatible[i] = new int[T][];
//            for (int t = 0; t < T; t++) compatible[i][t] = new int[DIRECTIONS_AMOUNT];
//        }

//        weightLogWeights = new double[T];
//        sumOfWeights = 0;
//        sumOfWeightLogWeights = 0;

//        for (int t = 0; t < T; t++)
//        {
//            weightLogWeights[t] = weights[t] * Math.Log(weights[t]);
//            sumOfWeights += weights[t];
//            sumOfWeightLogWeights += weightLogWeights[t];
//        }

//        startingEntropy = Math.Log(sumOfWeights) - sumOfWeightLogWeights / sumOfWeights;

//        sumsOfOnes = new int[FMX * FMY * FMZ];
//        sumsOfWeights = new double[FMX * FMY * FMZ];
//        sumsOfWeightLogWeights = new double[FMX * FMY * FMZ];
//        entropies = new double[FMX * FMY * FMZ];

//        stack = new Tuple<int, int>[wave.Length * T];
//        stacksize = 0;
//    }

//    bool Observe()
//    {
//        double min = 1E+3;
//        int argmin = -1;

//        for (int i = 0; i < wave.Length; i++)
//        {
//            int x, y, z;
//            To3D(i, out x, out y, out z);
//            if (OnBoundary(x, y, z)) continue;

//            int amount = sumsOfOnes[i];
//            if (amount == 0) return false;

//            double entropy = entropies[i];
//            if (amount > 1 && entropy <= min)
//            {
//                double noise = 1E-6 * random.NextDouble();
//                if (entropy + noise < min)
//                {
//                    min = entropy + noise;
//                    argmin = i;
//                }
//            }
//        }

//        if (argmin == -1)
//        {
//            observed = new int[FMX * FMY * FMZ];
//            for (int i = 0; i < wave.Length; i++)
//            {
//                for (int t = 0; t < T; t++)
//                {
//                    if (wave[i][t])
//                    {
//                        observed[i] = t; break;
//                    }
//                }
//            }
//            return true;
//        }

//        double[] distribution = new double[T];
//        for (int t = 0; t < T; t++) distribution[t] = wave[argmin][t] ? weights[t] : 0;
//        int r = distribution.Random(random.NextDouble());

//        bool[] w = wave[argmin];
//        for (int t = 0; t < T; t++) if (w[t] != (t == r)) Ban(argmin, t);

//        return null;
//    }

//    void PropogateGrid(List<Tile> lastAdjustedTiles) //make sure you don't get infinite loops
//    {
//        while (stacksize > 0)
//        {
//            var e1 = stack[stacksize - 1];
//            stacksize--;

//            int i1 = e1.Item1;
//            int x1, y1, z1;
//            To3D(i1, out x1, out y1, out z1);
//            bool[] w1 = wave[i1];

//            for (int d = 0; d < DIRECTIONS_AMOUNT; d++)
//            {
//                int dx = DX[d], dy = DY[d], dz = DZ[d];
//                int x2 = x1 + dx, y2 = y1 + dy, z2 = z1 + dz;
//                if (OnBoundary(x2, y2, z2)) continue;

//                if (x2 < 0) x2 += FMX;
//                else if (x2 >= FMX) x2 -= FMX;
//                if (y2 < 0) y2 += FMY;
//                else if (y2 >= FMY) y2 -= FMY;
//                if (z2 < 0) z2 += FMY;
//                else if (z2 >= FMZ) z2 -= FMZ;

//                int i2 = To1D(x2, y2, z2);
//                int[] p = propagator[d][e1.Item2];
//                int[][] compat = compatible[i2];

//                for (int l = 0; l < p.Length; l++)
//                {
//                    int t2 = p[l];
//                    int[] comp = compat[t2];

//                    comp[d]--;
//                    if (comp[d] == 0)
//                    {
//                        Ban(i2, t2);
//                    }
//                }
//            }
//        }

//    void Ban(int i, int t)
//    {
//        wave[i][t] = false;

//        int[] comp = compatible[i][t];
//        for (int d = 0; d < DIRECTIONS_AMOUNT; d++) comp[d] = 0;
//        stack[stacksize] = new Tuple<int, int>(i, t);
//        stacksize++;

//        double sum = sumsOfWeights[i];
//        entropies[i] += sumsOfWeightLogWeights[i] / sum - Math.Log(sum);

//        sumsOfOnes[i] -= 1;
//        sumsOfWeights[i] -= weights[t];
//        sumsOfWeightLogWeights[i] -= weightLogWeights[t];

//        sum = sumsOfWeights[i];
//        entropies[i] -= sumsOfWeightLogWeights[i] / sum - Math.Log(sum);
//    }
//    protected virtual void Clear()                                       // I am actually not sure what this part does but it is listed in the   (1) 
//    {                                                                                                                     // same section as the propagate function so I included it here for now (2)
//        for (int i = 0; i < wave.Length; i++)
//        {
//            for (int t = 0; t < T; t++)
//            {
//                wave[i][t] = true;
//                for (int d = 0; d < DIRECTIONS_AMOUNT; d++) compatible[i][t][d] = propagator[opposite[d]][t].Length;
//            }

//            sumsOfOnes[i] = weights.Length;
//            sumsOfWeights[i] = sumOfWeights;
//            sumsOfWeightLogWeights[i] = sumOfWeightLogWeights;
//            entropies[i] = startingEntropy;
//        }
//    }

//    public Dictionary<int, GameObject> GOPatternPrefabs
//    {
//        get
//        {
//            if (_goPatternPrefabs == null)
//            {
//                _goPatternPrefabs = new Dictionary<int, GameObject>();
//                _goPatternPrefabs.Add(0, Resources.Load("Prefabs/PrefabPatternA") as GameObject);
//                _goPatternPrefabs.Add(1, Resources.Load("Prefabs/PrefabPatternB") as GameObject);
//                _goPatternPrefabs.Add(2, Resources.Load("Prefabs/PrefabPatternC") as GameObject);

//            }
//        return _goPatternPrefabs;
//        }
//    }


//    void BackTrack()
//    {
//        //When a tile has no possible options, return to the last operation that reduced the options in the tile
//        //Remove the selected option that made the grid invalid
//        //Select another option.
//        //How many backTracks steps do you store? --- IT IS PRETTY LIGHT RIGHT NOW, SO SHOULDN'T BE A PROBLEM TO STORE ALL
//    }   //Need to Figure this out, Maybe Initial code does not have to back track
//}

//public class WFCGrid
//{
//    void CalculateEntropyAndPatternIdAt(int x, int y, int z, out double entropy, out int? patternId)
//    {
//        int indexInWave = To1D(x, y, z);
//        int amount = 0;
//        patternId = null;
//        var possiblePatternsFlags = wave[indexInWave];
//        for (int t = 0; t < T; t++)
//        {
//            if (possiblePatternsFlags[t])
//            {
//                amount += 1;
//                patternId = t;
//            }
//        }

//        if (amount != 1)
//        {
//            patternId = null;
//        }

//        entropy = entropies[indexInWave] / startingEntropy;
//    }

//    protected int To1D(int x, int y, int z)
//    {
//        return z * FMX * FMY + y * FMX + x;
//    }

//    protected void To3D(int index, out int x, out int y, out int z)
//    {
//        z = index / (FMX * FMY);
//        index -= z * FMX * FMY;
//        y = index / FMX;
//        x = index % FMX;
//    }

//    protected abstract bool OnBoundary(int x, int y, int z);

//    public abstract CellState GetCellStateAt(int x, int y, int z);

//    protected static int[] DX = { -1, 0, 1, 0, 0, 0 };
//    protected static int[] DY = { 0, 0, 0, 0, -1, 1 };
//    protected static int[] DZ = { 0, 1, 0, -1, 0, 0 };
//    static int[] opposite = { 2, 3, 0, 1, 5, 4 };

//}
