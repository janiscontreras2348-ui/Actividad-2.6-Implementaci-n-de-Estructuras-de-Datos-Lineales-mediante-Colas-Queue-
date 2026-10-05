namespace Biblioteca
{
    partial class FrmReserva
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
            txb_Info_frmReser = new TextBox();
            dtp_FechaEntr_frmReser = new DateTimePicker();
            dtp_FechaReser_frmReser = new DateTimePicker();
            label74 = new Label();
            label73 = new Label();
            txb_LibReser_frmReser = new TextBox();
            btn_Imagen_frmReser = new Button();
            ckb_Activo_frmReser = new CheckBox();
            txb_RutaIma_frmReser = new TextBox();
            txb_Usuario_frmReser = new TextBox();
            label69 = new Label();
            label70 = new Label();
            label71 = new Label();
            btnAtender = new Button();
            btnRegistrarTurno = new Button();
            lstCola = new ListBox();
            label2 = new Label();
            lblSiguiente = new Label();
            btnVaciar = new Button();
            pnlFormularioBase.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFormularioBase
            // 
            pnlFormularioBase.Controls.Add(btnVaciar);
            pnlFormularioBase.Controls.Add(lblSiguiente);
            pnlFormularioBase.Controls.Add(label2);
            pnlFormularioBase.Controls.Add(lstCola);
            pnlFormularioBase.Controls.Add(btnRegistrarTurno);
            pnlFormularioBase.Controls.Add(btnAtender);
            pnlFormularioBase.Controls.Add(txb_Info_frmReser);
            pnlFormularioBase.Controls.Add(dtp_FechaEntr_frmReser);
            pnlFormularioBase.Controls.Add(dtp_FechaReser_frmReser);
            pnlFormularioBase.Controls.Add(label74);
            pnlFormularioBase.Controls.Add(label73);
            pnlFormularioBase.Controls.Add(txb_LibReser_frmReser);
            pnlFormularioBase.Controls.Add(btn_Imagen_frmReser);
            pnlFormularioBase.Controls.Add(ckb_Activo_frmReser);
            pnlFormularioBase.Controls.Add(txb_RutaIma_frmReser);
            pnlFormularioBase.Controls.Add(txb_Usuario_frmReser);
            pnlFormularioBase.Controls.Add(label69);
            pnlFormularioBase.Controls.Add(label70);
            pnlFormularioBase.Controls.Add(label71);
            pnlFormularioBase.Size = new Size(1143, 886);
            pnlFormularioBase.Controls.SetChildIndex(label1, 0);
            pnlFormularioBase.Controls.SetChildIndex(txtId, 0);
            pnlFormularioBase.Controls.SetChildIndex(label71, 0);
            pnlFormularioBase.Controls.SetChildIndex(label70, 0);
            pnlFormularioBase.Controls.SetChildIndex(label69, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Usuario_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_RutaIma_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(ckb_Activo_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(btn_Imagen_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_LibReser_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(label73, 0);
            pnlFormularioBase.Controls.SetChildIndex(label74, 0);
            pnlFormularioBase.Controls.SetChildIndex(dtp_FechaReser_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(dtp_FechaEntr_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Info_frmReser, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnAtender, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnRegistrarTurno, 0);
            pnlFormularioBase.Controls.SetChildIndex(lstCola, 0);
            pnlFormularioBase.Controls.SetChildIndex(label2, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblSiguiente, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnVaciar, 0);
            // 
            // txtId
            // 
            txtId.Location = new Point(130, 236);
            // 
            // label1
            // 
            label1.Location = new Point(97, 239);
            // 
            // txb_Info_frmReser
            // 
            txb_Info_frmReser.BackColor = SystemColors.ButtonHighlight;
            txb_Info_frmReser.Location = new Point(467, 228);
            txb_Info_frmReser.Margin = new Padding(3, 4, 3, 4);
            txb_Info_frmReser.Multiline = true;
            txb_Info_frmReser.Name = "txb_Info_frmReser";
            txb_Info_frmReser.ReadOnly = true;
            txb_Info_frmReser.ScrollBars = ScrollBars.Both;
            txb_Info_frmReser.Size = new Size(632, 353);
            txb_Info_frmReser.TabIndex = 78;
            // 
            // dtp_FechaEntr_frmReser
            // 
            dtp_FechaEntr_frmReser.Font = new Font("Segoe UI", 9F);
            dtp_FechaEntr_frmReser.Format = DateTimePickerFormat.Short;
            dtp_FechaEntr_frmReser.Location = new Point(221, 442);
            dtp_FechaEntr_frmReser.Margin = new Padding(3, 4, 3, 4);
            dtp_FechaEntr_frmReser.Name = "dtp_FechaEntr_frmReser";
            dtp_FechaEntr_frmReser.Size = new Size(227, 27);
            dtp_FechaEntr_frmReser.TabIndex = 77;
            dtp_FechaEntr_frmReser.Value = new DateTime(2026, 9, 12, 16, 20, 43, 0);
            // 
            // dtp_FechaReser_frmReser
            // 
            dtp_FechaReser_frmReser.Font = new Font("Segoe UI", 9F);
            dtp_FechaReser_frmReser.Format = DateTimePickerFormat.Short;
            dtp_FechaReser_frmReser.Location = new Point(221, 387);
            dtp_FechaReser_frmReser.Margin = new Padding(3, 4, 3, 4);
            dtp_FechaReser_frmReser.Name = "dtp_FechaReser_frmReser";
            dtp_FechaReser_frmReser.Size = new Size(227, 27);
            dtp_FechaReser_frmReser.TabIndex = 76;
            dtp_FechaReser_frmReser.Value = new DateTime(2026, 9, 12, 16, 20, 43, 0);
            // 
            // label74
            // 
            label74.AutoSize = true;
            label74.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label74.Location = new Point(93, 447);
            label74.Name = "label74";
            label74.Size = new Size(126, 20);
            label74.TabIndex = 75;
            label74.Text = "Fecha de Entrega:";
            // 
            // label73
            // 
            label73.AutoSize = true;
            label73.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label73.Location = new Point(93, 392);
            label73.Name = "label73";
            label73.Size = new Size(122, 20);
            label73.TabIndex = 74;
            label73.Text = "Fecha de reserva:";
            // 
            // txb_LibReser_frmReser
            // 
            txb_LibReser_frmReser.Location = new Point(210, 345);
            txb_LibReser_frmReser.Margin = new Padding(3, 4, 3, 4);
            txb_LibReser_frmReser.Name = "txb_LibReser_frmReser";
            txb_LibReser_frmReser.Size = new Size(238, 27);
            txb_LibReser_frmReser.TabIndex = 73;
            // 
            // btn_Imagen_frmReser
            // 
            btn_Imagen_frmReser.Location = new Point(410, 549);
            btn_Imagen_frmReser.Margin = new Padding(3, 4, 3, 4);
            btn_Imagen_frmReser.Name = "btn_Imagen_frmReser";
            btn_Imagen_frmReser.Size = new Size(38, 32);
            btn_Imagen_frmReser.TabIndex = 72;
            btn_Imagen_frmReser.Text = "...";
            btn_Imagen_frmReser.UseVisualStyleBackColor = true;
            // 
            // ckb_Activo_frmReser
            // 
            ckb_Activo_frmReser.AutoSize = true;
            ckb_Activo_frmReser.Checked = true;
            ckb_Activo_frmReser.CheckState = CheckState.Checked;
            ckb_Activo_frmReser.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ckb_Activo_frmReser.Location = new Point(94, 488);
            ckb_Activo_frmReser.Margin = new Padding(3, 4, 3, 4);
            ckb_Activo_frmReser.Name = "ckb_Activo_frmReser";
            ckb_Activo_frmReser.Size = new Size(73, 24);
            ckb_Activo_frmReser.TabIndex = 71;
            ckb_Activo_frmReser.Text = "Activo";
            ckb_Activo_frmReser.UseVisualStyleBackColor = true;
            // 
            // txb_RutaIma_frmReser
            // 
            txb_RutaIma_frmReser.BackColor = SystemColors.ButtonHighlight;
            txb_RutaIma_frmReser.Location = new Point(92, 552);
            txb_RutaIma_frmReser.Margin = new Padding(3, 4, 3, 4);
            txb_RutaIma_frmReser.Name = "txb_RutaIma_frmReser";
            txb_RutaIma_frmReser.ReadOnly = true;
            txb_RutaIma_frmReser.Size = new Size(312, 27);
            txb_RutaIma_frmReser.TabIndex = 70;
            // 
            // txb_Usuario_frmReser
            // 
            txb_Usuario_frmReser.Location = new Point(157, 291);
            txb_Usuario_frmReser.Margin = new Padding(3, 4, 3, 4);
            txb_Usuario_frmReser.Name = "txb_Usuario_frmReser";
            txb_Usuario_frmReser.Size = new Size(291, 27);
            txb_Usuario_frmReser.TabIndex = 69;
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Font = new Font("Segoe UI", 9F);
            label69.Location = new Point(92, 523);
            label69.Name = "label69";
            label69.Size = new Size(59, 20);
            label69.TabIndex = 67;
            label69.Text = "Imagen";
            // 
            // label70
            // 
            label70.AutoSize = true;
            label70.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label70.Location = new Point(93, 349);
            label70.Name = "label70";
            label70.Size = new Size(115, 20);
            label70.TabIndex = 66;
            label70.Text = "Libro reservado:";
            // 
            // label71
            // 
            label71.AutoSize = true;
            label71.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label71.Location = new Point(93, 301);
            label71.Name = "label71";
            label71.Size = new Size(62, 20);
            label71.TabIndex = 65;
            label71.Text = "Usuario:";
            // 
            // btnAtender
            // 
            btnAtender.Location = new Point(942, 655);
            btnAtender.Name = "btnAtender";
            btnAtender.Size = new Size(146, 29);
            btnAtender.TabIndex = 79;
            btnAtender.Text = "Atender Siguiente";
            btnAtender.UseVisualStyleBackColor = true;
            // 
            // btnRegistrarTurno
            // 
            btnRegistrarTurno.Location = new Point(842, 655);
            btnRegistrarTurno.Name = "btnRegistrarTurno";
            btnRegistrarTurno.Size = new Size(94, 29);
            btnRegistrarTurno.TabIndex = 82;
            btnRegistrarTurno.Text = "Registrar";
            btnRegistrarTurno.UseVisualStyleBackColor = true;
            // 
            // lstCola
            // 
            lstCola.FormattingEnabled = true;
            lstCola.Location = new Point(91, 611);
            lstCola.Name = "lstCola";
            lstCola.Size = new Size(635, 124);
            lstCola.TabIndex = 83;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(92, 587);
            label2.Name = "label2";
            label2.Size = new Size(120, 20);
            label2.TabIndex = 84;
            label2.Text = "Cola de reservas:";
            // 
            // lblSiguiente
            // 
            lblSiguiente.AutoSize = true;
            lblSiguiente.Location = new Point(732, 611);
            lblSiguiente.Name = "lblSiguiente";
            lblSiguiente.Size = new Size(181, 20);
            lblSiguiente.TabIndex = 85;
            lblSiguiente.Text = "Siguiente en ser atendido:";
            // 
            // btnVaciar
            // 
            btnVaciar.Location = new Point(907, 690);
            btnVaciar.Name = "btnVaciar";
            btnVaciar.Size = new Size(94, 29);
            btnVaciar.TabIndex = 86;
            btnVaciar.Text = "Vaciar Cola";
            btnVaciar.UseVisualStyleBackColor = true;
            // 
            // FrmReserva
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1144, 886);
            Name = "FrmReserva";
            Text = "FrmReserva";
            pnlFormularioBase.ResumeLayout(false);
            pnlFormularioBase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txb_Info_frmReser;
        private DateTimePicker dtp_FechaEntr_frmReser;
        private DateTimePicker dtp_FechaReser_frmReser;
        private Label label74;
        private Label label73;
        private TextBox txb_LibReser_frmReser;
        private Button btn_Imagen_frmReser;
        private CheckBox ckb_Activo_frmReser;
        private TextBox txb_RutaIma_frmReser;
        private TextBox txb_Usuario_frmReser;
        private Label label69;
        private Label label70;
        private Label label71;
        private ListBox lstCola;
        private Button btnRegistrarTurno;
        private Button btnAtender;
        private Label label2;
        private Button btnVaciar;
        private Label lblSiguiente;
    }
}