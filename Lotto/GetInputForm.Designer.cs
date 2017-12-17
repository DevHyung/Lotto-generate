namespace Lotto
{
    partial class GetInputForm
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
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.num2 = new System.Windows.Forms.NumericUpDown();
            this.num3 = new System.Windows.Forms.NumericUpDown();
            this.num5 = new System.Windows.Forms.NumericUpDown();
            this.num4 = new System.Windows.Forms.NumericUpDown();
            this.num6 = new System.Windows.Forms.NumericUpDown();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num6)).BeginInit();
            this.SuspendLayout();
            // 
            // num1
            // 
            this.num1.Location = new System.Drawing.Point(22, 31);
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(40, 25);
            this.num1.TabIndex = 0;
            this.num1.ValueChanged += new System.EventHandler(this.num1_ValueChanged);
            // 
            // num2
            // 
            this.num2.Location = new System.Drawing.Point(68, 31);
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(40, 25);
            this.num2.TabIndex = 1;
            this.num2.ValueChanged += new System.EventHandler(this.num2_ValueChanged);
            // 
            // num3
            // 
            this.num3.Location = new System.Drawing.Point(114, 31);
            this.num3.Name = "num3";
            this.num3.Size = new System.Drawing.Size(40, 25);
            this.num3.TabIndex = 2;
            this.num3.ValueChanged += new System.EventHandler(this.num3_ValueChanged);
            // 
            // num5
            // 
            this.num5.Location = new System.Drawing.Point(206, 31);
            this.num5.Name = "num5";
            this.num5.Size = new System.Drawing.Size(40, 25);
            this.num5.TabIndex = 4;
            this.num5.ValueChanged += new System.EventHandler(this.num5_ValueChanged);
            // 
            // num4
            // 
            this.num4.Location = new System.Drawing.Point(160, 31);
            this.num4.Name = "num4";
            this.num4.Size = new System.Drawing.Size(40, 25);
            this.num4.TabIndex = 3;
            this.num4.ValueChanged += new System.EventHandler(this.num4_ValueChanged);
            // 
            // num6
            // 
            this.num6.Location = new System.Drawing.Point(252, 31);
            this.num6.Name = "num6";
            this.num6.Size = new System.Drawing.Size(40, 25);
            this.num6.TabIndex = 5;
            this.num6.ValueChanged += new System.EventHandler(this.num6_ValueChanged);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(310, 31);
            this.OK.Name = "OK";
            this.OK.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 6;
            this.OK.Text = "추가";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(391, 31);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "취소";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // GetInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 82);
            this.ControlBox = false;
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.num6);
            this.Controls.Add(this.num4);
            this.Controls.Add(this.num5);
            this.Controls.Add(this.num3);
            this.Controls.Add(this.num2);
            this.Controls.Add(this.num1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetInputForm";
            this.ShowInTaskbar = false;
            this.Text = "임의 번호 입력";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown num1;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.NumericUpDown num3;
        private System.Windows.Forms.NumericUpDown num5;
        private System.Windows.Forms.NumericUpDown num4;
        private System.Windows.Forms.NumericUpDown num6;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
    }
}