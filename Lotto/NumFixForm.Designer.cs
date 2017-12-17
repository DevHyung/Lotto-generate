namespace Lotto
{
    partial class NumFixForm
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
            this.Fix = new System.Windows.Forms.CheckedListBox();
            this.FixGroup = new System.Windows.Forms.GroupBox();
            this.ExcludeGroup = new System.Windows.Forms.GroupBox();
            this.Exclude = new System.Windows.Forms.CheckedListBox();
            this.Button_Ok = new System.Windows.Forms.Button();
            this.FixGroup.SuspendLayout();
            this.ExcludeGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fix
            // 
            this.Fix.BackColor = System.Drawing.SystemColors.Control;
            this.Fix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Fix.CheckOnClick = true;
            this.Fix.ColumnWidth = 40;
            this.Fix.FormattingEnabled = true;
            this.Fix.Location = new System.Drawing.Point(24, 29);
            this.Fix.MultiColumn = true;
            this.Fix.Name = "Fix";
            this.Fix.Size = new System.Drawing.Size(347, 140);
            this.Fix.TabIndex = 0;
            this.Fix.TabStop = false;
            this.Fix.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Fix_ItemCheck);
            // 
            // FixGroup
            // 
            this.FixGroup.Controls.Add(this.Fix);
            this.FixGroup.Location = new System.Drawing.Point(22, 24);
            this.FixGroup.Name = "FixGroup";
            this.FixGroup.Size = new System.Drawing.Size(489, 203);
            this.FixGroup.TabIndex = 1;
            this.FixGroup.TabStop = false;
            this.FixGroup.Text = "고정";
            // 
            // ExcludeGroup
            // 
            this.ExcludeGroup.Controls.Add(this.Exclude);
            this.ExcludeGroup.Location = new System.Drawing.Point(22, 249);
            this.ExcludeGroup.Name = "ExcludeGroup";
            this.ExcludeGroup.Size = new System.Drawing.Size(489, 203);
            this.ExcludeGroup.TabIndex = 7;
            this.ExcludeGroup.TabStop = false;
            this.ExcludeGroup.Text = "제외";
            // 
            // Exclude
            // 
            this.Exclude.BackColor = System.Drawing.SystemColors.Control;
            this.Exclude.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Exclude.CheckOnClick = true;
            this.Exclude.ColumnWidth = 40;
            this.Exclude.FormattingEnabled = true;
            this.Exclude.Location = new System.Drawing.Point(24, 29);
            this.Exclude.MultiColumn = true;
            this.Exclude.Name = "Exclude";
            this.Exclude.Size = new System.Drawing.Size(401, 140);
            this.Exclude.TabIndex = 0;
            this.Exclude.TabStop = false;
            this.Exclude.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Exclude_ItemCheck);
            // 
            // Button_Ok
            // 
            this.Button_Ok.Location = new System.Drawing.Point(548, 103);
            this.Button_Ok.Name = "Button_Ok";
            this.Button_Ok.Size = new System.Drawing.Size(129, 38);
            this.Button_Ok.TabIndex = 8;
            this.Button_Ok.Text = "확인";
            this.Button_Ok.UseVisualStyleBackColor = true;
            this.Button_Ok.Click += new System.EventHandler(this.Button_Ok_Click);
            // 
            // NumFixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 474);
            this.ControlBox = false;
            this.Controls.Add(this.Button_Ok);
            this.Controls.Add(this.ExcludeGroup);
            this.Controls.Add(this.FixGroup);
            this.Name = "NumFixForm";
            this.Text = "NumFixForm";
            this.FixGroup.ResumeLayout(false);
            this.ExcludeGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox Fix;
        private System.Windows.Forms.GroupBox FixGroup;
        private System.Windows.Forms.GroupBox ExcludeGroup;
        private System.Windows.Forms.CheckedListBox Exclude;
        private System.Windows.Forms.Button Button_Ok;
    }
}