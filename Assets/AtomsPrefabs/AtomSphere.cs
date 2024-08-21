using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Oculus.Interaction;
using UnityEngine;
using VelNet;
using VelUtils.VRInteraction;


public class AtomSphere : MonoBehaviour
{
    public MoleculeManager moleculeManager;
    public SyncTransform syncTransform;

    public Transform Cylinder1;
    public Transform Cylinder2;
    public Transform Cylinder3;
    public Transform Cylinder4;
    
    public int numBonds;
    public int tempNumBonds;

    public string atomType;

    public AtomSphere[] bondedAtoms;// = new AtomSphere[4];

    public float raycastDistance = .6f;

    private void Awake()
    {
        moleculeManager = FindObjectOfType<MoleculeManager>();
        syncTransform = GetComponent<SyncTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        numBonds = 0;
        if (this.CompareTag("Nitrogen"))
        {
            atomType = "Nitrogen";
            bondedAtoms = new AtomSphere[3];
        }
        if (this.CompareTag("Carbon"))
        {
            atomType = "Carbon";
            bondedAtoms = new AtomSphere[4];
        }
        if (this.CompareTag("Oxygen"))
        {
            atomType = "Oxygen";
            bondedAtoms = new AtomSphere[2];
        }
        
        foreach (Transform child in transform)
        {
            if (child.name == "Cylinder1")
            {
                Cylinder1 = child;
                //tetraVector1 = child.localPosition.normalized;
            }
            else if (child.name == "Cylinder2")
            {
                Cylinder2 = child;
            }
            else if (child.name == "Cylinder3")
            {
                Cylinder3 = child;
            }
            else if (child.name == "Cylinder4")
            {
                Cylinder4 = child;
            }
        }
        moleculeManager.InitializeNewAtom(this);
    }

    private void ShootRayFromAtom(Transform cylinder)
    {
        if (cylinder == null)
        {
            Debug.LogError("Cylinder is null.");
            return;
        }

        Vector3 forwardDirection = cylinder.up;
        Vector3 rayStart = cylinder.position;

        RaycastHit[] hits = Physics.RaycastAll(rayStart, forwardDirection, raycastDistance);
        foreach (var hit in hits)
        {
            // Check if the hit transform is the one you want to ignore
            if (hit.transform == cylinder || hit.transform == this.transform)
            {
                // It's the cylinder or the AtomSphere itself, ignore it
                continue;
            }

            // Now process the hit object
            AddAtom addAtomComponent = hit.collider.GetComponent<AddAtom>();
            if (addAtomComponent != null)
            {
                if (moleculeManager.networkObject.IsMine)
                {
                    switch (cylinder.name)
                    {
                        case "Cylinder1":
                            if (this.bondedAtoms[0] == null)
                            {
                                moleculeManager.proximityAdd(this, cylinder, addAtomComponent.atomType);
                            }
                            break;
                        case "Cylinder2":
                            if (this.bondedAtoms[1] == null)
                            {
                                moleculeManager.proximityAdd(this, cylinder, addAtomComponent.atomType);
                            }
                            break;
                        case "Cylinder3":
                            if (this.bondedAtoms[2] == null)
                            {
                                moleculeManager.proximityAdd(this, cylinder, addAtomComponent.atomType);
                            }
                            break;
                        case "Cylinder4":
                            if (this.bondedAtoms[3] == null)
                            {
                                moleculeManager.proximityAdd(this, cylinder, addAtomComponent.atomType);
                            }
                            break;
                    }
                }
                addAtomComponent.GetComponent<VRMoveable>().HandleRelease();
                GameObject.Destroy(addAtomComponent.gameObject);
            }
            AtomSphere atomSphereComponent = hit.collider.GetComponent<AtomSphere>();
            if (atomSphereComponent != null)
            {
                switch (cylinder.name)
                {
                    case "Cylinder1":
                        this.bondedAtoms[0] = atomSphereComponent;
                        //tempNumBonds++;
                        break;
                    case "Cylinder2":
                        this.bondedAtoms[1] = atomSphereComponent;
                        //tempNumBonds++;
                        break;
                    case "Cylinder3":
                        this.bondedAtoms[2] = atomSphereComponent;
                        //tempNumBonds++;
                        break;
                    case "Cylinder4":
                        this.bondedAtoms[3] = atomSphereComponent;
                        //tempNumBonds++;
                        break;
                }
            }
            break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        ShootRayFromAtom(Cylinder1);
        ShootRayFromAtom(Cylinder2);
        if (atomType == "Carbon" || atomType == "Nitrogen")
        {
            ShootRayFromAtom(Cylinder3);
        }
        if (atomType == "Carbon")
        {
            ShootRayFromAtom(Cylinder4);
        }

        tempNumBonds = 0;
        foreach (AtomSphere atom in bondedAtoms)
        {
            if (atom != null) tempNumBonds++;
        }
        numBonds = tempNumBonds;
    }
}