namespace Biblioteca
{
    partial class FrmBase
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
            pnlFormularioBase = new Panel();
            txtId = new TextBox();
            label1 = new Label();
            pnlFormularioBase.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFormularioBase
            // 
            pnlFormularioBase.Controls.Add(txtId);
            pnlFormularioBase.Controls.Add(label1);
            pnlFormularioBase.Dock = DockStyle.Left;
            pnlFormularioBase.Location = new Point(0, 0);
            pnlFormularioBase.Name = "pnlFormularioBase";
            pnlFormularioBase.Size = new Size(1143, 343);
            pnlFormularioBase.TabIndex = 0;
            // 
            // txtId
            // 
            txtId.Location = new Point(114, 125);
            txtId.Name = "txtId";
            txtId.Size = new Size(223, 27);
            txtId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(81, 128);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 0;
            label1.Text = "ID:";
            // 
            // FrmBase
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 343);
            Controls.Add(pnlFormularioBase);
            Name = "FrmBase";
            Text = "FrmBase";
            pnlFormularioBase.ResumeLayout(false);
            pnlFormularioBase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        protected Panel pnlFormularioBase;
        protected TextBox txtId;
        protected Label label1;
    }
}