using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

    
public class DraftController
    
{
    FileOpener fileOpener;
    public Division DraftPick;
    public List<List<Unit>> slots = new List<List<Unit>>();
    public List<DraftUnit> LogiUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> infUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> artUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> TankUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> RecUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> AAUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> helUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> airUnitsDrafted = new List<DraftUnit>();
    public List<DraftUnit> alldrafted = new List<DraftUnit>();

    public List<Unit> Logislot = null;
    public List<Unit> infslot = null;
    public List<Unit> artslot = null;
    public List<Unit> Tankslot = null;
    public List<Unit> Recslot = null;
    public List<Unit> AAslot = null;
    public List<Unit> helslot = null;
    public List<Unit> airslot = null;

    public Division DraftDivC1;
    public Division DraftDivC2;
    public Division DraftDivC3;

    public DraftUnit draftUnitC1;
    public DraftUnit draftUnitC2;
    public DraftUnit draftUnitC3;

    public int DivPoints = 50;

    public DraftController(FileOpener fileOpener)
    {
        this.fileOpener = fileOpener;

    }

    public void newDivDraft() 
    {
        Random rnd = new Random();
        List<Division> temp = new List<Division>(fileOpener.Divisionlist);
        DraftDivC1 = temp[rnd.Next(0, temp.Count)];
        temp.Remove(DraftDivC1);

        DraftDivC2 = temp[rnd.Next(0, temp.Count)];
        temp.Remove(DraftDivC2);

        DraftDivC3 = temp[rnd.Next(0, temp.Count)];
        temp.Remove(DraftDivC3);

    }
    public void Reset() 
    {
        DraftPick = null;
        LogiUnitsDrafted.Clear();
    infUnitsDrafted.Clear();
     artUnitsDrafted.Clear();
    TankUnitsDrafted.Clear();
    RecUnitsDrafted.Clear();
     AAUnitsDrafted.Clear();
     helUnitsDrafted.Clear();
     airUnitsDrafted.Clear();
        alldrafted.Clear();
        slots.Clear();
        Logislot = null;
        infslot = null;
        artslot = null;
        Tankslot = null;
        Recslot = null;
        AAslot = null;
        helslot = null;
        airslot = null;

        DraftDivC1 = null;
        DraftDivC2 = null;
        DraftDivC3 = null;

        draftUnitC1 = null;
        draftUnitC2 = null;
        draftUnitC3 = null;

     DivPoints = 50;
}
    public void newUnitDraft() 
    {
        if (checkslots())
        {
            draftUnitC1 = genUnit();
            draftUnitC2 = genUnit();
            draftUnitC3 = genUnit();
        }
        
        



    }
    public void setslots()
    {
        slots.Add(Logislot = new List<Unit>(DraftPick.LogiUnits));
        slots.Add(infslot =  new List<Unit>(DraftPick.infUnits));
        slots.Add(artslot = new List<Unit>(DraftPick.artUnits));
        slots.Add(Tankslot = new List<Unit>(DraftPick.TankUnits));
        slots.Add(Recslot = new List<Unit>(DraftPick.RecUnits));
        slots.Add(AAslot = new List<Unit>(DraftPick.AAUnits));
        slots.Add(helslot = new List<Unit>(DraftPick.helUnits));
        slots.Add(airslot = new List<Unit>(DraftPick.airUnits));
    }
    public bool checkslots() 
    {
        if (DraftPick.logi.Length == LogiUnitsDrafted.Count()||(DivPoints - DraftPick.logi[LogiUnitsDrafted.Count()] < 0)) 
        {
                slots.Remove(Logislot);
        }
        if (DraftPick.inf.Length == infUnitsDrafted.Count()|| (DivPoints - DraftPick.inf[LogiUnitsDrafted.Count()] <= 0)) {  slots.Remove(infslot);  }
        if (DraftPick.art.Length == artUnitsDrafted.Count()|| (DivPoints - DraftPick.art[artUnitsDrafted.Count()] <= 0)) { slots.Remove(artslot); }
        if (DraftPick.tank.Length == TankUnitsDrafted.Count() || (DivPoints - DraftPick.tank[TankUnitsDrafted.Count()] <= 0)) { slots.Remove(Tankslot); }
        if (DraftPick.rec.Length == RecUnitsDrafted.Count() || (DivPoints - DraftPick.rec[RecUnitsDrafted.Count()] <= 0)) { slots.Remove(Recslot); }
        if (DraftPick.aa.Length == AAUnitsDrafted.Count() || (DivPoints - DraftPick.aa[AAUnitsDrafted.Count()] <= 0)) { slots.Remove(AAslot); }
        if (DraftPick.hel.Length == helUnitsDrafted.Count() || (DivPoints - DraftPick.hel[helUnitsDrafted.Count()] <= 0)) { slots.Remove(helslot); }
        if (DraftPick.air.Length == airUnitsDrafted.Count() || (DivPoints - DraftPick.air[airUnitsDrafted.Count()] <= 0)) { slots.Remove(airslot); }
        foreach (var slot in slots) 
        { 
        if (slot.Count == 0) { slots.Remove(slot); };
        }
        if (slots.Count > 0) { return true; }
        return false;
    }
    DraftUnit genUnit() 
    {
        Random rnd = new Random();
        List<Unit> units = slots[rnd.Next(0, slots.Count)];
        Unit unit = units[rnd.Next(0, units.Count)];
        DraftUnit draftUnit = new DraftUnit();
        draftUnit.BaseUnit = unit;
        if (draftUnit.BaseUnit.transports.Count() == 0) { return draftUnit; }
        draftUnit.transport = draftUnit.BaseUnit.transports[rnd.Next(0, draftUnit.BaseUnit.transports.Count)];
        return draftUnit;
    }

    void vaildGen(DraftUnit U)
    { 
        int foundc = 1;
        switch (U.BaseUnit.unitType) {                 
        case UnitType.LOG:
                        {
                    
                    foreach (DraftUnit unit in LogiUnitsDrafted) 
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                            { foundc += 1; }
                                

                    }
                    if (foundc > U.BaseUnit.NumAvaible) 
                    {
                        foreach (List<Unit> units in slots) {units.Remove(U.BaseUnit); }
                        
                        //return false;
                    }
            break;
        }
                    case UnitType.INF:
                {
                    foreach (DraftUnit unit in infUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }


                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }

                        //return false;
                    }
                    break;
                }

            case UnitType.ART:
                        {
                    foreach (DraftUnit unit in artUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }

                        {
                        }
                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }

                       // return false;
                    }
                    break;
                }
                    case UnitType.TNK:
                        {
                    foreach (DraftUnit unit in TankUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }

                        {
                        }
                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }
                    }
                    break;
        }
                    case UnitType.REC:
                        {
                    foreach (DraftUnit unit in RecUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }

                        {
                        }
                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }
                    }
                    break;
        }
                    case UnitType.AA:
                        {
                    foreach (DraftUnit unit in AAUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }

                        {
                        }
                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }
                    }
                    break;
        }
                    case UnitType.HEL:
                        {
                    foreach (DraftUnit unit in helUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }

                        {
                        }
                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }
                    }
                    break;
        }
                     case UnitType.AIR: 
                        {

                    foreach (DraftUnit unit in airUnitsDrafted)
                    {
                        if (unit.BaseUnit.name.Contains(U.BaseUnit.name))
                        { foundc += 1; }

                        {
                        }
                    }
                    if (foundc > U.BaseUnit.NumAvaible)
                    {
                        foreach (List<Unit> units in slots) { units.Remove(U.BaseUnit); }
                    }
                    break;
        }
        }
        //return true;
    }
    public void draftedUnit(DraftUnit U) 
    {
        vaildGen(U);
        alldrafted.Add(U);
        switch (U.BaseUnit.unitType)
        {

            case UnitType.LOG:
                {
                    LogiUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.logi[LogiUnitsDrafted.Count() - 1];
                    break;
                }
            case UnitType.INF:
                {
                    infUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.inf[infUnitsDrafted.Count() - 1];
                    //division.OverflowUnits.Remove(U);
                    break;
                }
            case UnitType.ART:
                {
                    artUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.art[artUnitsDrafted.Count() - 1];
                    //division.OverflowUnits.Remove(U);
                    break;
                }
            case UnitType.TNK:
                {
                    TankUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.tank[TankUnitsDrafted.Count() - 1];
                    //division.OverflowUnits.Remove(U);
                    break;
                }
            case UnitType.REC:
                {
                    RecUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.rec[RecUnitsDrafted.Count() - 1];
                    // division.OverflowUnits.Remove(U);
                    break;
                }
            case UnitType.AA:
                {
                    AAUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.aa[AAUnitsDrafted.Count() - 1];
                    //division.OverflowUnits.Remove(U);
                    break;
                }
            case UnitType.HEL:
                {
                    helUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.hel[helUnitsDrafted.Count() - 1];
                    //division.OverflowUnits.Remove(U);
                    break;
                }
            case UnitType.AIR:
                {

                    airUnitsDrafted.Add(U);
                    DivPoints = DivPoints - DraftPick.air[airUnitsDrafted.Count() - 1];
                    // division.OverflowUnits.Remove(U);
                    break;
                }



        }
    }
}

