using System;
using System.Windows.Forms;

namespace Word_Recognition_System
{
    partial class MainMenu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.adminAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lettersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminAreaToolStripMenuItem,
            this.lettersToolStripMenuItem,
            this.numberToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(799, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adminAreaToolStripMenuItem
            // 
            this.adminAreaToolStripMenuItem.Name = "adminAreaToolStripMenuItem";
            this.adminAreaToolStripMenuItem.Size = new System.Drawing.Size(87, 21);
            this.adminAreaToolStripMenuItem.Text = "Admin area";
            this.adminAreaToolStripMenuItem.Click += new System.EventHandler(this.adminAreaToolStripMenuItem_Click);
            // 
            // lettersToolStripMenuItem
            // 
            this.lettersToolStripMenuItem.Name = "lettersToolStripMenuItem";
            this.lettersToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.lettersToolStripMenuItem.Text = "Letters";
            this.lettersToolStripMenuItem.Click += new System.EventHandler(this.lettersToolStripMenuItem_Click);
            // 
            // numberToolStripMenuItem
            // 
            this.numberToolStripMenuItem.Name = "numberToolStripMenuItem";
            this.numberToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.numberToolStripMenuItem.Text = "Number";
            this.numberToolStripMenuItem.Click += new System.EventHandler(this.numberToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.BackgroundImage = global::Word_Recognition_System.Properties.Resources.scene_of_children_learning_23_2147620646;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(799, 502);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adminAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lettersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem numberToolStripMenuItem;
    }
}

