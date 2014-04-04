namespace serwerHTTPforms
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
            this.start_s = new System.Windows.Forms.Button();
            this.stop_s = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.port_n = new System.Windows.Forms.TextBox();
            this.file_p = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // start_s
            // 
            this.start_s.Location = new System.Drawing.Point(42, 134);
            this.start_s.Name = "start_s";
            this.start_s.Size = new System.Drawing.Size(101, 52);
            this.start_s.TabIndex = 0;
            this.start_s.Text = "Start server";
            this.start_s.UseVisualStyleBackColor = true;
            this.start_s.Click += new System.EventHandler(this.start_s_Click);
            // 
            // stop_s
            // 
            this.stop_s.Location = new System.Drawing.Point(190, 134);
            this.stop_s.Name = "stop_s";
            this.stop_s.Size = new System.Drawing.Size(101, 52);
            this.stop_s.TabIndex = 1;
            this.stop_s.Text = "Server stop";
            this.stop_s.UseVisualStyleBackColor = true;
            this.stop_s.Click += new System.EventHandler(this.stop_s_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Path to files";
            // 
            // port_n
            // 
            this.port_n.Location = new System.Drawing.Point(42, 28);
            this.port_n.Name = "port_n";
            this.port_n.Size = new System.Drawing.Size(249, 20);
            this.port_n.TabIndex = 4;
            this.port_n.TextChanged += new System.EventHandler(this.port_n_TextChanged);
            // 
            // file_p
            // 
            this.file_p.Location = new System.Drawing.Point(42, 78);
            this.file_p.Name = "file_p";
            this.file_p.Size = new System.Drawing.Size(249, 20);
            this.file_p.TabIndex = 5;
            this.file_p.TextChanged += new System.EventHandler(this.file_p_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 210);
            this.Controls.Add(this.file_p);
            this.Controls.Add(this.port_n);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stop_s);
            this.Controls.Add(this.start_s);
            this.Name = "Form1";
            this.Text = "serverHTTP";
            this.Load += new System.EventHandler(this.Form1_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_s;
        private System.Windows.Forms.Button stop_s;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port_n;
        private System.Windows.Forms.TextBox file_p;
    }
}

