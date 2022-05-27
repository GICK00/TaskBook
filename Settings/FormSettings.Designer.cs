namespace Settings
{
    partial class FormSettings
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBoxSourceDBmdf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxNameServer = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxNameDB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxSourceDBldf = new System.Windows.Forms.TextBox();
            this.buttonDetech = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel8.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel8.Controls.Add(this.textBoxSourceDBmdf);
            this.panel8.Location = new System.Drawing.Point(15, 136);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(414, 24);
            this.panel8.TabIndex = 5;
            // 
            // textBoxSourceDBmdf
            // 
            this.textBoxSourceDBmdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxSourceDBmdf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSourceDBmdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxSourceDBmdf.Location = new System.Drawing.Point(4, 5);
            this.textBoxSourceDBmdf.Name = "textBoxSourceDBmdf";
            this.textBoxSourceDBmdf.ReadOnly = true;
            this.textBoxSourceDBmdf.ShortcutsEnabled = false;
            this.textBoxSourceDBmdf.Size = new System.Drawing.Size(406, 13);
            this.textBoxSourceDBmdf.TabIndex = 50;
            this.textBoxSourceDBmdf.TabStop = false;
            this.textBoxSourceDBmdf.WordWrap = false;
            this.textBoxSourceDBmdf.Click += new System.EventHandler(this.textBoxSourceDBmdf_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 15);
            this.label1.TabIndex = 52;
            this.label1.Text = "Путь к файлу DataBase.mdf";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonConnect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonConnect.FlatAppearance.BorderSize = 0;
            this.buttonConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConnect.Location = new System.Drawing.Point(15, 217);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(107, 23);
            this.buttonConnect.TabIndex = 54;
            this.buttonConnect.Text = "Присоеденить";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(16, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 55;
            this.label3.Text = "Имя сервера";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.textBoxNameServer);
            this.panel2.Location = new System.Drawing.Point(15, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(188, 24);
            this.panel2.TabIndex = 51;
            // 
            // textBoxNameServer
            // 
            this.textBoxNameServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxNameServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNameServer.Location = new System.Drawing.Point(3, 5);
            this.textBoxNameServer.Name = "textBoxNameServer";
            this.textBoxNameServer.Size = new System.Drawing.Size(182, 13);
            this.textBoxNameServer.TabIndex = 50;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.textBoxNameDB);
            this.panel3.Location = new System.Drawing.Point(241, 89);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(188, 24);
            this.panel3.TabIndex = 52;
            // 
            // textBoxNameDB
            // 
            this.textBoxNameDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxNameDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxNameDB.Location = new System.Drawing.Point(3, 5);
            this.textBoxNameDB.Name = "textBoxNameDB";
            this.textBoxNameDB.Size = new System.Drawing.Size(182, 13);
            this.textBoxNameDB.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(245, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 56;
            this.label4.Text = "Имя базы данных";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(15, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 15);
            this.label5.TabIndex = 58;
            this.label5.Text = "Путь к файлу DataBase_log.ldf";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.Controls.Add(this.textBoxSourceDBldf);
            this.panel4.Location = new System.Drawing.Point(15, 181);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(414, 24);
            this.panel4.TabIndex = 57;
            // 
            // textBoxSourceDBldf
            // 
            this.textBoxSourceDBldf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxSourceDBldf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSourceDBldf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxSourceDBldf.Location = new System.Drawing.Point(4, 5);
            this.textBoxSourceDBldf.Name = "textBoxSourceDBldf";
            this.textBoxSourceDBldf.ReadOnly = true;
            this.textBoxSourceDBldf.ShortcutsEnabled = false;
            this.textBoxSourceDBldf.Size = new System.Drawing.Size(406, 13);
            this.textBoxSourceDBldf.TabIndex = 50;
            this.textBoxSourceDBldf.TabStop = false;
            this.textBoxSourceDBldf.WordWrap = false;
            this.textBoxSourceDBldf.Click += new System.EventHandler(this.textBoxSourceDBldf_Click);
            // 
            // buttonDetech
            // 
            this.buttonDetech.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDetech.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDetech.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDetech.FlatAppearance.BorderSize = 0;
            this.buttonDetech.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonDetech.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.buttonDetech.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetech.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDetech.Location = new System.Drawing.Point(131, 217);
            this.buttonDetech.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDetech.Name = "buttonDetech";
            this.buttonDetech.Size = new System.Drawing.Size(107, 23);
            this.buttonDetech.TabIndex = 59;
            this.buttonDetech.Text = "Отсоеденить";
            this.buttonDetech.UseVisualStyleBackColor = false;
            this.buttonDetech.Click += new System.EventHandler(this.buttonDetech_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(248, 217);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 60;
            this.button1.Text = "Открыть SQL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 253);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonDetech);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(447, 253);
            this.MinimumSize = new System.Drawing.Size(447, 253);
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки БД";
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel8;
        public System.Windows.Forms.TextBox textBoxSourceDBmdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.TextBox textBoxNameServer;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.TextBox textBoxNameDB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.TextBox textBoxSourceDBldf;
        private System.Windows.Forms.Button buttonDetech;
        private System.Windows.Forms.Button button1;
    }
}

