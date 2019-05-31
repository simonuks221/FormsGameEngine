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
            this.VictoryScoreAmountTextBox = new System.Windows.Forms.TextBox();
            this.BallSpeedTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EnemyAiCheckBox
            // 
            this.EnemyAiCheckBox.AutoSize = true;
            this.EnemyAiCheckBox.Checked = true;
            this.EnemyAiCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnemyAiCheckBox.Location = new System.Drawing.Point(682, 28);
            this.EnemyAiCheckBox.Name = "EnemyAiCheckBox";
            this.EnemyAiCheckBox.Size = new System.Drawing.Size(70, 17);
            this.EnemyAiCheckBox.TabIndex = 0;
            this.EnemyAiCheckBox.Text = "Enemy Ai";
            this.EnemyAiCheckBox.UseVisualStyleBackColor = true;
            // 
            // VictoryScoreAmountTextBox
            // 
            this.VictoryScoreAmountTextBox.Location = new System.Drawing.Point(682, 50);
            this.VictoryScoreAmountTextBox.Name = "VictoryScoreAmountTextBox";
            this.VictoryScoreAmountTextBox.Size = new System.Drawing.Size(37, 20);
            this.VictoryScoreAmountTextBox.TabIndex = 1;
            this.VictoryScoreAmountTextBox.TextChanged += new System.EventHandler(this.VictoryScoreAmountTextBox_TextChanged);
            // 
            // BallSpeedTextBox
            // 
            this.BallSpeedTextBox.Location = new System.Drawing.Point(682, 76);
            this.BallSpeedTextBox.Name = "BallSpeedTextBox";
            this.BallSpeedTextBox.Size = new System.Drawing.Size(37, 20);
            this.BallSpeedTextBox.TabIndex = 2;
            this.BallSpeedTextBox.TextChanged += new System.EventHandler(this.BallSpeedTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(724, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Victory score amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(725, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ball speed";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(842, 445);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BallSpeedTextBox);
            this.Controls.Add(this.VictoryScoreAmountTextBox);
            this.Controls.Add(this.EnemyAiCheckBox);
            this.Name = "Form1";
            this.Text = "Ping pong";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EnemyAiCheckBox;
        private System.Windows.Forms.TextBox VictoryScoreAmountTextBox;
        private System.Windows.Forms.TextBox BallSpeedTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

