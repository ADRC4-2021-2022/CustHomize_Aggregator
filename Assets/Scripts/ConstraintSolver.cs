using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TMPro for interaction with TextMeshPro text
using TMPro;
//using Eppy; //ADDED ??DO WE NEED THIS??

public class ConstraintSolver
{
    #region public fields
    public Vector3Int GridDimensions;
    VoxelGrid _grid;

    #endregion

    #region private fields
    Tile[,,] _tileGrid;
    List<TilePattern> _patternLibrary; //?? WHERE DOES THIS GO??
    List<Connection> _connections;

    private TilePattern _mat_ConPink;   //ADDED
    private TilePattern _mat_ConYellow; //ADDED
    private TilePattern _mat_ConBlue;   //ADDED
    private TilePattern _mat_Orange;    //ADDED
    private TilePattern _mat_Cyan;      //ADDED
    private TilePattern _mat_Green;     //ADDED

    //Eppy.Tuple<int, int>[] stack;     //ADDED
    int stacksize;                      //ADDED



    #endregion
    #region constructors
    public ConstraintSolver(Vector3Int gridDimensions)
    {
        GridDimensions = gridDimensions;

        //Add all connections
        _connections = new List<Connection>();

        _connections.Add(new Connection(ConnectionType.conPink, "conPink"));
        _connections.Add(new Connection(ConnectionType.conYellow, "conYellow"));
        _connections.Add(new Connection(ConnectionType.conBlue, "conBlue"));            //ADDED
        _connections.Add(new Connection(ConnectionType.conOrange, "conOrange"));        //ADDED
        _connections.Add(new Connection(ConnectionType.conCyan, "conCyan"));            //ADDED
        _connections.Add(new Connection(ConnectionType.conGreen, "conGreen"));          //ADDED

        ////Add all patterns
        //_patternLibrary = new List<TilePattern>();                                      //ADDED ??IS THIS THE RIGHT PLACE TO PUT THE PATTERN LIBRARY?

        //_patternLibrary.Add(new TilePattern(PatternType.mat_ConPink, "mat_ConPink"));       //ADDED
        //_patternLibrary.Add(new TilePattern(PatternType.mat_ConYellow, "mat_Yellow"));      //ADDED
        //_patternLibrary.Add(new TilePattern(PatternType.mat_ConBlue, "mat_ConBlue"));       //ADDED
        //_patternLibrary.Add(new TilePattern(PatternType.mat_ConOrange, "mat_ConOrange"));   //ADDED
        //_patternLibrary.Add(new TilePattern(PatternType.mat_ConCyan, "mat_ConCyan"));       //ADDED
        //_patternLibrary.Add(new TilePattern(PatternType.mat_ConGreen, "mat_ConGreen"));     //ADDED
    }


    #endregion
    #region public functions


    #endregion
    #region private functions
    private void MakeTiles()
    {
        _tileGrid = new Tile[GridDimensions.x, GridDimensions.y, GridDimensions.z];
        for (int x = 0; x < GridDimensions.x; x++)
        {
            for (int y = 0; y < GridDimensions.y; y++)
            {
                for (int z = 0; z < GridDimensions.z; z++)
                {
                    _tileGrid[x, y, z] = new Tile(new Vector3Int(x, y, z), _patternLibrary);
                }
            }
        }
    }

    private void GetNextTile()
    {
        //Check if there still are tiles to set
        //Set grid to tile
        //Count how many possible patterns there are
        //Find all the tiles with the least amount of possible patterns
        //Select a random tile out of this list
        //Assign one of the possible patterns to the tile
        //PropogateGrid on the set tile

        //ADDED
        //SET GRID TO AVAILABLE
        _grid.SetNonDeadGridState(VoxelState.Available);    //ADDED

        var anchor = new Vector3Int(2, 8, 0);               //ADDED       
        var rotation = Quaternion.Euler(0, 0, -90);         //ADDED

        _grid.SetPatternIndex(0);                           //ADDED
        _grid.AddBlock(anchor, rotation);                   //ADDED
        _grid.TryAddCurrentBlocksToGrid();                  //ADDED
    }

    //Find a tile in the grid that it not set with the least possible options

    public Voxel GetPatternVoxel()                          //ADDED
    {
        List<TilePattern> patternVoxel = new List<patternVoxel>(GetPattern().Where(v => v.Status == PatternVoxelState.Available));
        return patternVoxel[Overlapping.Range(0, patternVoxel.Count)];      //ADDED
    }

    public void StartStopWaveFunctionCollapse()             //ADDED
    {
        StopAllCoroutines();                                //ADDED
        if (!_generating)                                   //ADDED
        {
            StartCoroutine(WaveFunctionEngine());           //ADDED
        }
        _generating = !_generating;                         //ADDED
    }

    private IEnumerator WaveFunction()                      //ADDED
    {
        while (_iterationCounter < _iterations)             //ADDED
        {
            Library.InitState(Seed++); //this should not be random, but instead should be related to the ruleset provided ((what is the command for this))
            WaveFunctionStep();                             //ADDED
            _iterationCounter++;                            //ADDED
            yield return new WaitForSeconds(0.01f);         //ADDED
        }
    }


    private void WaveFunctionStep()                         //ADDED
    {
        _grid.PurgeAllBlocks();                             //ADDED
        _tryCounter = 0;                                    //ADDED
        while (_tryCounter < _triesPerIteration)            //ADDED
        {
            TryAddCorrrectBlock();                          //ADDED
            _tryCounter++;                                  //ADDED
        }

        UpdateEfficiency();                                 //ADDED
    }

    private bool TryAddCorrectBlock()                       //ADDED
    {
        _grid.SetCorrectPatternIndex();                     //ADDED

        //Add function the gets the voxels around the generated block
        //==> Get the available neighbours of all the alive voxels


        //Random available voxel: Most accurate, but very expensive ==> slow EG Seed 0: 64% filled
        //_grid.AddBlock(_grid.GetRandomAvailableVoxel().Index, Util.RandomCarthesianRotation());

        IEnumerator WaveFunctionSeed()                      //ADDED
        {
            _grid.PurgeAllBlocks();                         //ADDED
            Random.InitState(Seed);                         //ADDED
            _tryCounter = 0;                                //ADDED
            while (_tryCounter < _triesPerIteration)        //ADDED
            {
                TryAddRandomBlock();                        //ADDED
                UpdateStats();                              //ADDED
                _tryCounter++;                              //ADDED
                yield return new WaitForSeconds(0.001f);    //ADDED
            }
            UpdateEfficiency();                             //ADDED
            yield break;                                    //ADDED
        }

        //Random voxel in the entire grid: Least accurate but non expensive ==> very fast EG Seed 0: 35%
        //_grid.AddBlock(Util.RandomIndex(_grid.GridDimensions), Util.RandomCarthesianRotation());
        //Random voxel in the non dead voxels: Medium accurate, low expensive ==> medium fast EG Seed 0: 59% filled
        //ADDED
        _grid.AddBlock(_nonDeadVoxels[Random.Range(0, _nonDeadVoxels.Count)].Index, Util.RandomCarthesianRotation());       //this is better than lines 123-136

        bool blockAdded = _grid.TryAddCurrentBlocksToGrid();    //ADDED
        _grid.PurgeUnplacedBlocks();                            //ADDED



        return blockAdded;                                  //ADDED

    }

    private void PropogateGrid(Tile changedTile)            //ADDED
    {
        //Loop over the connections of the changedTile
        //Per connection: go to the neighbour tile in the connection direction
        //Crossreference the list of possible connections in the neighbour tile with the list of possilbepatterns in the connection

        //If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
        //you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

        //If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
        //propogate the grid for the new set tile

    }

    //Loop over the connections of the changedTile
    public void WaveFunctionLoop()      //ADDED
    {
        if (conPink = conYellow)        //ADDED
        {
            continue;                   //ADDED
        }

        else (conPink == conPink)       //ADDED
        {
            Console.Write(i);           //ADDED
        }
    }

    //Per connection: go to the neighbour tile in the connection direction

    public class Movement : MonoBehaviour                                   //ADDED
    {
        private bool isMatch = false;                                       //ADDED

        void Update()                                                       //ADDED
        {
            if (Input.GetMouseButtonDown(0))                                //ADDED
            {
                if (isMatch == false)                                       //ADDED
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);         //ADDED
                    isMatch = true;                                         //ADDED
                    Debug.Log("continued along direction");                 //ADDED
                }
                else
                {
                    transform.rotation = Quaternion.Euler(90, -45, 0);      //ADDED
                    Debug.Log("Turn Left and try again");                   //ADDED
                    isMatch = false;                                        //ADDED
                }
            }
        }
        
    }

    //Crossreference the list of possible connections in the neighbour tile with the list of possilbepatterns in the connection

    public abstract class ConnectionTypes                                   //ADDED
    {
    }

    public class Voxel : voxelConnect                                       //ADDED
    {
        private HashSet<VoxelPatternRelationship> _voxelConnect =           //ADDED
            new HashSet<VoxelPatternRelationship>();                        //ADDED

        public virtual IEnumerable<VoxelPatternRelationship> voxelConnect   //ADDED
        {
            get { return this._voxelConnect; }                              //ADDED
        }

        public virtual void AddVoxelConnect(VoxelConnect voxelConnect, RelationshipKind relationshipKind) //ADDED
        {
            var relationship = new VoxelPatternRelationship()               //ADDED
            {
                Parent = this,                                              //ADDED
                Child = voxelConnect,                                       //ADDED
                RelationshipKind = relationshipKind                         //ADDED
            };

            this._children.Add(relationship);                               //ADDED
            child.AddParent(relationship);                                  //ADDED
        }
    }

    public class Child : Voxel                                              //ADDED
    {
        private HashSet<ParentChildRelationship> _parents =                 //ADDED
            new HashSet<ParentChildRelationship>();                         //ADDED

        public virtual IEnumerable<VoxelPatternRelationship> Parents        //ADDED
        {
            get { return this._parents; }                                   //ADDED
        }

        internal virtual void AddParent(VoxelPatternRelationship relationship)      //ADDED
        {
            this._parents.Add(relationship);                                //ADDED
        }
    }

    public class ParentChildRelationship                                    //ADDED
    {
        public virtual Parent Parent { get; protected internal set; }       //ADDED

        public virtual Child Child { get; protected internal set; }         //ADDED

        public virtual RelationshipKind RelationshipKind { get; set; }      //ADDED
    }

    public enum RelationshipKind    //ADDED
    {
        conPink,                    //ADDED
        conYellow                   //ADDED
    }
    //If one or multiple of the neighbours has no more possible tilepattern, solving failed, start over
    //you could assign a possible tile of the previous propogation, this will cause impurities but might make it easier to solve

    //If one or multiple of the neighbours has only one possible tilepattern, set the tile pattern
    //propogate the grid for the new set tile
    #endregion
}