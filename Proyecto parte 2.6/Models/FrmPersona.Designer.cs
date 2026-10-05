namespace Biblioteca
{
    partial class FrmPersona
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
            btn_Imagen_frmPerso = new Button();
            txb_RutaIma_frmPerso = new TextBox();
            label69 = new Label();
            txb_Info_frmPerso = new TextBox();
            cmb_Estado_frmPerso = new ComboBox();
            label5 = new Label();
            txb_Correo_frmPerso = new TextBox();
            label4 = new Label();
            txb_Edad_frmPerso = new TextBox();
            label2 = new Label();
            txb_Nombre_frmPerso = new TextBox();
            label3 = new Label();
            pnlFormularioBase.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFormularioBase
            // 
            pnlFormularioBase.Controls.Add(btn_Imagen_frmPerso);
            pnlFormularioBase.Controls.Add(txb_RutaIma_frmPerso);
            pnlFormularioBase.Controls.Add(label69);
            pnlFormularioBase.Controls.Add(txb_Info_frmPerso);
            pnlFormularioBase.Controls.Add(cmb_Estado_frmPerso);
            pnlFormularioBase.Controls.Add(label5);
            pnlFormularioBase.Controls.Add(txb_Correo_frmPerso);
            pnlFormularioBase.Controls.Add(label4);
            pnlFormularioBase.Controls.Add(txb_Edad_frmPerso);
            pnlFormularioBase.Controls.Add(label2);
            pnlFormularioBase.Controls.Add(txb_Nombre_frmPerso);
            pnlFormularioBase.Controls.Add(label3);
            pnlFormularioBase.Size = new Size(1143, 885);
            pnlFormularioBase.Controls.SetChildIndex(label1, 0);
            pnlFormularioBase.Controls.SetChildIndex(txtId, 0);
            pnlFormularioBase.Controls.SetChildIndex(label3, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Nombre_frmPerso, 0);
            pnlFormularioBase.Controls.SetChildIndex(label2, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Edad_frmPerso, 0);
            pnlFormularioBase.Controls.SetChildIndex(label4, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Correo_frmPerso, 0);
            pnlFormularioBase.Controls.SetChildIndex(label5, 0);
            pnlFormularioBase.Controls.SetChildIndex(cmb_Estado_frmPerso, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Info_frmPerso, 0);
            pnlFormularioBase.Controls.SetChildIndex(label69, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_RutaIma_frmPerso, 0);
            pnlFormularioBase.Controls.SetChildIndex(btn_Imagen_frmPerso, 0);
            // 
            // txtId
            // 
            txtId.Location = new Point(103, 295);
            // 
            // label1
            // 
            label1.Location = new Point(70, 298);
            // 
            // btn_Imagen_frmPerso
            // 
            btn_Imagen_frmPerso.Location = new Point(289, 548);
            btn_Imagen_frmPerso.Margin = new Padding(3, 4, 3, 4);
            btn_Imagen_frmPerso.Name = "btn_Imagen_frmPerso";
            btn_Imagen_frmPerso.Size = new Size(38, 32);
            btn_Imagen_frmPerso.TabIndex = 77;
            btn_Imagen_frmPerso.Text = "...";
            btn_Imagen_frmPerso.UseVisualStyleBackColor = true;
            // 
            // txb_RutaIma_frmPerso
            // 
            txb_RutaIma_frmPerso.BackColor = SystemColors.ButtonHighlight;
            txb_RutaIma_frmPerso.Location = new Point(70, 549);
            txb_RutaIma_frmPerso.Margin = new Padding(3, 4, 3, 4);
            txb_RutaIma_frmPerso.Name = "txb_RutaIma_frmPerso";
            txb_RutaIma_frmPerso.ReadOnly = true;
            txb_RutaIma_frmPerso.Size = new Size(212, 27);
            txb_RutaIma_frmPerso.TabIndex = 76;
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Font = new Font("Segoe UI", 9F);
            label69.Location = new Point(70, 525);
            label69.Name = "label69";
            label69.Size = new Size(59, 20);
            label69.TabIndex = 75;
            label69.Text = "Imagen";
            // 
            // txb_Info_frmPerso
            // 
            txb_Info_frmPerso.BackColor = SystemColors.ButtonHighlight;
            txb_Info_frmPerso.Location = new Point(354, 254);
            txb_Info_frmPerso.Margin = new Padding(3, 4, 3, 4);
            txb_Info_frmPerso.Multiline = true;
            txb_Info_frmPerso.Name = "txb_Info_frmPerso";
            txb_Info_frmPerso.ReadOnly = true;
            txb_Info_frmPerso.ScrollBars = ScrollBars.Both;
            txb_Info_frmPerso.Size = new Size(721, 377);
            txb_Info_frmPerso.TabIndex = 74;
            // 
            // cmb_Estado_frmPerso
            // 
            cmb_Estado_frmPerso.FormattingEnabled = true;
            cmb_Estado_frmPerso.Items.AddRange(new object[] { "Activo", "Egresado", "Bloqueado" });
            cmb_Estado_frmPerso.Location = new Point(128, 488);
            cmb_Estado_frmPerso.Margin = new Padding(3, 4, 3, 4);
            cmb_Estado_frmPerso.Name = "cmb_Estado_frmPerso";
            cmb_Estado_frmPerso.Size = new Size(198, 28);
            cmb_Estado_frmPerso.TabIndex = 73;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(70, 484);
            label5.Name = "label5";
            label5.Size = new Size(57, 20);
            label5.TabIndex = 70;
            label5.Text = "Estado:";
            // 
            // txb_Correo_frmPerso
            // 
            txb_Correo_frmPerso.Location = new Point(130, 435);
            txb_Correo_frmPerso.Margin = new Padding(3, 4, 3, 4);
            txb_Correo_frmPerso.Name = "txb_Correo_frmPerso";
            txb_Correo_frmPerso.Size = new Size(196, 27);
            txb_Correo_frmPerso.TabIndex = 69;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(68, 437);
            label4.Name = "label4";
            label4.Size = new Size(57, 20);
            label4.TabIndex = 68;
            label4.Text = "Correo:";
            // 
            // txb_Edad_frmPerso
            // 
            txb_Edad_frmPerso.Location = new Point(130, 391);
            txb_Edad_frmPerso.Margin = new Padding(3, 4, 3, 4);
            txb_Edad_frmPerso.Name = "txb_Edad_frmPerso";
            txb_Edad_frmPerso.Size = new Size(196, 27);
            txb_Edad_frmPerso.TabIndex = 67;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(70, 393);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 66;
            label2.Text = "Edad:";
            // 
            // txb_Nombre_frmPerso
            // 
            txb_Nombre_frmPerso.Location = new Point(137, 343);
            txb_Nombre_frmPerso.Margin = new Padding(3, 4, 3, 4);
            txb_Nombre_frmPerso.Name = "txb_Nombre_frmPerso";
            txb_Nombre_frmPerso.Size = new Size(196, 27);
            txb_Nombre_frmPerso.TabIndex = 65;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(70, 345);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 64;
            label3.Text = "Nombre:";
            // 
            // FrmPersona
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1144, 885);
            Name = "FrmPersona";
            Text = "FrmPersona";
            pnlFormularioBase.ResumeLayout(false);
            pnlFormularioBase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Imagen_frmPerso;
        private TextBox txb_RutaIma_frmPerso;
        private Label label69;
        private TextBox txb_Info_frmPerso;
        private ComboBox cmb_Estado_frmPerso;
        private Label label5;
        private TextBox txb_Correo_frmPerso;
        private Label label4;
        private TextBox txb_Edad_frmPerso;
        private Label label2;
        private TextBox txb_Nombre_frmPerso;
        private Label label3;
    }
}