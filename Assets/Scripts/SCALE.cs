using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCALE : MonoBehaviour
{
    public int LFI; //Life Force Index
    public int NPI; //Neurological Peformance Index
    public int PRI; //Physical Resilience Index
    public int GMI; //Genetic Mutation Index
    public int PRF; //Pathogen Resistance Factor

    [SerializeField]
    public Dictionary<string, Vector2> min_max = new Dictionary<string, Vector2>();

    public bool isAllowed;
    public bool onBoard;

    public int peopleAllowed;
    public int wrongDesison;
    public int allowedWrongDesicions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
