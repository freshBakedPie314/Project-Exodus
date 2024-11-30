using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PC : MonoBehaviour
{
    SCALE scale;

    [Header("Sliders")]
    public Slider LFI_s; //Life Force Index
    public Slider NPI_s; //Neurological Peformance Index
    public Slider PRI_s; //Physical Resilience Index
    public Slider GMI_s; //Genetic Mutation Index
    public Slider PRF_s; //Pathogen Resistance Factor

    [Header("Values")]
    public TextMeshProUGUI LFI_t; //Life Force Index
    public TextMeshProUGUI NPI_t; //Neurological Peformance Index
    public TextMeshProUGUI PRI_t; //Physical Resilience Index
    public TextMeshProUGUI GMI_t; //Genetic Mutation Index
    public TextMeshProUGUI PRF_t; //Pathogen Resistance Factor

    [Header("Min")]
    public TextMeshProUGUI LFI_min; //Life Force Index
    public TextMeshProUGUI NPI_min; //Neurological Peformance Index
    public TextMeshProUGUI PRI_min; //Physical Resilience Index
    public TextMeshProUGUI GMI_min; //Genetic Mutation Index
    public TextMeshProUGUI PRF_min; //Pathogen Resistance Factor

    [Header("Max")]
    public TextMeshProUGUI LFI_max; //Life Force Index
    public TextMeshProUGUI NPI_max; //Neurological Peformance Index
    public TextMeshProUGUI PRI_max; //Physical Resilience Index
    public TextMeshProUGUI GMI_max; //Genetic Mutation Index
    public TextMeshProUGUI PRF_max; //Pathogen Resistance Factor

    [Header("ID")]
    public TextMeshProUGUI admittance;
    public TextMeshProUGUI status;
    // Start is called before the first frame update
    void Awake()
    {
        scale = GameObject.FindGameObjectWithTag("SCALE").GetComponent<SCALE>();
        
    }
    void Start()
    {
        UpdateUISCAN();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUISCALE()
    {
        LFI_s.value = scale.LFI;
        NPI_s.value = scale.NPI;
        PRI_s.value = scale.PRI;
        GMI_s.value = scale.GMI;
        PRF_s.value = scale.PRF;

        LFI_min.text = scale.min_max["LFI"][0] + "";
        NPI_min.text = scale.min_max["NPI"][0] + "";
        PRI_min.text = scale.min_max["PRI"][0] + "";
        GMI_min.text = scale.min_max["GMI"][0] + "";
        PRF_min.text = scale.min_max["PRF"][0] + "";
        
        LFI_max.text = scale.min_max["LFI"][1] + "";
        NPI_max.text = scale.min_max["NPI"][1] + "";
        PRI_max.text = scale.min_max["PRI"][1] + "";
        GMI_max.text = scale.min_max["GMI"][1] + "";
        PRF_max.text = scale.min_max["PRF"][1] + "";
    }

    public void UpdateUISCAN()
    {
        Debug.Log("Update Scan");
        LFI_s.value = scale.LFI;
        NPI_s.value = scale.NPI;
        PRI_s.value = scale.PRI;
        GMI_s.value = scale.GMI;
        PRF_s.value = scale.PRF;

        LFI_t.text = scale.LFI + "";
        NPI_t.text = scale.NPI + "";
        PRI_t.text = scale.PRI + "";
        GMI_t.text = scale.GMI + "";
        PRF_t.text = scale.PRF + "";
    }

    public void UpdateID()
    {
        if (scale.isAllowed)
        {
            admittance.text = " Accepted";
            admittance.color = new Color32(32, 194, 14 , 255);
        }
            
        else
        {
            admittance.text = " Denied";
            admittance.color = new Color32(194, 26 , 14 , 255);
        }
            

        if (scale.onBoard)
        {
            status.text = " Boarded";
            status.color = new Color32(32, 194, 14, 255);
        }
            
        else
        {
            status.text = " Yet to Board";
            status.color = new Color32(194, 26, 14 , 255);
        }

    }
}
