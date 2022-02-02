using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Eppy;
using Random = System.Random;


public class PatternManager 
{
    /// <summary>
    /// Singleton object of the PatternManager class. Refer to this to access the data inside the object.
    /// </summary>
    
    public Vector3Int index;
    public Grid grid;
    private static List<TilePattern> _patterns;

    //The pattern manager is a singleton class. This means there is only one instance of the PatternManager class in the entire project and it can be refered to anywhere withing the project

    
    public static PatternManager Instance { get; } = new PatternManager();

    private static List<Pattern> _patterns;
    public static Dictionary<string, Pattern> _patternsByName;

    /// <summary>
    /// returns a read only list of the patterns defined in the project
    /// </summary>
    public static ReadOnlyCollection<Pattern> Patterns => new ReadOnlyCollection<Pattern>(_patterns);


    /// <summary>
    /// returns a read only dictionary of the patterns defined in the project organised by name
    /// </summary>
    public static ReadOnlyDictionary<string, Pattern> PatternsByName => new ReadOnlyDictionary<string, Pattern>(_patternsByName);

    /// <summary>
    /// private constructor. All initial patterns will be defined in here
    /// </summary>
    
    private PatternManager()
    {
        _patterns = new List<Pattern>();
        _patternsByName = new Dictionary<string, Pattern>();

        //Define pattern A ?????? HOW DO WE ADD COLOR INFORMATION?
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0, 0, 0)
            },
            "pattern A"
            );


        //Define pattern B
         AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0, 0, 0),
            },
                "pattern B"
            );

        //Define pattern C
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern C"
            );

        //Define pattern D
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern D"
            );

        //Define pattern E
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern E"
            );

        //Define pattern F
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern F"
            );

        //Define pattern G
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern G"
            );

        //Define pattern H
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern H"
            );

        //Define pattern I
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern I"
            );

        //Define pattern J
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern J"
            );

        //Define pattern K
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern K"
            );

        //Define pattern L
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern L"
            );

        //Define pattern M
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern M"
            );

        //Define pattern N
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern N"
            );

        //Define pattern O
        AddPattern(
            new List<Vector3Int>()
            {
                new Vector3Int(0,0,0)
            },
            "pattern O"
            );
    }
}

class TilePattern
{
    int TileIndex; //every tile index needs to be unique
    GameObject TilePrefab;
    
    List<Connection> connections = new List<Connection>(6);
    
    connections.Add (new Connection() { PatternSideConnection = "yellow" } );

    //How do we write the connections in a list?

    
    //CALLING THE COLORS OF EACH SIDE  ,,,,, I think that RAL numbers must be used for this or a numerical value 
    public enum TileIndexA
    {
        xl = y,
        xr = y,
        zd = c,
        zu = c
    }

    public enum TileIndexB
    {
        xl = y,
        xr = y,
        zd = m,
        zu = m
    }

    public enum TileIndexC
    {
        xl = y,
        xr = g,
        zd = o,
        zu = c
    }

    public enum TileIndexD
    {
        xl = y,
        xr = g,
        zd = o,
        zu = m
    }

    public enum TileIndexE
    {
        xl = b,
        xr = g,
        zd = o,
        zu = o
    }

    public enum TileIndexF
    {
        xl = g,
        xr = b,
        zd = o,
        zu = o
    }

    public enum TileIndexG
    {
        xl = b,
        xr = b,
        zd = o,
        zu = o
    }

    public enum TileIndexH
    {
        xl = b,
        xr = y,
        zd = c,
        zu = o
    }

    public enum TileIndexI
    {
        xl = y,
        xr = b,
        zd = m,
        zu = o
    }

    public enum TileIndexJ
    {
        xl = g,
        xr = g,
        zd = o,
        zu = o
    }

    public enum TileIndexK
    {
        xl = b,
        xr = y,
        zd = o,
        zu = m
    }

    public enum TileIndexL
    {
        xl = y,
        xr = b,
        zd = o,
        zu = c
    }

    public enum TileIndexM
    {
        xl = y,
        xr = y,
        zd = o,
        zu = o
    }

    public enum TileIndexN
    {
        xl = y,
        xr = g,
        zd = m,
        zu = o
    }

    public enum TileIndexO
    {
        xl = g,
        xr = y,
        zd = c,
        zu = o
    }


    //private readonly Dictionary<PatternSides, Color> colors = new Dictionary<PatternSides>
    //{

    //}

}

class Connection
{
    //If a tile has a certain connection, it can connect to these possible Neighbours
    List<Tile> possibleNeighbours;
}

class WFC
{
    //this will be the generator for your WFC

    protected bool[][] wave;

    protected double[] weights;
    double[] weightLogWeights;

    int[] sumsOfOnes;
    double sumOfWeights, sumOfWeightLogWeights, startingEntropy;
    double[] sumsOfWeights, sumsOfWeightLogWeights, entropies;

    protected int[][][] propagator; // ??WHY IS THIS PROTECTED?? propogating in all three axes
    int[][][] compatible;           // direction of the integers
    protected int[] observed;

    Eppy.Tuple<int, int>[] stack;
    int stacksize;

    public bool Run(int seed, int limit) // running loop of seed to its limit 
    {
        if (wave == null) Init(); //if the wave is null, init. This is called lower down.

        Clear(); // ??WHY DOES THIS CLEAR??
        random = new Random(seed); // creating a new random seed

        for (int l = 0; l < limit || limit == 0; l++) // ?? (int i = 0; i < length; i++) 1 is less than 0, 1 is less than the limit or the limit is zero, add 1
        {
            bool? result = Observe(); // ??WHY THE QUESTION MARK?? the result is observe
            if (result != null) return (bool)result; //if the result is not null, return the result
            Propagate(); //propogate
        }

        return true; //return true
    }

    public IEnumerator RunViaEnumerator(int seed, int limit, Action<bool> resultCallback, Action<bool[][]> iterationCallback)
    {                               //calling seed, limit, calling back result, calling back iteration
        if (wave == null) Init();   //if the wave is null, init. This is called lower down.

        Clear(); //??WHY DOES THIS CLEAR??
        if (seed != 0) //if the seed is not zero...
        {
            random = new Random(seed); //...new random seed
        }
        else
        {
            random = new Random(); // ??WHAT DOES THIS HAVE IN IT, IF NOT THE SEED?? else, new random
        }


        for (int l = 0; l < limit || limit == 0; l++) //same for loop as above
        {
            bool? result = Observe(); //same as above
            if (result != null) // if the result is not null...
            {
                resultCallback(result.Value); //...call back the result...
                break; //...and break the for loop
            }
            Debug.Log("Propagate, iteration: " + l); //write propogate, iteration, +1.
            Propagate(); //propogate
            iterationCallback(wave); //??WHY IS IT WAVE?? callback iteration, wave
            yield return null; //??
        }
    }

    //Need to create a grid file
    void CreateGrid()
    {
       
    }

    //void SelectNextTile()
    //{
    //    //select the tile with the least amount of options
    //}

    public IEnumerator RunViaEnumerator(int seed, int limit, Action<bool> resultCallback, Action<bool[][]> iterationCallback)           // This should run through list and create possible       (1)        
    {                                                                                                                                   // connection points within the system                    (2)
        if (wave == null) Init();

        Clear();
        if (seed != 0)
        {
            random = new Random(seed);
        }
        else
        {
            random = new Random();
        }


        for (int l = 0; l < limit || limit == 0; l++)
        {
            bool? result = Observe();
            if (result != null)
            {
                resultCallback(result.Value);
                break;
            }
            Debug.Log("Propagate, iteration: " + l);
            Propagate();
            iterationCallback(wave);
            yield return null;
        }
    }

    private void Init()
    {
        wave = new bool[FMX * FMY * FMZ][];
        compatible = new int[wave.Length][][];
        for (int i = 0; i < wave.Length; i++)
        {
            wave[i] = new bool[T];
            compatible[i] = new int[T][];
            for (int t = 0; t < T; t++) compatible[i][t] = new int[DIRECTIONS_AMOUNT];
        }

        weightLogWeights = new double[T];
        sumOfWeights = 0;
        sumOfWeightLogWeights = 0;

        for (int t = 0; t < T; t++)
        {
            weightLogWeights[t] = weights[t] * Math.Log(weights[t]);
            sumOfWeights += weights[t];
            sumOfWeightLogWeights += weightLogWeights[t];
        }

        startingEntropy = Math.Log(sumOfWeights) - sumOfWeightLogWeights / sumOfWeights;

        sumsOfOnes = new int[FMX * FMY * FMZ];
        sumsOfWeights = new double[FMX * FMY * FMZ];
        sumsOfWeightLogWeights = new double[FMX * FMY * FMZ];
        entropies = new double[FMX * FMY * FMZ];

        stack = new Tuple<int, int>[wave.Length * T];
        stacksize = 0;
    }

    bool Observe()
    {
        double min = 1E+3;
        int argmin = -1;

        for (int i = 0; i < wave.Length; i++)
        {
            int x, y, z;
            To3D(i, out x, out y, out z);
            if (OnBoundary(x, y, z)) continue;

            int amount = sumsOfOnes[i];
            if (amount == 0) return false;

            double entropy = entropies[i];
            if (amount > 1 && entropy <= min)
            {
                double noise = 1E-6 * random.NextDouble();
                if (entropy + noise < min)
                {
                    min = entropy + noise;
                    argmin = i;
                }
            }
        }

        if (argmin == -1)
        {
            observed = new int[FMX * FMY * FMZ];
            for (int i = 0; i < wave.Length; i++)
            {
                for (int t = 0; t < T; t++)
                {
                    if (wave[i][t])
                    {
                        observed[i] = t; break;
                    }
                }
            }
            return true;
        }

        double[] distribution = new double[T];
        for (int t = 0; t < T; t++) distribution[t] = wave[argmin][t] ? weights[t] : 0;
        int r = distribution.Random(random.NextDouble());

        bool[] w = wave[argmin];
        for (int t = 0; t < T; t++) if (w[t] != (t == r)) Ban(argmin, t);

        return null;
    }



    void PropogateGrid(List<Tile> lastAdjustedTiles) //make sure you don't get infinite loops
    {
        //Find all the neighbours of the lastAdjustedTiles that are not set yet

        //Reduce the possible tiel patterns in the neighbours according to the connections

        //Save a list of every tile that has been changed


        //Run PropogateGrid with the next changed tiles ---FUNCTION CALLS ITSELF

        while (stacksize > 0)
        {
            var e1 = stack[stacksize - 1];
            stacksize--;

            int i1 = e1.Item1;
            int x1, y1, z1;
            To3D(i1, out x1, out y1, out z1);
            bool[] w1 = wave[i1];

            for (int d = 0; d < DIRECTIONS_AMOUNT; d++)
            {
                int dx = DX[d], dy = DY[d], dz = DZ[d];
                int x2 = x1 + dx, y2 = y1 + dy, z2 = z1 + dz;
                if (OnBoundary(x2, y2, z2)) continue;

                if (x2 < 0) x2 += FMX;
                else if (x2 >= FMX) x2 -= FMX;
                if (y2 < 0) y2 += FMY;
                else if (y2 >= FMY) y2 -= FMY;
                if (z2 < 0) z2 += FMY;
                else if (z2 >= FMZ) z2 -= FMZ;

                int i2 = To1D(x2, y2, z2);
                int[] p = propagator[d][e1.Item2];
                int[][] compat = compatible[i2];

                for (int l = 0; l < p.Length; l++)
                {
                    int t2 = p[l];
                    int[] comp = compat[t2];

                    comp[d]--;
                    if (comp[d] == 0)
                    {
                        Ban(i2, t2);
                    }
                }
            }
        }


        void Ban(int i, int t)             //ban?
        {
            wave[i][t] = false;

            int[] comp = compatible[i][t];
            for (int d = 0; d < DIRECTIONS_AMOUNT; d++) comp[d] = 0;
            stack[stacksize] = new Tuple<int, int>(i, t);
            stacksize++;

            double sum = sumsOfWeights[i];
            entropies[i] += sumsOfWeightLogWeights[i] / sum - Math.Log(sum);

            sumsOfOnes[i] -= 1;
            sumsOfWeights[i] -= weights[t];
            sumsOfWeightLogWeights[i] -= weightLogWeights[t];

            sum = sumsOfWeights[i];
            entropies[i] -= sumsOfWeightLogWeights[i] / sum - Math.Log(sum);
        }
    protected virtual void Clear()                                       // I am actually not sure what this part does but it is listed in the   (1) 
    {                                                                                                                     // same section as the propagate function so I included it here for now (2)
        for (int i = 0; i < wave.Length; i++)
        {
            for (int t = 0; t < T; t++)
            {
                wave[i][t] = true;
                for (int d = 0; d < DIRECTIONS_AMOUNT; d++) compatible[i][t][d] = propagator[opposite[d]][t].Length;
            }

            sumsOfOnes[i] = weights.Length;
            sumsOfWeights[i] = sumOfWeights;
            sumsOfWeightLogWeights[i] = sumOfWeightLogWeights;
            entropies[i] = startingEntropy;
        }
    }





    //This function should run every time the amount of options in a tile is reduced to 1
    void AssignTilePattern()
    {
        //Create the geometry of the tile inside the grid
        //Instantiate prefab



    }

    public Dictionary<int, GameObject> GOPatternPrefabs
    {
        get
        {
            if (_goPatternPrefabs == null)
            {
                _goPatternPrefabs = new Dictionary<int, GameObject>();
                _goPatternPrefabs.Add(0, Resources.Load("Prefabs/PrefabPatternA") as GameObject);
                _goPatternPrefabs.Add(1, Resources.Load("Prefabs/PrefabPatternB") as GameObject);
                _goPatternPrefabs.Add(2, Resources.Load("Prefabs/PrefabPatternC") as GameObject);
                _goPatternPrefabs.Add(3, Resources.Load("Prefabs/PrefabPatternD") as GameObject);
                _goPatternPrefabs.Add(4, Resources.Load("Prefabs/PrefabPatternE") as GameObject);
                _goPatternPrefabs.Add(5, Resources.Load("Prefabs/PrefabPatternF") as GameObject);
                _goPatternPrefabs.Add(6, Resources.Load("Prefabs/PrefabPatternG") as GameObject);
                _goPatternPrefabs.Add(7, Resources.Load("Prefabs/PrefabPatternH") as GameObject);
                _goPatternPrefabs.Add(8, Resources.Load("Prefabs/PrefabPatternI") as GameObject);
                _goPatternPrefabs.Add(9, Resources.Load("Prefabs/PrefabPatternJ") as GameObject);
                _goPatternPrefabs.Add(10, Resources.Load("Prefabs/PrefabPatternK") as GameObject);
                _goPatternPrefabs.Add(11, Resources.Load("Prefabs/PrefabPatternL") as GameObject);
                _goPatternPrefabs.Add(12, Resources.Load("Prefabs/PrefabPatternM") as GameObject);
                _goPatternPrefabs.Add(13, Resources.Load("Prefabs/PrefabPatternN") as GameObject);
                _goPatternPrefabs.Add(14, Resources.Load("Prefabs/PrefabPatternO") as GameObject);

            }
            return _goPatternPrefabs;
        }
    }


    void GlobalOperations()
    {
        //ex: reducing the possibilities next to a street
    }

    //for further implementation
    void BackTrack()
    {
        //When a tile has no possible options, return to the last operation that reduced the options in the tile
        //Remove the selected option that made the grid invalid
        //Select another option.
        //How many backTracks steps do you store? --- IT IS PRETTY LIGHT RIGHT NOW, SO SHOULDN'T BE A PROBLEM TO STORE ALL
        //IS IT NECESSARY FOR US TO BACKTRACK?  - i second this
    }
}

class WFCGrid
{
    Tile[,,] Tiles;
    //All the operations with the grid

    void CalculateEntropyAndPatternIdAt(int x, int y, int z, out double entropy, out int? patternId)
    {
        int indexInWave = To1D(x, y, z);
        int amount = 0;
        patternId = null;
        var possiblePatternsFlags = wave[indexInWave];
        for (int t = 0; t < T; t++)
        {
            if (possiblePatternsFlags[t])
            {
                amount += 1;
                patternId = t;
            }
        }

        if (amount != 1)
        {
            patternId = null;
        }

        entropy = entropies[indexInWave] / startingEntropy;
    }

    protected int To1D(int x, int y, int z)
    {
        return z * FMX * FMY + y * FMX + x;
    }

    protected void To3D(int index, out int x, out int y, out int z)
    {
        z = index / (FMX * FMY);
        index -= z * FMX * FMY;
        y = index / FMX;
        x = index % FMX;
    }

    protected abstract bool OnBoundary(int x, int y, int z);

    public abstract CellState GetCellStateAt(int x, int y, int z);

    protected static int[] DX = { -1, 0, 1, 0, 0, 0 };
    protected static int[] DY = { 0, 0, 0, 0, -1, 1 };
    protected static int[] DZ = { 0, 1, 0, -1, 0, 0 };
    static int[] opposite = { 2, 3, 0, 1, 5, 4 };

}
