using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VelNet;
using System;
using VelUtils;

public class MoleculeManager : MonoBehaviour
{

    // Reference to the CarbonAtom prefab
    [SerializeField]
    public GameObject carbonAtomPrefab;
    [SerializeField]
    public GameObject nitrogenAtomPrefab;
    [SerializeField]
    public GameObject oxygenAtomPrefab;
    [SerializeField]
    public GameObject addCarbonAtomPrefab;
    [SerializeField]
    public GameObject addNitrogenAtomPrefab;
    [SerializeField]
    public GameObject addOxygenAtomPrefab;
    public List<AtomSphere> atoms = new List<AtomSphere>();
    private int addCounter = 0; 
    public TextMeshProUGUI counterText;

    [SerializeField]
    public TextMeshProUGUI buildText;

    public NetworkObject networkObject;

    public AtomSphere initialAtom;

    public int numCarbonAtoms;
    public int numNitrogenAtoms;
    public int numHydrogenAtoms;
    public int numOxygenAtoms;

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    private IEnumerator Start()
    {
        while (networkObject.networkId == "")
        {
            yield return null;
        }

        if (networkObject.IsMine)
        {
            initialAtom = AddAtom(this.transform.position, Quaternion.identity, "Carbon");
        }
    }

    private void Update()
    { 
        Debug.Log("atoms size: " + atoms.Count);
        Boolean notSaidMessage4 = true;
        if (atoms.Count == 2 && notSaidMessage4)
        {
            buildText.text = "Correct!";
            notSaidMessage4 = false;
            
        }

        /*
        Boolean notSaidMessage9 = true;
        if (CheckAtomName("nonane") && notSaidMessage9)
        {
            Debug.Log("nonane");
            notSaidMessage9 = false;
        }
        */
    }

    public Boolean CheckAtomName(string atomName)
    {
        int[] connectionsArray = new int[atoms.Count];
        int i = 0;
        foreach (AtomSphere graphAtom in atoms)
        {
            connectionsArray[i] = graphAtom.numBonds;
            i++;
        }
        int numOnes = 0;
        int numTwos = 0;
        foreach (int j in connectionsArray)
        {
            if (j == 3) return false;
            if (j == 2) numTwos++;
            if (j == 1) numOnes++;
        }
        if (atoms.Count == 1 && atomName == "methane") return true;
        if (numOnes == 2 && numTwos == 0 && atomName == "ethane") return true;
        if (numOnes == 2 && numTwos == 1 && atomName == "propane") return true;
        if (numOnes == 2 && numTwos == 2 && atomName == "butane") return true;
        if (numOnes == 2 && numTwos == 3 && atomName == "pentane") return true;
        if (numOnes == 2 && numTwos == 4 && atomName == "hexane") return true;
        if (numOnes == 2 && numTwos == 5 && atomName == "heptane") return true;
        if (numOnes == 2 && numTwos == 6 && atomName == "octane") return true;
        if (numOnes == 2 && numTwos == 7 && atomName == "nonane") return true;
        return false;
    }
    public int NumberOfBonds(AtomSphere atom)
    {
        int bonds = 0;
        foreach (AtomSphere atomic in atoms)
        {
            if (atomic != null)
            {
                bonds += 1;
            }
        }
        return bonds;
    }
    public AtomSphere AddAtom(Vector3 position, Quaternion rotation, string atomType)
    {
        AtomSphere newAtom = null;
        if (networkObject.IsMine)
        {
            if (atomType == "Carbon")
            {
                newAtom = VelNetManager.NetworkInstantiate(carbonAtomPrefab.name, position, rotation).GetComponent<AtomSphere>();
                atoms.Add(newAtom);
            }
            if (atomType == "Nitrogen")
            {
                newAtom = VelNetManager.NetworkInstantiate(nitrogenAtomPrefab.name, position, rotation).GetComponent<AtomSphere>();
                atoms.Add(newAtom);
            }
            if (atomType == "Oxygen")
            {
                newAtom = VelNetManager.NetworkInstantiate(oxygenAtomPrefab.name, position, rotation).GetComponent<AtomSphere>();
                atoms.Add(newAtom);
            }
            return newAtom;
        }
        return null;

    }

    public void InitializeNewAtom(AtomSphere newAtom)
    {
        if (newAtom != null)
        {
            newAtom.transform.SetParent(this.transform);
            //atoms.Add(newAtom);
        }
    }


    public void proximityAdd (AtomSphere currentAtom, Transform cylinder, string atomType)
    {
        Vector3 newBondPosition = currentAtom.Cylinder2.position + currentAtom.Cylinder2.up * 0.12f;

        // Calculate the rotation for the new atom to be added
        Quaternion newBondRotation = Quaternion.FromToRotation(Vector3.down, currentAtom.Cylinder2.up);

        switch (cylinder.name)
        {
            case "Cylinder1":
                newBondPosition = currentAtom.Cylinder1.position + currentAtom.Cylinder1.up * 0.12f;

                // Calculate the rotation for the new atom to be added
                newBondRotation = Quaternion.FromToRotation(Vector3.down, currentAtom.Cylinder1.up);
                break;
            case "Cylinder2":
                newBondPosition = currentAtom.Cylinder2.position + currentAtom.Cylinder2.up * 0.12f;

                // Calculate the rotation for the new atom to be added
                newBondRotation = Quaternion.FromToRotation(Vector3.down, currentAtom.Cylinder2.up);
                break;
            case "Cylinder3":
                newBondPosition = currentAtom.Cylinder3.position + currentAtom.Cylinder3.up * 0.12f;

                // Calculate the rotation for the new atom to be added
                newBondRotation = Quaternion.FromToRotation(Vector3.down, currentAtom.Cylinder3.up);
                break;
            case "Cylinder4":
                newBondPosition = currentAtom.Cylinder4.position + currentAtom.Cylinder4.up * 0.12f;

                // Calculate the rotation for the new atom to be added
                newBondRotation = Quaternion.FromToRotation(Vector3.down, currentAtom.Cylinder4.up);
                break;
            default:
                newBondPosition = currentAtom.Cylinder2.position + currentAtom.Cylinder2.up * 0.12f;

                // Calculate the rotation for the new atom to be added
                newBondRotation = Quaternion.FromToRotation(Vector3.down, currentAtom.Cylinder2.up);
                break;
        }
        
            // Add the new carbon atom at the calculated position and with the calculated rotation
            AddAtom(newBondPosition, newBondRotation, atomType);
    }

    public void CountAtoms()
    {
        int carbonAtoms = 0;
        int hydrogenAtoms = 0;
        int nitrogenAtoms = 0;
        int oxygenAtoms = 0;
        foreach (AtomSphere atom in atoms)
        {
            foreach (AtomSphere bondedAtom in atom.bondedAtoms)
            {
                if (bondedAtom == null)
                {
                    hydrogenAtoms += 1;
                }
            }
            if (atom.atomType == "Carbon")
            {
                carbonAtoms += 1;
            }
            if (atom.atomType == "Nitrogen")
            {
                nitrogenAtoms += 1;
            }
            if (atom.atomType == "Oxygen")
            {
                oxygenAtoms += 1;
            }
        }

        numCarbonAtoms = carbonAtoms;
        numHydrogenAtoms = hydrogenAtoms;
        numNitrogenAtoms = nitrogenAtoms;
        numOxygenAtoms = oxygenAtoms;
    }

     private void UpdateCounterText()
    {
        if (counterText != null) 
        {
            counterText.text =  $"{addCounter}";
        }
    }
}
