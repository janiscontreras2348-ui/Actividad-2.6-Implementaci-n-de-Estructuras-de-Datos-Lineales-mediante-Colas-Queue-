namespace Biblioteca
{
    partial class FrmPrestamo
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
            btn_Imagen_frmPrest = new Button();
            txb_RutaIma_frmPrest = new TextBox();
            label69 = new Label();
            txb_Info_frmPrest = new TextBox();
            dtp_FechaEntr_frmPrest = new DateTimePicker();
            label41 = new Label();
            cmb_Estado_frmPrest = new ComboBox();
            cmb_Status_frmPrest = new ComboBox();
            txb_Libro_frmPrest = new TextBox();
            txb_Usuario_frmPrest = new TextBox();
            label36 = new Label();
            label37 = new Label();
            label38 = new Label();
            label39 = new Label();
            lstHistorial = new ListBox();
            lblCima = new Label();
            btnDeshacer = new Button();
            btnRegistrarEstado = new Button();
            pnlFormularioBase.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFormularioBase
            // 
            pnlFormularioBase.Controls.Add(btnRegistrarEstado);
            pnlFormularioBase.Controls.Add(btnDeshacer);
            pnlFormularioBase.Controls.Add(lblCima);
            pnlFormularioBase.Controls.Add(lstHistorial);
            pnlFormularioBase.Controls.Add(btn_Imagen_frmPrest);
            pnlFormularioBase.Controls.Add(txb_RutaIma_frmPrest);
            pnlFormularioBase.Controls.Add(label69);
            pnlFormularioBase.Controls.Add(txb_Info_frmPrest);
            pnlFormularioBase.Controls.Add(dtp_FechaEntr_frmPrest);
            pnlFormularioBase.Controls.Add(label41);
            pnlFormularioBase.Controls.Add(cmb_Estado_frmPrest);
            pnlFormularioBase.Controls.Add(cmb_Status_frmPrest);
            pnlFormularioBase.Controls.Add(txb_Libro_frmPrest);
            pnlFormularioBase.Controls.Add(txb_Usuario_frmPrest);
            pnlFormularioBase.Controls.Add(label36);
            pnlFormularioBase.Controls.Add(label37);
            pnlFormularioBase.Controls.Add(label38);
            pnlFormularioBase.Controls.Add(label39);
            pnlFormularioBase.Size = new Size(1143, 889);
            pnlFormularioBase.Controls.SetChildIndex(label1, 0);
            pnlFormularioBase.Controls.SetChildIndex(txtId, 0);
            pnlFormularioBase.Controls.SetChildIndex(label39, 0);
            pnlFormularioBase.Controls.SetChildIndex(label38, 0);
            pnlFormularioBase.Controls.SetChildIndex(label37, 0);
            pnlFormularioBase.Controls.SetChildIndex(label36, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Usuario_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Libro_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(cmb_Status_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(cmb_Estado_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(label41, 0);
            pnlFormularioBase.Controls.SetChildIndex(dtp_FechaEntr_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_Info_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(label69, 0);
            pnlFormularioBase.Controls.SetChildIndex(txb_RutaIma_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(btn_Imagen_frmPrest, 0);
            pnlFormularioBase.Controls.SetChildIndex(lstHistorial, 0);
            pnlFormularioBase.Controls.SetChildIndex(lblCima, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnDeshacer, 0);
            pnlFormularioBase.Controls.SetChildIndex(btnRegistrarEstado, 0);
            // 
            // txtId
            // 
            txtId.Location = new Point(145, 268);
            // 
            // label1
            // 
            label1.Location = new Point(112, 271);
            // 
            // btn_Imagen_frmPrest
            // 
            btn_Imagen_frmPrest.Location = new Point(354, 553);
            btn_Imagen_frmPrest.Margin = new Padding(3, 4, 3, 4);
            btn_Imagen_frmPrest.Name = "btn_Imagen_frmPrest";
            btn_Imagen_frmPrest.Size = new Size(38, 33);
            btn_Imagen_frmPrest.TabIndex = 74;
            btn_Imagen_frmPrest.Text = "...";
            btn_Imagen_frmPrest.UseVisualStyleBackColor = true;
            // 
            // txb_RutaIma_frmPrest
            // 
            txb_RutaIma_frmPrest.BackColor = SystemColors.ButtonHighlight;
            txb_RutaIma_frmPrest.Location = new Point(108, 555);
            txb_RutaIma_frmPrest.Margin = new Padding(3, 4, 3, 4);
            txb_RutaIma_frmPrest.Name = "txb_RutaIma_frmPrest";
            txb_RutaIma_frmPrest.ReadOnly = true;
            txb_RutaIma_frmPrest.Size = new Size(238, 27);
            txb_RutaIma_frmPrest.TabIndex = 73;
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Font = new Font("Segoe UI", 9F);
            label69.Location = new Point(108, 531);
            label69.Name = "label69";
            label69.Size = new Size(59, 20);
            label69.TabIndex = 72;
            label69.Text = "Imagen";
            // 
            // txb_Info_frmPrest
            // 
            txb_Info_frmPrest.BackColor = SystemColors.ButtonHighlight;
            txb_Info_frmPrest.Location = new Point(410, 240);
            txb_Info_frmPrest.Margin = new Padding(3, 4, 3, 4);
            txb_Info_frmPrest.Multiline = true;
            txb_Info_frmPrest.Name = "txb_Info_frmPrest";
            txb_Info_frmPrest.ReadOnly = true;
            txb_Info_frmPrest.ScrollBars = ScrollBars.Both;
            txb_Info_frmPrest.Size = new Size(625, 346);
            txb_Info_frmPrest.TabIndex = 71;
            // 
            // dtp_FechaEntr_frmPrest
            // 
            dtp_FechaEntr_frmPrest.Font = new Font("Segoe UI", 9F);
            dtp_FechaEntr_frmPrest.Format = DateTimePickerFormat.Short;
            dtp_FechaEntr_frmPrest.Location = new Point(230, 391);
            dtp_FechaEntr_frmPrest.Margin = new Padding(3, 4, 3, 4);
            dtp_FechaEntr_frmPrest.Name = "dtp_FechaEntr_frmPrest";
            dtp_FechaEntr_frmPrest.Size = new Size(162, 27);
            dtp_FechaEntr_frmPrest.TabIndex = 70;
            dtp_FechaEntr_frmPrest.Value = new DateTime(2026, 9, 12, 16, 20, 43, 0);
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new Font("Segoe UI", 9F);
            label41.Location = new Point(108, 441);
            label41.Name = "label41";
            label41.Size = new Size(52, 20);
            label41.TabIndex = 69;
            label41.Text = "Status:";
            // 
            // cmb_Estado_frmPrest
            // 
            cmb_Estado_frmPrest.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmb_Estado_frmPrest.FormattingEnabled = true;
            cmb_Estado_frmPrest.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cmb_Estado_frmPrest.Location = new Point(167, 480);
            cmb_Estado_frmPrest.Name = "cmb_Estado_frmPrest";
            cmb_Estado_frmPrest.Size = new Size(225, 28);
            cmb_Estado_frmPrest.TabIndex = 68;
            // 
            // cmb_Status_frmPrest
            // 
            cmb_Status_frmPrest.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmb_Status_frmPrest.FormattingEnabled = true;
            cmb_Status_frmPrest.Items.AddRange(new object[] { "Pendiente", "Entregado" });
            cmb_Status_frmPrest.Location = new Point(163, 437);
            cmb_Status_frmPrest.Name = "cmb_Status_frmPrest";
            cmb_Status_frmPrest.Size = new Size(228, 28);
            cmb_Status_frmPrest.TabIndex = 67;
            // 
            // txb_Libro_frmPrest
            // 
            txb_Libro_frmPrest.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txb_Libro_frmPrest.Location = new Point(157, 353);
            txb_Libro_frmPrest.Name = "txb_Libro_frmPrest";
            txb_Libro_frmPrest.Size = new Size(234, 27);
            txb_Libro_frmPrest.TabIndex = 66;
            // 
            // txb_Usuario_frmPrest
            // 
            txb_Usuario_frmPrest.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txb_Usuario_frmPrest.Location = new Point(167, 309);
            txb_Usuario_frmPrest.Name = "txb_Usuario_frmPrest";
            txb_Usuario_frmPrest.Size = new Size(214, 27);
            txb_Usuario_frmPrest.TabIndex = 65;
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("Segoe UI", 9F);
            label36.Location = new Point(108, 488);
            label36.Name = "label36";
            label36.Size = new Size(57, 20);
            label36.TabIndex = 64;
            label36.Text = "Estado:";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new Font("Segoe UI", 9F);
            label37.Location = new Point(108, 396);
            label37.Name = "label37";
            label37.Size = new Size(126, 20);
            label37.TabIndex = 63;
            label37.Text = "Fecha de entrega:";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Segoe UI", 9F);
            label38.Location = new Point(108, 359);
            label38.Name = "label38";
            label38.Size = new Size(46, 20);
            label38.TabIndex = 62;
            label38.Text = "Libro:";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new Font("Segoe UI", 9F);
            label39.Location = new Point(108, 312);
            label39.Name = "label39";
            label39.Size = new Size(62, 20);
            label39.TabIndex = 61;
            label39.Text = "Usuario:";
            // 
            // lstHistorial
            // 
            lstHistorial.FormattingEnabled = true;
            lstHistorial.Location = new Point(112, 631);
            lstHistorial.Name = "lstHistorial";
            lstHistorial.Size = new Size(451, 104);
            lstHistorial.TabIndex = 75;
            // 
            // lblCima
            // 
            lblCima.AutoSize = true;
            lblCima.Location = new Point(112, 608);
            lblCima.Name = "lblCima";
            lblCima.Size = new Size(46, 20);
            lblCima.TabIndex = 76;
            lblCima.Text = "Cima:";
            // 
            // btnDeshacer
            // 
            btnDeshacer.Location = new Point(706, 642);
            btnDeshacer.Name = "btnDeshacer";
            btnDeshacer.Size = new Size(94, 29);
            btnDeshacer.TabIndex = 77;
            btnDeshacer.Text = "Deshacer";
            btnDeshacer.UseVisualStyleBackColor = true;
            // 
            // btnRegistrarEstado
            // 
            btnRegistrarEstado.Location = new Point(578, 642);
            btnRegistrarEstado.Name = "btnRegistrarEstado";
            btnRegistrarEstado.Size = new Size(94, 29);
            btnRegistrarEstado.TabIndex = 78;
            btnRegistrarEstado.Text = "Registrar";
            btnRegistrarEstado.UseVisualStyleBackColor = true;
            // 
            // FrmPrestamo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 889);
            Name = "FrmPrestamo";
            Text = "FrmPrestamo";
            pnlFormularioBase.ResumeLayout(false);
            pnlFormularioBase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Imagen_frmPrest;
        private TextBox txb_RutaIma_frmPrest;
        private Label label69;
        private TextBox txb_Info_frmPrest;
        private DateTimePicker dtp_FechaEntr_frmPrest;
        private Label label41;
        private ComboBox cmb_Estado_frmPrest;
        private ComboBox cmb_Status_frmPrest;
        private TextBox txb_Libro_frmPrest;
        private TextBox txb_Usuario_frmPrest;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Button btnRegistrarEstado;
        private Button btnDeshacer;
        private Label lblCima;
        private ListBox lstHistorial;
    }
}