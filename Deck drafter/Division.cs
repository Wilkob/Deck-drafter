using System;


	public class Division
	{
		string name;
    public int id;
    public string UserName; 
		public int[] logi;
		public int[] inf;
		public int[] art;
		public int[] tank;
		public int[] rec;
    public int[] aa;
    public int[] hel;
    public int[] air;
    public List<Unit> LogiUnits = new List<Unit>();
    public List<Unit> infUnits = new List<Unit>();
    public List<Unit> artUnits = new List<Unit>();
    public List<Unit> TankUnits = new List<Unit>();
    public List<Unit> RecUnits = new List<Unit>();
    public List<Unit> AAUnits = new List<Unit>();
    public List<Unit> helUnits = new List<Unit>();
    public List<Unit> airUnits = new List<Unit>();
    public List<Unit> OverflowUnits = new List<Unit>();
    public Division()
        {
        }
    public void setname (string inname) { name = inname; }
    public string getname () { return name; }

    public void SetCosts (int[] logi, int[] inf, int[] art, int[] tank, int[] rec, int[] aa, int[] hel, int[] air)
    {
        this.logi = logi;
        this.inf = inf;
        this.art = art;
        this.tank = tank;
        this.rec = rec;
        this.aa = aa;
        this.hel = hel;
        this.air = air;
    }


     
}
