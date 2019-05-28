namespace TestingProject
{
    partial class Form1
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
            this.buttonOpenInventor = new System.Windows.Forms.Button();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.buttonGetEdgeNumber = new System.Windows.Forms.Button();
            this.buttonGetFaceNumber = new System.Windows.Forms.Button();
            this.buttonAssemble = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.textBoxSample = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOpenInventor
            // 
            this.buttonOpenInventor.Location = new System.Drawing.Point(49, 31);
            this.buttonOpenInventor.Name = "buttonOpenInventor";
            this.buttonOpenInventor.Size = new System.Drawing.Size(79, 80);
            this.buttonOpenInventor.TabIndex = 17;
            this.buttonOpenInventor.Text = "Запустить Inventor";
            this.buttonOpenInventor.UseVisualStyleBackColor = true;
            this.buttonOpenInventor.Click += new System.EventHandler(this.ButtonOpenInventor_Click);
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(220, 31);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(79, 80);
            this.buttonBuild.TabIndex = 18;
            this.buttonBuild.Text = "Построить";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.ButtonBuild_Click);
            // 
            // buttonGetEdgeNumber
            // 
            this.buttonGetEdgeNumber.Location = new System.Drawing.Point(49, 144);
            this.buttonGetEdgeNumber.Name = "buttonGetEdgeNumber";
            this.buttonGetEdgeNumber.Size = new System.Drawing.Size(79, 80);
            this.buttonGetEdgeNumber.TabIndex = 19;
            this.buttonGetEdgeNumber.Text = "Номер ребра";
            this.buttonGetEdgeNumber.UseVisualStyleBackColor = true;
            this.buttonGetEdgeNumber.Click += new System.EventHandler(this.ButtonGetEdgeNumber_Click);
            // 
            // buttonGetFaceNumber
            // 
            this.buttonGetFaceNumber.Location = new System.Drawing.Point(220, 144);
            this.buttonGetFaceNumber.Name = "buttonGetFaceNumber";
            this.buttonGetFaceNumber.Size = new System.Drawing.Size(79, 80);
            this.buttonGetFaceNumber.TabIndex = 20;
            this.buttonGetFaceNumber.Text = "Номер поверхности";
            this.buttonGetFaceNumber.UseVisualStyleBackColor = true;
            this.buttonGetFaceNumber.Click += new System.EventHandler(this.ButtonGetFaceNumber_Click);
            // 
            // buttonAssemble
            // 
            this.buttonAssemble.Location = new System.Drawing.Point(372, 31);
            this.buttonAssemble.Name = "buttonAssemble";
            this.buttonAssemble.Size = new System.Drawing.Size(79, 80);
            this.buttonAssemble.TabIndex = 21;
            this.buttonAssemble.Text = "Собрать";
            this.buttonAssemble.UseVisualStyleBackColor = true;
            this.buttonAssemble.Click += new System.EventHandler(this.ButtonAssemble_Click);
            // 
            // textBoxSample
            // 
            this.textBoxSample.Location = new System.Drawing.Point(372, 175);
            this.textBoxSample.Name = "textBoxSample";
            this.textBoxSample.Size = new System.Drawing.Size(79, 20);
            this.textBoxSample.TabIndex = 22;
            this.textBoxSample.Text = "11.1";
            this.textBoxSample.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 304);
            this.Controls.Add(this.textBoxSample);
            this.Controls.Add(this.buttonAssemble);
            this.Controls.Add(this.buttonGetFaceNumber);
            this.Controls.Add(this.buttonGetEdgeNumber);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.buttonOpenInventor);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Конфигуратор";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOpenInventor;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Button buttonGetEdgeNumber;
        private System.Windows.Forms.Button buttonGetFaceNumber;
        private System.Windows.Forms.Button buttonAssemble;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox textBoxSample;
    }
}

