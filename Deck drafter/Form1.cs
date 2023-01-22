namespace Deck_drafter
{

    public partial class Form1 : Form
    {
        public FileOpener fileOpener = new FileOpener();
        public DeckEncoder DeckEncoder = new DeckEncoder();
        public DraftController Drafter;
        public Form1()
        {
            fileOpener.run();
            Drafter = new DraftController(fileOpener);
            
            InitializeComponent();
            setDivDraft();
            //this.Bob.Text = Convert.ToString(fileOpener.Divisionlist[4].logi[2]);

        }


        void setDivDraft() 
        {
            Drafter.newDivDraft();
            string CDName1 = Drafter.DraftDivC1.getname();
            string CDName2 = Drafter.DraftDivC2.getname();
            string CDName3 = Drafter.DraftDivC3.getname();
            this.CDiv1.Text = CDName1;
            this.CDiv2.Text = CDName2;
            this.CDiv3.Text = CDName3;
            this.DivDraft.Show();
            this.UnitDraft.Hide();
            
        }

        private void pickdiv1_Click(object sender, EventArgs e)
        {
            SetActiveDiv(Drafter.DraftDivC1);
        }

        private void SetActiveDiv(Division division)
        {
            Drafter.DraftPick = division;
            
            this.Bob.Text = Drafter.DraftPick.getname().Substring(18);
            this.DivDraft.Hide();
            this.UnitDraft.Show();
            setDeckTabs();
            Drafter.setslots();

            //SetDeckCode();
            setUnitDraft();

        }

        private void pickdiv2_Click(object sender, EventArgs e)
        {
            SetActiveDiv(Drafter.DraftDivC2);
        }

        private void pickdiv3_Click(object sender, EventArgs e)
        {
            SetActiveDiv(Drafter.DraftDivC3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Drafter.Reset();
            setDivDraft();
            SetDeckCode();
            DraftButton1.Enabled = true;
            DraftButton2.Enabled = true;
            DraftButton3.Enabled = true;
        }
        void setUnitDraft()
        {
            DraftButton1.Enabled = false;
            DraftButton2.Enabled = false;
            DraftButton3.Enabled = false;

            EXPE1.Checked = false;
            EXPV1.Checked = false;
            EXPT1.Checked = false;
            EXPP1.Checked = false;

            EXPE2.Checked = false;
            EXPV2.Checked = false;
            EXPT2.Checked = false;
            EXPP2.Checked = false;

            EXPE3.Checked = false;
            EXPV3.Checked = false;
            EXPT3.Checked = false;
            EXPP3.Checked = false;

            Drafter.newUnitDraft();
            this.UnitName1.Text = Drafter.draftUnitC1.BaseUnit.unitname.Substring(18);
            this.UnitName2.Text = Drafter.draftUnitC2.BaseUnit.unitname.Substring(18);
            this.UnitName3.Text = Drafter.draftUnitC3.BaseUnit.unitname.Substring(18);
            this.EXPE1.Text = Convert.ToString(Math.Round(Drafter.draftUnitC1.BaseUnit.Numinpack * Drafter.draftUnitC1.BaseUnit.NuminpackMod[3]));
            this.EXPV1.Text = Convert.ToString(Math.Round(Drafter.draftUnitC1.BaseUnit.Numinpack * Drafter.draftUnitC1.BaseUnit.NuminpackMod[2]));
            this.EXPT1.Text = Convert.ToString(Math.Round(Drafter.draftUnitC1.BaseUnit.Numinpack * Drafter.draftUnitC1.BaseUnit.NuminpackMod[1]));
            this.EXPP1.Text = Convert.ToString(Math.Round(Drafter.draftUnitC1.BaseUnit.Numinpack * Drafter.draftUnitC1.BaseUnit.NuminpackMod[0]));

            this.EXPE2.Text = Convert.ToString(Math.Round(Drafter.draftUnitC2.BaseUnit.Numinpack * Drafter.draftUnitC2.BaseUnit.NuminpackMod[3]));
            this.EXPV2.Text = Convert.ToString(Math.Round(Drafter.draftUnitC2.BaseUnit.Numinpack * Drafter.draftUnitC2.BaseUnit.NuminpackMod[2]));
            this.EXPT2.Text = Convert.ToString(Math.Round(Drafter.draftUnitC2.BaseUnit.Numinpack * Drafter.draftUnitC2.BaseUnit.NuminpackMod[1]));
            this.EXPP2.Text = Convert.ToString(Math.Round(Drafter.draftUnitC2.BaseUnit.Numinpack * Drafter.draftUnitC2.BaseUnit.NuminpackMod[0]));

            this.EXPE3.Text = Convert.ToString(Math.Round(Drafter.draftUnitC3.BaseUnit.Numinpack * Drafter.draftUnitC3.BaseUnit.NuminpackMod[3]));
            this.EXPV3.Text = Convert.ToString(Math.Round(Drafter.draftUnitC3.BaseUnit.Numinpack * Drafter.draftUnitC3.BaseUnit.NuminpackMod[2]));
            this.EXPT3.Text = Convert.ToString(Math.Round(Drafter.draftUnitC3.BaseUnit.Numinpack * Drafter.draftUnitC3.BaseUnit.NuminpackMod[1]));
            this.EXPP3.Text = Convert.ToString(Math.Round(Drafter.draftUnitC3.BaseUnit.Numinpack * Drafter.draftUnitC3.BaseUnit.NuminpackMod[0]));

            if (EXPE1.Text == "0") { EXPE1.Enabled = false; } else { EXPE1.Enabled = true; }
            if (EXPV1.Text == "0") { EXPV1.Enabled = false; } else { EXPV1.Enabled = true; }
            if (EXPT1.Text == "0") { EXPT1.Enabled = false; } else { EXPT1.Enabled = true; }
            if (EXPP1.Text == "0") { EXPP1.Enabled = false; } else { EXPP1.Enabled = true; }

            if (EXPE2.Text == "0") { EXPE2.Enabled = false; } else { EXPE2.Enabled = true; }
            if (EXPV2.Text == "0") { EXPV2.Enabled = false; } else { EXPV2.Enabled = true; }
            if (EXPT2.Text == "0") { EXPT2.Enabled = false; } else { EXPT2.Enabled = true; }
            if (EXPP2.Text == "0") { EXPP2.Enabled = false; } else { EXPP2.Enabled = true; }

            if (EXPE3.Text == "0") { EXPE3.Enabled = false; } else { EXPE3.Enabled = true; }
            if (EXPV3.Text == "0") { EXPV3.Enabled = false; } else { EXPV3.Enabled = true; }
            if (EXPT3.Text == "0") { EXPT3.Enabled = false; } else { EXPT3.Enabled = true; }
            if (EXPP3.Text == "0") { EXPP3.Enabled = false; } else { EXPP3.Enabled = true; }

            if(Drafter.draftUnitC1.transport != null) 
            { 
            Transport1.Text = Drafter.draftUnitC1.transport.unitname.Substring(16);
            }
            else { Transport1.Text = " "; }

            if (Drafter.draftUnitC2.transport != null)
            {
                Transport2.Text = Drafter.draftUnitC2.transport.unitname.Substring(16);
            }
            else { Transport2.Text = " "; }

            if (Drafter.draftUnitC3.transport != null)
            {
                Transport3.Text = Drafter.draftUnitC3.transport.unitname.Substring(16);
            }
            else { Transport3.Text = " "; }


        }

        void setDeckTabs()
        {
            string Data1 = "";
            string Data2 = "";
            this.ActivionPoints.Text = Convert.ToString(Drafter.DivPoints); 
            foreach (int i in Drafter.DraftPick.logi) 
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.LogiUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null){ Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.LogiData.Text = Data1;
            this.LogiData2.Text = Data2;

            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.inf)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.infUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.InfData1.Text = Data1;
            this.InfData2.Text = Data2;

            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.art)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.artUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.artData1.Text = Data1;
            this.artData2.Text = Data2;

            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.tank)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.TankUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.tnkData1.Text = Data1;
            this.tnkData2.Text = Data2;


            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.rec)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.RecUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.redData1.Text = Data1;
            this.redData2.Text = Data2;

            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.aa)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.AAUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.aaData1.Text = Data1;
            this.aaData2.Text = Data2;

            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.hel)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.helUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.helData1.Text = Data1;
            this.helData2.Text = Data2;

            Data1 = "";
            Data2 = "";
            foreach (int i in Drafter.DraftPick.air)
            {
                Data1 = Data1 + i.ToString() + "\n" + "\n";
            }
            foreach (DraftUnit i in Drafter.airUnitsDrafted)
            {
                Data2 = Data2 + i.BaseUnit.unitname.Substring(18);
                if (i.transport != null) { Data2 = Data2 + " Tranport: " + i.transport.unitname.Substring(16); }
                Data2 = Data2 + "\n" + "\n";
            }
            this.airData1.Text = Data1;
            this.airData2.Text = Data2;


        }

        private void EXPE3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPE3.Checked) 
            {
                //EXPE3.Checked = false;
                EXPV3.Checked = false;
                EXPT3.Checked = false;
                EXPP3.Checked = false;
                DraftButton3.Enabled = true;
            }
            else { DraftButton3.Enabled = false; }
        }

        private void EXPV3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPV3.Checked)
            {
                EXPE3.Checked = false;
                //EXPV3.Checked = false;
                EXPT3.Checked = false;
                EXPP3.Checked = false;
                DraftButton3.Enabled = true;
            }
            else { DraftButton3.Enabled = false; }
        }

        private void EXPT3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPT3.Checked)
            {
                EXPE3.Checked = false;
                EXPV3.Checked = false;
                //EXPT3.Checked = false;
                EXPP3.Checked = false;
                DraftButton3.Enabled = true;
            }
            else { DraftButton3.Enabled = false; }
        }

        private void EXPP3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPP3.Checked)
            {
                EXPE3.Checked = false;
                EXPV3.Checked = false;
                EXPT3.Checked = false;
                //EXPP3.Checked = false;
                DraftButton3.Enabled = true;
            }
            else { DraftButton3.Enabled = false; }
        }

        private void EXPE2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPE2.Checked)
            {
                //EXPE2.Checked = false;
                EXPV2.Checked = false;
                EXPT2.Checked = false;
                EXPP2.Checked = false;
                DraftButton2.Enabled = true;
            }
            else { DraftButton2.Enabled = false; }
        }

        private void EXPV2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPV2.Checked)
            {
                EXPE2.Checked = false;
                //EXPV2.Checked = false;
                EXPT2.Checked = false;
                EXPP2.Checked = false;
                DraftButton2.Enabled = true;
            }
            else { DraftButton2.Enabled = false; }
        }

        private void EXPT2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPT2.Checked)
            {
                EXPE2.Checked = false;
                EXPV2.Checked = false;
                //EXPT2.Checked = false;
                EXPP2.Checked = false;
                DraftButton2.Enabled = true;
            }
            else { DraftButton2.Enabled = false; }
        }

        private void EXPP2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPP2.Checked)
            {
                EXPE2.Checked = false;
                EXPV2.Checked = false;
                EXPT2.Checked = false;
                //EXPP2.Checked = false;
                DraftButton2.Enabled = true;
            }
            else { DraftButton2.Enabled = false; }
        }

        private void EXPE1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPE1.Checked)
            {
                //EXPE1.Checked = false;
                EXPV1.Checked = false;
                EXPT1.Checked = false;
                EXPP1.Checked = false;
                DraftButton1.Enabled = true;
            }
            else { DraftButton1.Enabled = false; }
        }

        private void EXPV1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPV1.Checked)
            {
                EXPE1.Checked = false;
                //EXPV1.Checked = false;
                EXPT1.Checked = false;
                EXPP1.Checked = false;
                DraftButton1.Enabled = true;
            }
            else { DraftButton1.Enabled = false; }
        }

        private void EXPT1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPT1.Checked)
            {
                EXPE1.Checked = false;
                EXPV1.Checked = false;
                //EXPT1.Checked = false;
                EXPP1.Checked = false;
                DraftButton1.Enabled = true;
            }
            else { DraftButton1.Enabled = false; }
        }

        private void EXPP1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.EXPP1.Checked)
            {
                EXPE1.Checked = false;
                EXPV1.Checked = false;
                EXPT1.Checked = false;
                //EXPP1.Checked = false;
                DraftButton1.Enabled = true;
            }
            else { DraftButton1.Enabled = false; }
        }
        private void SetDrafted(DraftUnit draftUnit)
        {
            Drafter.draftedUnit(draftUnit);
            setDeckTabs();
            if (Drafter.checkslots()) { setUnitDraft(); }
            else
            {
                enddraft();
            }
            SetDeckCode();

        }

        private void SetDeckCode()
        {
            this.DeckCode.Text = DeckEncoder.encodedeck(Drafter);
        }

        private void DraftButton1_Click(object sender, EventArgs e)
        {
            int level = 0;
            if (EXPE1.Checked == true) { level = 3; }
            if (EXPV1.Checked == true) { level = 2; }
            if (EXPT1.Checked == true) { level = 1; }
            if (EXPP1.Checked == true) { level = 0; }
            Drafter.draftUnitC1.Level = level;
            SetDrafted(Drafter.draftUnitC1);
        }



        private void DraftButton2_Click(object sender, EventArgs e)
        {
            int level = 0;
            if (EXPE2.Checked == true) { level = 3; }
            if (EXPV2.Checked == true) { level = 2; }
            if (EXPT2.Checked == true) { level = 1; }
            if (EXPP2.Checked == true) { level = 0; }
            Drafter.draftUnitC2.Level = level;

            SetDrafted(Drafter.draftUnitC2);
        }

        private void DraftButton3_Click(object sender, EventArgs e)
        {
            int level = 0;

            if (EXPE3.Checked == true){ level = 3;}
            if (EXPV3.Checked == true) { level = 2; }
            if (EXPT3.Checked == true) { level = 1; }
            if (EXPP3.Checked == true) { level = 0; }
            Drafter.draftUnitC3.Level = level;

            SetDrafted(Drafter.draftUnitC3);
        }

        void enddraft() 
        {
            DraftButton1.Enabled = false;
            DraftButton2.Enabled = false;
            DraftButton3.Enabled = false;
            string message = "Draft Finished";
            MessageBox.Show(message);
        }


    }
}