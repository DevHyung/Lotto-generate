namespace Lotto
{
    partial class statForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LDigitView = new System.Windows.Forms.DataGridView();
            this.NumFreqView = new System.Windows.Forms.DataGridView();
            this.UnShownGridView = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ConsView = new System.Windows.Forms.DataGridView();
            this.OddEvenView = new System.Windows.Forms.DataGridView();
            this.Mod5FreqView = new System.Windows.Forms.DataGridView();
            this.HighLowView = new System.Windows.Forms.DataGridView();
            this.SumFreqView = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.LSumView = new System.Windows.Forms.DataGridView();
            this.MSumView = new System.Windows.Forms.DataGridView();
            this.FirstLastView = new System.Windows.Forms.DataGridView();
            this.RearView = new System.Windows.Forms.DataGridView();
            this.DistView = new System.Windows.Forms.DataGridView();
            this.FrontSumView = new System.Windows.Forms.DataGridView();
            this.SearchFrom = new System.Windows.Forms.NumericUpDown();
            this.SearchTo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchByPeriodButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LastPeriod = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LDigitView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumFreqView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnShownGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OddEvenView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mod5FreqView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighLowView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SumFreqView)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LSumView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSumView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLastView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontSumView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LastPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1116, 738);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LDigitView);
            this.tabPage1.Controls.Add(this.NumFreqView);
            this.tabPage1.Controls.Add(this.UnShownGridView);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1108, 709);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "통계1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LDigitView
            // 
            this.LDigitView.AllowUserToAddRows = false;
            this.LDigitView.AllowUserToDeleteRows = false;
            this.LDigitView.AllowUserToResizeColumns = false;
            this.LDigitView.AllowUserToResizeRows = false;
            this.LDigitView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.LDigitView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.LDigitView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LDigitView.Location = new System.Drawing.Point(633, 423);
            this.LDigitView.Name = "LDigitView";
            this.LDigitView.ReadOnly = true;
            this.LDigitView.RowHeadersVisible = false;
            this.LDigitView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.LDigitView.RowTemplate.Height = 27;
            this.LDigitView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.LDigitView.Size = new System.Drawing.Size(462, 275);
            this.LDigitView.TabIndex = 7;
            this.LDigitView.TabStop = false;
            this.LDigitView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.LDigitView_CellPainting);
            // 
            // NumFreqView
            // 
            this.NumFreqView.AllowUserToAddRows = false;
            this.NumFreqView.AllowUserToDeleteRows = false;
            this.NumFreqView.AllowUserToResizeColumns = false;
            this.NumFreqView.AllowUserToResizeRows = false;
            this.NumFreqView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.NumFreqView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.NumFreqView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.NumFreqView.GridColor = System.Drawing.SystemColors.Control;
            this.NumFreqView.Location = new System.Drawing.Point(633, 10);
            this.NumFreqView.Name = "NumFreqView";
            this.NumFreqView.ReadOnly = true;
            this.NumFreqView.RowHeadersVisible = false;
            this.NumFreqView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.NumFreqView.RowTemplate.Height = 27;
            this.NumFreqView.Size = new System.Drawing.Size(462, 400);
            this.NumFreqView.TabIndex = 6;
            this.NumFreqView.TabStop = false;
            this.NumFreqView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.NumFreqView_CellPainting);
            // 
            // UnShownGridView
            // 
            this.UnShownGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.UnShownGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.UnShownGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UnShownGridView.Location = new System.Drawing.Point(10, 523);
            this.UnShownGridView.Name = "UnShownGridView";
            this.UnShownGridView.ReadOnly = true;
            this.UnShownGridView.RowHeadersVisible = false;
            this.UnShownGridView.RowTemplate.Height = 27;
            this.UnShownGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.UnShownGridView.Size = new System.Drawing.Size(617, 175);
            this.UnShownGridView.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(617, 494);
            this.dataGridView1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ConsView);
            this.tabPage2.Controls.Add(this.OddEvenView);
            this.tabPage2.Controls.Add(this.Mod5FreqView);
            this.tabPage2.Controls.Add(this.HighLowView);
            this.tabPage2.Controls.Add(this.SumFreqView);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1108, 709);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "통계2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ConsView
            // 
            this.ConsView.AllowUserToAddRows = false;
            this.ConsView.AllowUserToDeleteRows = false;
            this.ConsView.AllowUserToResizeColumns = false;
            this.ConsView.AllowUserToResizeRows = false;
            this.ConsView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ConsView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.ConsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConsView.Location = new System.Drawing.Point(570, 450);
            this.ConsView.Name = "ConsView";
            this.ConsView.ReadOnly = true;
            this.ConsView.RowHeadersVisible = false;
            this.ConsView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ConsView.RowTemplate.Height = 27;
            this.ConsView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ConsView.Size = new System.Drawing.Size(525, 175);
            this.ConsView.TabIndex = 11;
            this.ConsView.TabStop = false;
            this.ConsView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.ConsView_CellPainting);
            // 
            // OddEvenView
            // 
            this.OddEvenView.AllowUserToAddRows = false;
            this.OddEvenView.AllowUserToDeleteRows = false;
            this.OddEvenView.AllowUserToResizeColumns = false;
            this.OddEvenView.AllowUserToResizeRows = false;
            this.OddEvenView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.OddEvenView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.OddEvenView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OddEvenView.Location = new System.Drawing.Point(570, 230);
            this.OddEvenView.Name = "OddEvenView";
            this.OddEvenView.ReadOnly = true;
            this.OddEvenView.RowHeadersVisible = false;
            this.OddEvenView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.OddEvenView.RowTemplate.Height = 27;
            this.OddEvenView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.OddEvenView.Size = new System.Drawing.Size(525, 200);
            this.OddEvenView.TabIndex = 10;
            this.OddEvenView.TabStop = false;
            this.OddEvenView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.OddEvenView_CellPainting);
            // 
            // Mod5FreqView
            // 
            this.Mod5FreqView.AllowUserToAddRows = false;
            this.Mod5FreqView.AllowUserToDeleteRows = false;
            this.Mod5FreqView.AllowUserToResizeColumns = false;
            this.Mod5FreqView.AllowUserToResizeRows = false;
            this.Mod5FreqView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.Mod5FreqView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Mod5FreqView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Mod5FreqView.Location = new System.Drawing.Point(10, 375);
            this.Mod5FreqView.Name = "Mod5FreqView";
            this.Mod5FreqView.ReadOnly = true;
            this.Mod5FreqView.RowHeadersVisible = false;
            this.Mod5FreqView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.Mod5FreqView.RowTemplate.Height = 27;
            this.Mod5FreqView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Mod5FreqView.Size = new System.Drawing.Size(535, 250);
            this.Mod5FreqView.TabIndex = 9;
            this.Mod5FreqView.TabStop = false;
            this.Mod5FreqView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Mod5FreqView_CellPainting);
            // 
            // HighLowView
            // 
            this.HighLowView.AllowUserToAddRows = false;
            this.HighLowView.AllowUserToDeleteRows = false;
            this.HighLowView.AllowUserToResizeColumns = false;
            this.HighLowView.AllowUserToResizeRows = false;
            this.HighLowView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.HighLowView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.HighLowView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HighLowView.Location = new System.Drawing.Point(570, 10);
            this.HighLowView.Name = "HighLowView";
            this.HighLowView.ReadOnly = true;
            this.HighLowView.RowHeadersVisible = false;
            this.HighLowView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.HighLowView.RowTemplate.Height = 27;
            this.HighLowView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.HighLowView.Size = new System.Drawing.Size(525, 200);
            this.HighLowView.TabIndex = 8;
            this.HighLowView.TabStop = false;
            this.HighLowView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.HighLowView_CellPainting);
            // 
            // SumFreqView
            // 
            this.SumFreqView.AllowUserToAddRows = false;
            this.SumFreqView.AllowUserToDeleteRows = false;
            this.SumFreqView.AllowUserToResizeColumns = false;
            this.SumFreqView.AllowUserToResizeRows = false;
            this.SumFreqView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.SumFreqView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.SumFreqView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SumFreqView.Location = new System.Drawing.Point(10, 10);
            this.SumFreqView.Name = "SumFreqView";
            this.SumFreqView.ReadOnly = true;
            this.SumFreqView.RowHeadersVisible = false;
            this.SumFreqView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.SumFreqView.RowTemplate.Height = 27;
            this.SumFreqView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SumFreqView.Size = new System.Drawing.Size(535, 300);
            this.SumFreqView.TabIndex = 7;
            this.SumFreqView.TabStop = false;
            this.SumFreqView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.SumFreqView_CellPainting);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.LSumView);
            this.tabPage3.Controls.Add(this.MSumView);
            this.tabPage3.Controls.Add(this.FirstLastView);
            this.tabPage3.Controls.Add(this.RearView);
            this.tabPage3.Controls.Add(this.DistView);
            this.tabPage3.Controls.Add(this.FrontSumView);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1108, 709);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "통계3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // LSumView
            // 
            this.LSumView.AllowUserToAddRows = false;
            this.LSumView.AllowUserToDeleteRows = false;
            this.LSumView.AllowUserToResizeColumns = false;
            this.LSumView.AllowUserToResizeRows = false;
            this.LSumView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.LSumView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.LSumView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LSumView.Location = new System.Drawing.Point(570, 449);
            this.LSumView.Name = "LSumView";
            this.LSumView.ReadOnly = true;
            this.LSumView.RowHeadersVisible = false;
            this.LSumView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.LSumView.RowTemplate.Height = 27;
            this.LSumView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.LSumView.Size = new System.Drawing.Size(525, 150);
            this.LSumView.TabIndex = 13;
            this.LSumView.TabStop = false;
            this.LSumView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.LSumView_CellPainting);
            // 
            // MSumView
            // 
            this.MSumView.AllowUserToAddRows = false;
            this.MSumView.AllowUserToDeleteRows = false;
            this.MSumView.AllowUserToResizeColumns = false;
            this.MSumView.AllowUserToResizeRows = false;
            this.MSumView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.MSumView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.MSumView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MSumView.Location = new System.Drawing.Point(10, 449);
            this.MSumView.Name = "MSumView";
            this.MSumView.ReadOnly = true;
            this.MSumView.RowHeadersVisible = false;
            this.MSumView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.MSumView.RowTemplate.Height = 27;
            this.MSumView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.MSumView.Size = new System.Drawing.Size(525, 230);
            this.MSumView.TabIndex = 12;
            this.MSumView.TabStop = false;
            this.MSumView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.MSumView_CellPainting);
            // 
            // FirstLastView
            // 
            this.FirstLastView.AllowUserToAddRows = false;
            this.FirstLastView.AllowUserToDeleteRows = false;
            this.FirstLastView.AllowUserToResizeColumns = false;
            this.FirstLastView.AllowUserToResizeRows = false;
            this.FirstLastView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.FirstLastView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.FirstLastView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FirstLastView.Location = new System.Drawing.Point(570, 229);
            this.FirstLastView.Name = "FirstLastView";
            this.FirstLastView.ReadOnly = true;
            this.FirstLastView.RowHeadersVisible = false;
            this.FirstLastView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FirstLastView.RowTemplate.Height = 27;
            this.FirstLastView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.FirstLastView.Size = new System.Drawing.Size(525, 200);
            this.FirstLastView.TabIndex = 11;
            this.FirstLastView.TabStop = false;
            this.FirstLastView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.FirstLastView_CellPainting);
            // 
            // RearView
            // 
            this.RearView.AllowUserToAddRows = false;
            this.RearView.AllowUserToDeleteRows = false;
            this.RearView.AllowUserToResizeColumns = false;
            this.RearView.AllowUserToResizeRows = false;
            this.RearView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.RearView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.RearView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RearView.Location = new System.Drawing.Point(10, 229);
            this.RearView.Name = "RearView";
            this.RearView.ReadOnly = true;
            this.RearView.RowHeadersVisible = false;
            this.RearView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RearView.RowTemplate.Height = 27;
            this.RearView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.RearView.Size = new System.Drawing.Size(525, 200);
            this.RearView.TabIndex = 10;
            this.RearView.TabStop = false;
            this.RearView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.RearView_CellPainting);
            // 
            // DistView
            // 
            this.DistView.AllowUserToAddRows = false;
            this.DistView.AllowUserToDeleteRows = false;
            this.DistView.AllowUserToResizeColumns = false;
            this.DistView.AllowUserToResizeRows = false;
            this.DistView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DistView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DistView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DistView.Location = new System.Drawing.Point(570, 10);
            this.DistView.Name = "DistView";
            this.DistView.ReadOnly = true;
            this.DistView.RowHeadersVisible = false;
            this.DistView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DistView.RowTemplate.Height = 27;
            this.DistView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.DistView.Size = new System.Drawing.Size(525, 200);
            this.DistView.TabIndex = 9;
            this.DistView.TabStop = false;
            this.DistView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DistView_CellPainting);
            // 
            // FrontSumView
            // 
            this.FrontSumView.AllowUserToAddRows = false;
            this.FrontSumView.AllowUserToDeleteRows = false;
            this.FrontSumView.AllowUserToResizeColumns = false;
            this.FrontSumView.AllowUserToResizeRows = false;
            this.FrontSumView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.FrontSumView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.FrontSumView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FrontSumView.Location = new System.Drawing.Point(10, 10);
            this.FrontSumView.Name = "FrontSumView";
            this.FrontSumView.ReadOnly = true;
            this.FrontSumView.RowHeadersVisible = false;
            this.FrontSumView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FrontSumView.RowTemplate.Height = 27;
            this.FrontSumView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.FrontSumView.Size = new System.Drawing.Size(525, 200);
            this.FrontSumView.TabIndex = 8;
            this.FrontSumView.TabStop = false;
            this.FrontSumView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.FrontSumView_CellPainting);
            // 
            // SearchFrom
            // 
            this.SearchFrom.Location = new System.Drawing.Point(484, 11);
            this.SearchFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SearchFrom.Name = "SearchFrom";
            this.SearchFrom.Size = new System.Drawing.Size(77, 25);
            this.SearchFrom.TabIndex = 1;
            this.SearchFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SearchTo
            // 
            this.SearchTo.Location = new System.Drawing.Point(617, 11);
            this.SearchTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SearchTo.Name = "SearchTo";
            this.SearchTo.Size = new System.Drawing.Size(77, 25);
            this.SearchTo.TabIndex = 2;
            this.SearchTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(587, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "~";
            // 
            // SearchByPeriodButton
            // 
            this.SearchByPeriodButton.Location = new System.Drawing.Point(724, 11);
            this.SearchByPeriodButton.Name = "SearchByPeriodButton";
            this.SearchByPeriodButton.Size = new System.Drawing.Size(85, 26);
            this.SearchByPeriodButton.TabIndex = 4;
            this.SearchByPeriodButton.Text = "검색";
            this.SearchByPeriodButton.UseVisualStyleBackColor = true;
            this.SearchByPeriodButton.Click += new System.EventHandler(this.SearchByPeriodButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1014, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "검색";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LastPeriod
            // 
            this.LastPeriod.Location = new System.Drawing.Point(903, 11);
            this.LastPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LastPeriod.Name = "LastPeriod";
            this.LastPeriod.Size = new System.Drawing.Size(77, 25);
            this.LastPeriod.TabIndex = 5;
            this.LastPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(563, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "회";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(697, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "회";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(982, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "회";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(860, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "최근";
            // 
            // statForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 746);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LastPeriod);
            this.Controls.Add(this.SearchByPeriodButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchTo);
            this.Controls.Add(this.SearchFrom);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "statForm";
            this.Text = "통계";
            this.Load += new System.EventHandler(this.statForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LDigitView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumFreqView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnShownGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OddEvenView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mod5FreqView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighLowView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SumFreqView)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LSumView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MSumView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstLastView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RearView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontSumView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LastPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView UnShownGridView;
        private System.Windows.Forms.DataGridView NumFreqView;
        private System.Windows.Forms.DataGridView LDigitView;
        private System.Windows.Forms.DataGridView SumFreqView;
        private System.Windows.Forms.DataGridView ConsView;
        private System.Windows.Forms.DataGridView OddEvenView;
        private System.Windows.Forms.DataGridView Mod5FreqView;
        private System.Windows.Forms.DataGridView HighLowView;
        private System.Windows.Forms.DataGridView LSumView;
        private System.Windows.Forms.DataGridView MSumView;
        private System.Windows.Forms.DataGridView FirstLastView;
        private System.Windows.Forms.DataGridView RearView;
        private System.Windows.Forms.DataGridView DistView;
        private System.Windows.Forms.DataGridView FrontSumView;
        private System.Windows.Forms.NumericUpDown SearchFrom;
        private System.Windows.Forms.NumericUpDown SearchTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchByPeriodButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown LastPeriod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}