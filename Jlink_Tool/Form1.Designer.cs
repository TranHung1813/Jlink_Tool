namespace Jlink_Tool
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
            this.btConnect = new System.Windows.Forms.Button();
            this.rTb_LogTerminal = new System.Windows.Forms.RichTextBox();
            this.bt_Connect_wthoutHalt = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(642, 273);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(137, 39);
            this.btConnect.TabIndex = 0;
            this.btConnect.Text = "Reset n Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // rTb_LogTerminal
            // 
            this.rTb_LogTerminal.Location = new System.Drawing.Point(725, 260);
            this.rTb_LogTerminal.Name = "rTb_LogTerminal";
            this.rTb_LogTerminal.Size = new System.Drawing.Size(63, 42);
            this.rTb_LogTerminal.TabIndex = 1;
            this.rTb_LogTerminal.Text = "";
            this.rTb_LogTerminal.Visible = false;
            // 
            // bt_Connect_wthoutHalt
            // 
            this.bt_Connect_wthoutHalt.Location = new System.Drawing.Point(407, 273);
            this.bt_Connect_wthoutHalt.Name = "bt_Connect_wthoutHalt";
            this.bt_Connect_wthoutHalt.Size = new System.Drawing.Size(129, 39);
            this.bt_Connect_wthoutHalt.TabIndex = 2;
            this.bt_Connect_wthoutHalt.Text = "Connect";
            this.bt_Connect_wthoutHalt.UseVisualStyleBackColor = true;
            this.bt_Connect_wthoutHalt.Click += new System.EventHandler(this.bt_Connect_wthoutHalt_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Location = new System.Drawing.Point(552, 273);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 39);
            this.btDisconnect.TabIndex = 3;
            this.btDisconnect.Text = "Disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Font = new System.Drawing.Font("Candara", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 228);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "asds";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 331);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btDisconnect);
            this.Controls.Add(this.bt_Connect_wthoutHalt);
            this.Controls.Add(this.rTb_LogTerminal);
            this.Controls.Add(this.btConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.RichTextBox rTb_LogTerminal;
        private System.Windows.Forms.Button bt_Connect_wthoutHalt;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

