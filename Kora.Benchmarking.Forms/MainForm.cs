﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UAM.Kora;

namespace UAM.Kora.Forms
{
    public partial class MainForm : Form
    {
        private int Start { get { return (int)StartControl.Value; } }
        private int Count { get { return (int)CountControl.Value; } }
        private int Step { get { return (int)StepControl.Value; } }
        private int Control { get { return (int)ControlCountControl.Value; } }

        public MainForm()
        {
            InitializeComponent();
            // setup proper names
            RBTreeCheck.Text = Properties.Resources.RBTree;
            VEBCheck.Text = Properties.Resources.VEB;
            XTrieDPHCheck.Text = Properties.Resources.XTrieDPH;
            YTrieDPHCheck.Text = Properties.Resources.YTrieDPH;
            XTrieStandardCheck.Text = Properties.Resources.XTrieStandard;
            YTrieStandardCheck.Text = Properties.Resources.YTrieStandard;
        }

        private void ForwardClicked(object sender, EventArgs e)
        {
            BenchmarkType currentType  = GetBenchmarkType();
            ChartForm chart = new ChartForm(currentType, MarkedTypes(), Start, Count, Step, Control);
            chart.ShowDialog();
        }

        private StructureType MarkedTypes()
        {
            StructureType marked = 0;
            marked |= (RBTreeCheck.Checked ? StructureType.RBTree : marked);
            marked |= (VEBCheck.Checked ? StructureType.VEB : marked);
            marked |= (XTrieDPHCheck.Checked ? StructureType.XTrieDPH : marked);
            marked |= (YTrieDPHCheck.Checked ? StructureType.YTrieDPH : marked);
            marked |= (XTrieStandardCheck.Checked ? StructureType.XTrieStandard : marked);
            marked |= (YTrieStandardCheck.Checked ? StructureType.YTrieStandard : marked);
            return marked;
        }

        private BenchmarkType GetBenchmarkType()
        {
            if (AddCheckButton.Checked)
                return BenchmarkType.Add;
            if (DeleteCheckButton.Checked)
                return BenchmarkType.Delete;
            if (SearchCheckButton.Checked)
                return BenchmarkType.Search;
            if (SuccessorCheckButton.Checked)
                return BenchmarkType.Successor;
            if (MemoryCheckButton.Checked)
                return BenchmarkType.Memory;
            return BenchmarkType.Unknown;
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            RadioButton source = (RadioButton)sender;
            if (!source.Checked)
                return;
            if(source == SearchCheckButton || source == SuccessorCheckButton)
            {
                ControlCountControl.Visible = true;
                ControlCountLabel.Visible = true;
            }
            else
            {
                ControlCountControl.Visible = false;
                ControlCountLabel.Visible = false;
            }
        }
    }
}
