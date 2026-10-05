namespace Biblioteca
{
    partial class FrmPrincipal
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
            components = new System.ComponentModel.Container();
            pnlContenedorVistas = new Panel();
            pnlBotonera = new Panel();
            label1 = new Label();
            txtIdBusqueda = new TextBox();
            btnMasterBuscar = new Button();
            btnMasterEliminar = new Button();
            btnMasterActualizar = new Button();
            btnMasterGuardar = new Button();
            pnlCabezera = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label6 = new Label();
            btnEditorial = new Button();
            btnAdministrador = new Button();
            btnMulta = new Button();
            btnReserva = new Button();
            btnGenero = new Button();
            btnPersona = new Button();
            btnPrestamos = new Button();
            btnAutores = new Button();
            btnLibros = new Button();
            btnUsuarios = new Button();
            statusStrip1 = new StatusStrip();
            errorProvider1 = new ErrorProvider(components);
            pnlBotonera.SuspendLayout();
            pnlCabezera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // pnlContenedorVistas
            // 
            pnlContenedorVistas.Dock = DockStyle.Fill;
            pnlContenedorVistas.Location = new Point(0, 0);
            pnlContenedorVistas.Name = "pnlContenedorVistas";
            pnlContenedorVistas.Size = new Size(1142, 780);
            pnlContenedorVistas.TabIndex = 12;
            // 
            // pnlBotonera
            // 
            pnlBotonera.Controls.Add(label1);
            pnlBotonera.Controls.Add(txtIdBusqueda);
            pnlBotonera.Controls.Add(btnMasterBuscar);
            pnlBotonera.Controls.Add(btnMasterEliminar);
            pnlBotonera.Controls.Add(btnMasterActualizar);
            pnlBotonera.Controls.Add(btnMasterGuardar);
            pnlBotonera.Dock = DockStyle.Top;
            pnlBotonera.Location = new Point(0, 153);
            pnlBotonera.Name = "pnlBotonera";
            pnlBotonera.Size = new Size(1142, 67);
            pnlBotonera.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(598, 27);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 5;
            label1.Text = "ID a buscar:";
            // 
            // txtIdBusqueda
            // 
            txtIdBusqueda.Location = new Point(688, 23);
            txtIdBusqueda.Name = "txtIdBusqueda";
            txtIdBusqueda.Size = new Size(151, 27);
            txtIdBusqueda.TabIndex = 4;
            // 
            // btnMasterBuscar
            // 
            btnMasterBuscar.Location = new Point(845, 23);
            btnMasterBuscar.Name = "btnMasterBuscar";
            btnMasterBuscar.Size = new Size(94, 29);
            btnMasterBuscar.TabIndex = 3;
            btnMasterBuscar.Text = "Buscar";
            btnMasterBuscar.UseVisualStyleBackColor = true;
            btnMasterBuscar.Click += btnMasterBuscar_Click;
            // 
            // btnMasterEliminar
            // 
            btnMasterEliminar.Location = new Point(375, 22);
            btnMasterEliminar.Name = "btnMasterEliminar";
            btnMasterEliminar.Size = new Size(94, 29);
            btnMasterEliminar.TabIndex = 2;
            btnMasterEliminar.Text = "Eliminar";
            btnMasterEliminar.UseVisualStyleBackColor = true;
            btnMasterEliminar.Click += btnMasterEliminar_Click;
            // 
            // btnMasterActualizar
            // 
            btnMasterActualizar.Location = new Point(210, 22);
            btnMasterActualizar.Name = "btnMasterActualizar";
            btnMasterActualizar.Size = new Size(94, 29);
            btnMasterActualizar.TabIndex = 1;
            btnMasterActualizar.Text = "Actualizar";
            btnMasterActualizar.UseVisualStyleBackColor = true;
            btnMasterActualizar.Click += btnMasterActualizar_Click;
            // 
            // btnMasterGuardar
            // 
            btnMasterGuardar.Location = new Point(46, 22);
            btnMasterGuardar.Name = "btnMasterGuardar";
            btnMasterGuardar.Size = new Size(94, 29);
            btnMasterGuardar.TabIndex = 0;
            btnMasterGuardar.Text = "Guardar";
            btnMasterGuardar.UseVisualStyleBackColor = true;
            btnMasterGuardar.Click += btnMasterGuardar_Click;
            // 
            // pnlCabezera
            // 
            pnlCabezera.Controls.Add(label5);
            pnlCabezera.Controls.Add(label4);
            pnlCabezera.Controls.Add(label3);
            pnlCabezera.Controls.Add(label2);
            pnlCabezera.Controls.Add(label6);
            pnlCabezera.Controls.Add(btnEditorial);
            pnlCabezera.Controls.Add(btnAdministrador);
            pnlCabezera.Controls.Add(btnMulta);
            pnlCabezera.Controls.Add(btnReserva);
            pnlCabezera.Controls.Add(btnGenero);
            pnlCabezera.Controls.Add(btnPersona);
            pnlCabezera.Controls.Add(btnPrestamos);
            pnlCabezera.Controls.Add(btnAutores);
            pnlCabezera.Controls.Add(btnLibros);
            pnlCabezera.Controls.Add(btnUsuarios);
            pnlCabezera.Dock = DockStyle.Top;
            pnlCabezera.Location = new Point(0, 0);
            pnlCabezera.Name = "pnlCabezera";
            pnlCabezera.Size = new Size(1142, 153);
            pnlCabezera.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(934, 9);
            label5.Name = "label5";
            label5.Size = new Size(77, 23);
            label5.TabIndex = 20;
            label5.Text = "Equipo 3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F);
            label4.Location = new Point(622, 9);
            label4.Name = "label4";
            label4.Size = new Size(220, 23);
            label4.TabIndex = 19;
            label4.Text = "TORRES Barrera José Ángel ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(328, 8);
            label3.Name = "label3";
            label3.Size = new Size(254, 23);
            label3.TabIndex = 18;
            label3.Text = "MACÍAS Cruz Meredith Miranda";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(24, 8);
            label2.Name = "label2";
            label2.Size = new Size(277, 23);
            label2.TabIndex = 17;
            label2.Text = "CONTRERAS Rodríguez Janis Isabel";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.Control;
            label6.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(354, 45);
            label6.Name = "label6";
            label6.Size = new Size(353, 35);
            label6.TabIndex = 16;
            label6.Text = "Gestión de Biblioteca Escolar";
            // 
            // btnEditorial
            // 
            btnEditorial.Location = new Point(856, 105);
            btnEditorial.Name = "btnEditorial";
            btnEditorial.Size = new Size(94, 29);
            btnEditorial.TabIndex = 15;
            btnEditorial.Text = "Editorial";
            btnEditorial.UseVisualStyleBackColor = true;
            btnEditorial.Click += btnEditorial_Click;
            // 
            // btnAdministrador
            // 
            btnAdministrador.Location = new Point(956, 105);
            btnAdministrador.Name = "btnAdministrador";
            btnAdministrador.Size = new Size(114, 29);
            btnAdministrador.TabIndex = 14;
            btnAdministrador.Text = "Administrador";
            btnAdministrador.UseVisualStyleBackColor = true;
            btnAdministrador.Click += btnAdministrador_Click;
            // 
            // btnMulta
            // 
            btnMulta.Location = new Point(756, 105);
            btnMulta.Name = "btnMulta";
            btnMulta.Size = new Size(94, 29);
            btnMulta.TabIndex = 13;
            btnMulta.Text = "Multa";
            btnMulta.UseVisualStyleBackColor = true;
            btnMulta.Click += btnMulta_Click;
            // 
            // btnReserva
            // 
            btnReserva.Location = new Point(656, 105);
            btnReserva.Name = "btnReserva";
            btnReserva.Size = new Size(94, 29);
            btnReserva.TabIndex = 12;
            btnReserva.Text = "Reserva";
            btnReserva.UseVisualStyleBackColor = true;
            btnReserva.Click += btnReserva_Click;
            // 
            // btnGenero
            // 
            btnGenero.Location = new Point(556, 105);
            btnGenero.Name = "btnGenero";
            btnGenero.Size = new Size(94, 29);
            btnGenero.TabIndex = 11;
            btnGenero.Text = "Género";
            btnGenero.UseVisualStyleBackColor = true;
            btnGenero.Click += btnGenero_Click;
            // 
            // btnPersona
            // 
            btnPersona.Location = new Point(456, 105);
            btnPersona.Name = "btnPersona";
            btnPersona.Size = new Size(94, 29);
            btnPersona.TabIndex = 10;
            btnPersona.Text = "Persona";
            btnPersona.UseVisualStyleBackColor = true;
            btnPersona.Click += btnPersona_Click;
            // 
            // btnPrestamos
            // 
            btnPrestamos.Location = new Point(356, 105);
            btnPrestamos.Name = "btnPrestamos";
            btnPrestamos.Size = new Size(94, 29);
            btnPrestamos.TabIndex = 9;
            btnPrestamos.Text = "Préstamos";
            btnPrestamos.UseVisualStyleBackColor = true;
            btnPrestamos.Click += btnPrestamos_Click;
            // 
            // btnAutores
            // 
            btnAutores.Location = new Point(256, 105);
            btnAutores.Name = "btnAutores";
            btnAutores.Size = new Size(94, 29);
            btnAutores.TabIndex = 8;
            btnAutores.Text = "Autores";
            btnAutores.UseVisualStyleBackColor = true;
            btnAutores.Click += btnAutores_Click;
            // 
            // btnLibros
            // 
            btnLibros.Location = new Point(156, 105);
            btnLibros.Name = "btnLibros";
            btnLibros.Size = new Size(94, 29);
            btnLibros.TabIndex = 7;
            btnLibros.Text = "Libros";
            btnLibros.UseVisualStyleBackColor = true;
            btnLibros.Click += btnLibros_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.Location = new Point(56, 105);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(94, 29);
            btnUsuarios.TabIndex = 6;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.UseVisualStyleBackColor = true;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 758);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1142, 22);
            statusStrip1.TabIndex = 15;
            statusStrip1.Text = "statusStrip1";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1142, 780);
            Controls.Add(pnlBotonera);
            Controls.Add(statusStrip1);
            Controls.Add(pnlCabezera);
            Controls.Add(pnlContenedorVistas);
            Name = "FrmPrincipal";
            Text = "FrmPrincipal";
            pnlBotonera.ResumeLayout(false);
            pnlBotonera.PerformLayout();
            pnlCabezera.ResumeLayout(false);
            pnlCabezera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlContenedorVistas;
        private Panel pnlCabezera;
        private Panel pnlBotonera;
        private Button btnMasterBuscar;
        private Button btnMasterEliminar;
        private Button btnMasterActualizar;
        private Button btnMasterGuardar;
        private Label label1;
        private TextBox txtIdBusqueda;
        private StatusStrip statusStrip1;
        private Button btnEditorial;
        private Button btnAdministrador;
        private Button btnMulta;
        private Button btnReserva;
        private Button btnGenero;
        private Button btnPersona;
        private Button btnPrestamos;
        private Button btnAutores;
        private Button btnLibros;
        private Button btnUsuarios;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label6;
        private ErrorProvider errorProvider1;
    }
}