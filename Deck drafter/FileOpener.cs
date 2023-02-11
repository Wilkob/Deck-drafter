using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

public class FileOpener
{
    public List<Division> Divisionlist = new List<Division>();

    public void run()
    {
        getDivsNamesfromDivsionList();
        getDivsCosts();
        UnitFileDivisions();
        UnitFilePacks();
        UnitFileDivisionRules();
        getIDs();
        getunitdetails();
        getunitnames();
    }

    void getDivsCosts()
    {
        string path = @"Data/DivisionCostMatrix.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                bool foundM = false;
                int Mcount = 0;
                Division Found = Divisionlist[0];
                while ((s = sr.ReadLine()) != null)
                {
                    s.Trim();
                    if (s != "")
                    {

                        if (foundM)
                        {
                            if (s != "]")
                            {
                                if (s != "[")
                                {

                                    Mcount++;
                                    if (Mcount == 1)
                                    {
                                        Found.logi = FillDivArray(ref s);
                                    }
                                    if (Mcount == 2) { Found.rec = FillDivArray(ref s); }
                                    if (Mcount == 3) { Found.inf = FillDivArray(ref s); }
                                    if (Mcount == 4) { Found.tank = FillDivArray(ref s); }
                                    if (Mcount == 5) { Found.art = FillDivArray(ref s); }
                                    if (Mcount == 6) { Found.aa = FillDivArray(ref s); }
                                    if (Mcount == 9) { Found.hel = FillDivArray(ref s); }
                                    if (Mcount == 10) { Found.air = FillDivArray(ref s); }
                                }
                                else { continue; }
                            }
                            else
                            {
                                foundM = false;
                                Mcount = 0;
                                continue;
                            }
                        }
                        if (s[0] == 'M')
                        {

                            s = s.Substring(s.IndexOf("_"));
                            s = s.Substring(0, s.IndexOf(" "));
                            s.Trim();
                            foreach (Division Division in Divisionlist)
                            {

                                string n = Division.getname();
                                if (n == "~/Descriptor_Deck_Division_RFA_TerrKdo_Sud_multi") { n = "~/Descriptor_Deck_Division_RFA_TerrKo_Sud_multi"; }
                                n = n.Substring(n.IndexOf("_") + 1);
                                n = n.Substring(n.IndexOf("_") + 1);
                                n = n.Substring(n.IndexOf("_"));
                                if (s == n)
                                {
                                    foundM = true;
                                    Found = Division;
                                    break;
                                }
                            }
                        }


                    }
                }
            }
        }
    }

    private static int[] FillDivArray(ref string s)
    {
        int[] Array;
        s = s.Substring(s.IndexOf("["));
        s = s.Substring(0, s.IndexOf("/"));
        int count = s.Count(f => (f == ','));
        string justNumbers = string.Concat(s.Where(char.IsDigit));
        Array = new int[count];
        int i = 0;
        foreach (char c in justNumbers)
        {
            Array[i++] = c - '0';
        }

        return Array;
    }

    void getDivsNamesfromDivsionList()
    {
        string path = @"Data/DivisionList.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                bool atnames = false;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    if (s.Trim() == "]")
                    {
                        atnames = false;
                    }
                    if (atnames)
                    {
                        Division temp = new Division();
                        s = s.Trim();
                        s = s.Substring(0, s.Length - 1);

                        temp.setname(s);

                        Divisionlist.Add(temp);

                    }

                    if (s.Trim() == "[")
                    {
                        atnames = true;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("error");
        }
    }

    void UnitFileDivisions()
    {
        string path = @"Data/Divisions.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                Division activeDiv = Divisionlist[1];
                bool DevFound = false;
                bool unittime = false;
                while ((s = sr.ReadLine()) != null)
                {

                    s.Trim();

                    if (unittime == true)
                    {
                        if (s != "    ]")
                        {
                            Unit temp = new Unit();
                            string s2 = s.Substring(s.IndexOf(","));
                            string justNumbers = string.Concat(s2.Where(char.IsDigit));

                            temp.NumAvaible = int.Parse(justNumbers);
                            temp.name = s.Substring(s.IndexOf("/") + 1);
                            temp.name = temp.name.Substring(0, temp.name.IndexOf(","));
                            activeDiv.OverflowUnits.Add(temp);
                        }
                        else { unittime = false; }
                    }
                    if (DevFound)
                    {
                        if (activeDiv.UserName == null)
                        {
                            if (s.Length > 16)
                            {
                                string T;
                                try
                                {
                                    T = s.Substring(0, 16);
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    T = s;
                                    throw;
                                }

                                if (T == "    DivisionName")
                                {
                                    activeDiv.UserName = s.Substring(20, 10);//(s.IndexOf("'"), s.LastIndexOf("'"));
                                }
                            }
                        }
                        else
                        {
                            if (s == "    [") { unittime = true; }
                            if (s == ")") { DevFound = false; }

                        }
                    }

                    else
                    {

                        if (s != "")
                        {
                            //s = s.Substring(0, s.IndexOf(" "));

                            foreach (Division Division in Divisionlist)

                            {

                                string n = Division.getname();
                                n = n.Substring(n.IndexOf("/") + 1);

                                if (s.Contains(n))

                                {

                                    activeDiv = Division;

                                    DevFound = true;
                                    break;
                                }

                            }
                        }

                    }
                }

            }
        }
    }
    void UnitFilePacks()
    {


        List<Unit> AllUnits = new List<Unit>();
        foreach (Division division in Divisionlist)
        {
            foreach (Unit U in division.OverflowUnits) { AllUnits.Add(U); }
        }
        string path = @"Data/Packs.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                Unit Found = null;
                bool FoundU = false;
                while ((s = sr.ReadLine()) != null)
                {
                    if (!FoundU)
                    {
                        if (s.Length > 51)
                        {
                            foreach (Unit unit in AllUnits)
                            {
                                if (s.Contains(unit.name))
                                {
                                    Found = unit;
                                    FoundU = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (s.Contains("~/Descriptor_Unit"))
                        {
                            Found.unitname = s.Substring(s.IndexOf("~"));
                            FoundU = false;
                            AllUnits.Remove(Found);
                        }

                    }
                }
            }
        }
    }

    void UnitFileDivisionRules()
    {

        //List<Unit> AllUnits = new List<Unit>();

        string path = @"Data/DivisionRules.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                Division FoundD = Divisionlist[0];
                Unit Found = null;
                bool FoundU = false;
                int FoundC = 0;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains("Descriptor_Deck_Division"))
                    {
                        foreach (Division division in Divisionlist)
                        {
                            if (s.Contains(division.getname())) { FoundD = division; break; }
                        }
                    }

                    if (!FoundU)
                    {
                        foreach (Unit unit in FoundD.OverflowUnits)
                        {
                            if (s.Contains(unit.unitname.Substring(0, unit.unitname.Length - 1)))
                            {

                                Found = unit;
                                FoundU = true;
                                //FoundC = 0;


                            }

                        }
                    }
                    else
                    {

                        if (s.Contains("AvailableWithoutTransport"))
                        {
                            if (s[52] == 'T') { Found.AvailableWithoutTransport = true; }
                            else { Found.AvailableWithoutTransport = false; }
                        }
                        if (s.Contains("NumberOfUnitInPack ="))
                        {
                            string justNumbers = string.Concat(s.Where(char.IsDigit));

                            Found.Numinpack = int.Parse(justNumbers);
                        }
                        if (s.Contains("AvailableTransportList"))
                        {

                            s = s.Substring(s.LastIndexOf("["));
                            int count = s.Count(f => (f == ',')) + 1;
                            string[] array = s.Split("/");
                            foreach (string i in array)
                            {
                                transport temp = new transport();
                                temp.unitname = i.Replace("]", "");
                                temp.unitname = i.Replace(",", "");
                                temp.unitname = i.Replace("~", "");
                                if (temp.unitname.Contains("Descriptor_Unit")) { Found.transports.Add(temp); }
                            }
                        }
                        if (s.Contains("NumberOfUnitInPackXPMultiplier"))
                        {
                            int count = 4;


                            for (int i = 0; i < count; i++)
                            {
                                string s2 = "";
                                if (i == 0)
                                {
                                    s2 = s.Substring(s.IndexOf("[") + 1, 3);
                                }
                                if (i == 1)
                                {
                                    s2 = s.Substring(s.IndexOf(",") + 2, 3);
                                }
                                if (i == 2)
                                {
                                    s2 = s.Substring(s.LastIndexOf(",") - 4, 4); ;
                                }
                                if (i == 3)
                                {
                                    s2 = s.Substring(s.IndexOf("]") - 4, 4);
                                }

                                //string justNumbers = string.Concat(s2.Where(char.IsDigit));
                                Found.NuminpackMod[i] = float.Parse(s2);




                            }
                            FoundU = false;
                        }
                    }
                }

            }
        }
    }

    void getIDs()
    {
        List<Unit> AllUnits = new List<Unit>();
        List<Division> AllDivs = new List<Division>();
        List<transport> Alltransport = new List<transport>();
        foreach (Division division in Divisionlist)
        {
            AllDivs.Add(division);
            foreach (Unit U in division.OverflowUnits)
            {
                AllUnits.Add(U);
                foreach (transport T in U.transports)
                {
                    Alltransport.Add(T);
                }
            }
        }

        string path = @"Data/DeckSerializer.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    foreach (Division division in Divisionlist)
                    {
                        if (s.Contains(stripstuff(division.getname())))
                        {
                            s = s.Substring(s.IndexOf(","));
                            string justNumbers = string.Concat(s.Where(char.IsDigit));
                            division.id = int.Parse(justNumbers);
                            // AllDivs.Remove(division);
                            break;
                        }
                    }
                    foreach (Unit unit in AllUnits)
                    {
                        if (s.Contains(stripstuff(unit.unitname)))
                        {
                            s = s.Substring(s.IndexOf(","));
                            string justNumbers = string.Concat(s.Where(char.IsDigit));
                            unit.id = int.Parse(justNumbers);
                            // AllUnits.Remove(unit);
                            break;
                        }
                    }
                    foreach (transport transport in Alltransport)
                    {
                        if (s.Contains(stripstuff(transport.unitname)))
                        {
                            s = s.Substring(s.IndexOf(","));
                            string justNumbers = string.Concat(s.Where(char.IsDigit));
                            transport.id = int.Parse(justNumbers);
                            //Alltransport.Remove(transport);
                            break;
                        }
                    }
                }
            }
        }

    }
    string stripstuff(string input)
    {
        string temp2 = input.Trim();
        string temp = temp2.Substring(temp2.IndexOf("D"));
        temp = temp.Substring(0, temp.Length - 1);
        return temp;
    }
    void getunitdetails()
    {

        List<Unit> AllUnits = new List<Unit>();
        List<transport> Alltransport = new List<transport>();
        foreach (Division division in Divisionlist)
        {
            foreach (Unit U in division.OverflowUnits)
            {
                AllUnits.Add(U);
                foreach (transport T in U.transports)
                {
                    Alltransport.Add(T);
                }
            }
        }
        string path = @"Data/UniteDescriptor.ndf";
        if (File.Exists(path))
        {
            using StreamReader sr = File.OpenText(path);
            {
                string s;
                List<Unit> FounduL = new List<Unit>();
                bool BoolFoundU = false;
                List<transport> FoundtL = new List<transport>();
                bool BoolFoundT = false;
                while ((s = sr.ReadLine()) != null)
                {
                    if (BoolFoundT | BoolFoundU)
                    {
                        if (s.Contains("UnitName"))
                        {
                            if (BoolFoundU)
                            {
                                foreach (Unit Foundu in FounduL)
                                {
                                    Foundu.Username = s.Substring(s.IndexOf("'"));
                                    Foundu.Username = Foundu.Username.Substring(0, Foundu.Username.LastIndexOf("'"));
                                    BoolFoundU = false;


                                }
                                FounduL.Clear();
                            }
                            if (BoolFoundT)
                            {
                                foreach (transport Foundt in FoundtL)
                                {
                                    Foundt.username = s.Substring(s.IndexOf("'"));
                                    Foundt.username = Foundt.username.Substring(0, Foundt.username.LastIndexOf("'"));
                                    BoolFoundT = false;

                                }
                                FoundtL.Clear();
                            }
                        }
                    }
                    if (BoolFoundU)
                    {
                        if (s.Contains("Factory"))
                        {
                            //s = s.Substring(s.IndexOf("/"));
                            foreach (Unit Foundu in FounduL)
                            {
                                if (s.Contains("Logistic")) { Foundu.unitType = UnitType.LOG; }
                                if (s.Contains("Infantry")) { Foundu.unitType = UnitType.INF; }
                                if (s.Contains("Support")) { Foundu.unitType = UnitType.ART; }
                                if (s.Contains("Tanks")) { Foundu.unitType = UnitType.TNK; }
                                if (s.Contains("Recons")) { Foundu.unitType = UnitType.REC; }
                                if (s.Contains("AT")) { Foundu.unitType = UnitType.AA; }
                                if (s.Contains("Helis")) { Foundu.unitType = UnitType.HEL; }
                                if (s.Contains("Planes")) { Foundu.unitType = UnitType.AIR; }
                            }
                        }
                    }
                    if (s.Contains("Descriptor_Unit_"))
                    {
                        foreach (transport t in Alltransport)
                        {
                            string temp2 = t.unitname.Trim();
                            string temp = temp2.Substring(temp2.IndexOf("D"));
                            temp = temp.Substring(0, temp.Length - 1);
                            if (s.Contains(temp))
                            {
                                FoundtL.Add(t);
                                BoolFoundT = true;

                            }
                        }
                        foreach (Unit u in AllUnits)
                        {
                            string temp2 = u.unitname.Trim();
                            string temp = temp2.Substring(temp2.IndexOf("D"));
                            temp = temp.Substring(0, temp.Length - 1);
                            if (s.Contains(temp))
                            {
                                FounduL.Add(u);
                                BoolFoundU = true;

                            }


                        }
                    }
                }
            }
        }

        foreach (Division division in Divisionlist)
        {
            foreach (Unit U in division.OverflowUnits)
            {
                switch (U.unitType)
                {

                    case UnitType.LOG:
                        {
                            division.LogiUnits.Add(U);
                            //division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.INF:
                        {
                            division.infUnits.Add(U);
                            //division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.ART:
                        {
                            division.artUnits.Add(U);
                            //division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.TNK:
                        {
                            division.TankUnits.Add(U);
                            //division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.REC:
                        {
                            division.RecUnits.Add(U);
                            // division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.AA:
                        {
                            division.AAUnits.Add(U);
                            //division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.HEL:
                        {
                            division.helUnits.Add(U);
                            //division.OverflowUnits.Remove(U);
                            break;
                        }
                    case UnitType.AIR:
                        {

                            division.airUnits.Add(U);
                            // division.OverflowUnits.Remove(U);
                            break;
                        }



                }



            }

        }
    }

    void getunitnames()
    {
        List<Unit> AllUnits = new List<Unit>();
        List<transport> Alltransport = new List<transport>();
        foreach (Division division in Divisionlist)
        {
            foreach (Unit U in division.OverflowUnits)
            {
                AllUnits.Add(U);
                foreach (transport T in U.transports)
                {
                    Alltransport.Add(T);
                }
            }
        }
        string path = @"Data/UnitNames.txt";


        using StreamReader sr = File.OpenText(path);
        {
            string s;

            List<Unit> FounduL = new List<Unit>();
            bool BoolFoundU = false;

            List<transport> FoundtL = new List<transport>();

            bool BoolFoundT = false;

            while ((s = sr.ReadLine()) != null)
            {
                if (BoolFoundT | BoolFoundU)
                {
                    if (s.Contains("name:"))
                    {
                        if (BoolFoundU)
                        {
                            foreach (Unit Foundu in FounduL)
                            {
                                Foundu.Username = s.Substring(s.IndexOf("'"));
                                Foundu.Username = Foundu.Username.Substring(0, Foundu.Username.LastIndexOf("'"));
                                BoolFoundU = false;


                            }
                            FounduL.Clear();
                        }
                        if (BoolFoundT)
                        {
                            foreach (transport Foundt in FoundtL)
                            {
                                Foundt.username = s.Substring(s.IndexOf("'"));
                                Foundt.username = Foundt.username.Substring(0, Foundt.username.LastIndexOf("'"));
                                BoolFoundT = false;

                            }
                            FoundtL.Clear();
                        }
                    }
                }

                if (s.Contains("id:"))
                {
                    foreach (transport t in Alltransport)
                    {
                        int temp = t.id;
                        string justNumbers = string.Concat(s.Where(char.IsDigit));

                        if (temp == Int32.Parse(justNumbers))
                        {
                            FoundtL.Add(t);
                            BoolFoundT = true;

                        }
                    }
                    foreach (Unit u in AllUnits)
                    {
                        int temp = u.id;
                        string justNumbers = string.Concat(s.Where(char.IsDigit));

                        if (temp == Int32.Parse(justNumbers))
                        {
                            FounduL.Add(u);
                            BoolFoundU = true;

                        }
                    }
                }
            }
        }
    }
}

                