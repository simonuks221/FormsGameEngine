namespace FormsGameEngineFormExample
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
            this.EnemyAiCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // EnemyAiCheckBox
            // 
            this.EnemyAiCheckBox.AutoSize = true;
            this.EnemyAiCheckBox.Checked = true;
            this.EnemyAiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnemyAiCheckBox.Location = new System.Drawing.Point(695, 27);
            this.EnemyAiCheckBox.Name = "EnemyAiCheckBox";
            this.EnemyAiCheckBox.Size = new System.Drawing.Size(70, 17);
            this.EnemyAiCheckBox.TabIndex = 0;
            this.EnemyAiCheckBox.Text = "Enemy Ai";
            this.EnemyAiCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(842, 445);
            this.Controls.Add(this.EnemyAiCheckBox);
            this.Name = "Form1";
            this.Text = "Ping pong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EnemyAiCheckBox;
    }
}

