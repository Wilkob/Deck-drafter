using System;
using System.Windows.Forms;
    public enum UnitType
    {
        LOG, INF, ART, TNK, REC, AA, HEL, AIR
    }
	public class Unit
	{

    public UnitType unitType;
    public string name;
    public string Username;//found in unit discription and .csv
    public string unitname;
    public int id;
    public bool AvailableWithoutTransport;//found in div rules 
    public int Numinpack;//found in div rules 
    public float[] NuminpackMod = new float[4];//found in div rules 
    public int NumAvaible; // found in div
    public List<transport> transports = new List<transport>();//found in div rules


        public Unit()
		{
		}
	}
