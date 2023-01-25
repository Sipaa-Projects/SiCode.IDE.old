namespace SiCodeIDE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.FileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInCommandPromptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompilerContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runCompiledApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seeCompileLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seeCompileErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutSiCodeIDEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.referencedAssembliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.FileContextMenu.SuspendLayout();
            this.CompilerContextMenu.SuspendLayout();
            this.HelpContextMenu.SuspendLayout();
            this.OptionsContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.button4.Location = new System.Drawing.Point(255, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 32);
            this.button4.TabIndex = 4;
            this.button4.Text = "HELP";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button3_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.button3.Location = new System.Drawing.Point(170, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "OPTIONS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.button2.Location = new System.Drawing.Point(85, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "COMPILER";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 32);
            this.button1.TabIndex = 1;
            this.button1.Text = "FILE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FileContextMenu
            // 
            this.FileContextMenu.BackColor = System.Drawing.SystemColors.Control;
            this.FileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectToolStripMenuItem,
            this.createProjectToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.showInFileExplorerToolStripMenuItem,
            this.showInCommandPromptToolStripMenuItem});
            this.FileContextMenu.Name = "FileContextMenu";
            this.FileContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FileContextMenu.Size = new System.Drawing.Size(221, 114);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // createProjectToolStripMenuItem
            // 
            this.createProjectToolStripMenuItem.Name = "createProjectToolStripMenuItem";
            this.createProjectToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.createProjectToolStripMenuItem.Text = "Create Project";
            this.createProjectToolStripMenuItem.Click += new System.EventHandler(this.crToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // showInFileExplorerToolStripMenuItem
            // 
            this.showInFileExplorerToolStripMenuItem.Name = "showInFileExplorerToolStripMenuItem";
            this.showInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.showInFileExplorerToolStripMenuItem.Text = "Show in File Explorer";
            this.showInFileExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInFileExplorerToolStripMenuItem_Click);
            // 
            // showInCommandPromptToolStripMenuItem
            // 
            this.showInCommandPromptToolStripMenuItem.Name = "showInCommandPromptToolStripMenuItem";
            this.showInCommandPromptToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.showInCommandPromptToolStripMenuItem.Text = "Show in Command Prompt";
            this.showInCommandPromptToolStripMenuItem.Click += new System.EventHandler(this.showInCommandPromptToolStripMenuItem_Click);
            // 
            // CompilerContextMenu
            // 
            this.CompilerContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem,
            this.runCompiledApplicationToolStripMenuItem,
            this.seeCompileLogsToolStripMenuItem,
            this.seeCompileErrorsToolStripMenuItem,
            this.referencedAssembliesToolStripMenuItem});
            this.CompilerContextMenu.Name = "CompilerContextMenu";
            this.CompilerContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.CompilerContextMenu.Size = new System.Drawing.Size(216, 136);
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.compileToolStripMenuItem.Text = "Compile";
            this.compileToolStripMenuItem.Click += new System.EventHandler(this.compileToolStripMenuItem_Click);
            // 
            // runCompiledApplicationToolStripMenuItem
            // 
            this.runCompiledApplicationToolStripMenuItem.Name = "runCompiledApplicationToolStripMenuItem";
            this.runCompiledApplicationToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.runCompiledApplicationToolStripMenuItem.Text = "Run Compiled Application";
            this.runCompiledApplicationToolStripMenuItem.Click += new System.EventHandler(this.runCompiledApplicationToolStripMenuItem_Click);
            // 
            // seeCompileLogsToolStripMenuItem
            // 
            this.seeCompileLogsToolStripMenuItem.Name = "seeCompileLogsToolStripMenuItem";
            this.seeCompileLogsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.seeCompileLogsToolStripMenuItem.Text = "See compile logs";
            this.seeCompileLogsToolStripMenuItem.Click += new System.EventHandler(this.seeLastCompilerLogsToolStripMenuItem_Click);
            // 
            // seeCompileErrorsToolStripMenuItem
            // 
            this.seeCompileErrorsToolStripMenuItem.Name = "seeCompileErrorsToolStripMenuItem";
            this.seeCompileErrorsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.seeCompileErrorsToolStripMenuItem.Text = "See compile errors";
            this.seeCompileErrorsToolStripMenuItem.Click += new System.EventHandler(this.seeLastCompileErrorsToolStripMenuItem_Click);
            // 
            // HelpContextMenu
            // 
            this.HelpContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSiCodeIDEToolStripMenuItem});
            this.HelpContextMenu.Name = "HelpContextMenu";
            this.HelpContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.HelpContextMenu.Size = new System.Drawing.Size(171, 26);
            // 
            // aboutSiCodeIDEToolStripMenuItem
            // 
            this.aboutSiCodeIDEToolStripMenuItem.Name = "aboutSiCodeIDEToolStripMenuItem";
            this.aboutSiCodeIDEToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aboutSiCodeIDEToolStripMenuItem.Text = "About SiCode IDE";
            this.aboutSiCodeIDEToolStripMenuItem.Click += new System.EventHandler(this.aboutSiCodeIDEToolStripMenuItem_Click);
            // 
            // OptionsContextMenu
            // 
            this.OptionsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.OptionsContextMenu.Name = "HelpContextMenu";
            this.OptionsContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.OptionsContextMenu.Size = new System.Drawing.Size(193, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "SiCode IDE properties";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox1.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(0, 84);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.fastColoredTextBox1.BookmarkColor = System.Drawing.Color.MediumTurquoise;
            this.fastColoredTextBox1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fastColoredTextBox1.CaretColor = System.Drawing.Color.Transparent;
            this.fastColoredTextBox1.CharHeight = 14;
            this.fastColoredTextBox1.CharWidth = 8;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBox1.ForeColor = System.Drawing.Color.White;
            this.fastColoredTextBox1.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(100)))));
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.CSharp;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.LeftBracket2 = '{';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 32);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.RightBracket2 = '}';
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
            this.fastColoredTextBox1.ServiceLinesColor = System.Drawing.Color.Transparent;
            this.fastColoredTextBox1.Size = new System.Drawing.Size(800, 418);
            this.fastColoredTextBox1.TabIndex = 4;
            this.fastColoredTextBox1.Text = resources.GetString("fastColoredTextBox1.Text");
            this.fastColoredTextBox1.TextAreaBorderColor = System.Drawing.Color.Transparent;
            this.fastColoredTextBox1.WordWrap = true;
            this.fastColoredTextBox1.Zoom = 100;
            this.fastColoredTextBox1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fctbTextChanged);
            this.fastColoredTextBox1.Load += new System.EventHandler(this.fastColoredTextBox1_Load);
            // 
            // referencedAssembliesToolStripMenuItem
            // 
            this.referencedAssembliesToolStripMenuItem.Name = "referencedAssembliesToolStripMenuItem";
            this.referencedAssembliesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.referencedAssembliesToolStripMenuItem.Text = "Referenced Assemblies";
            this.referencedAssembliesToolStripMenuItem.Click += new System.EventHandler(this.referencedAssembliesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SiCode IDE";
            this.panel1.ResumeLayout(false);
            this.FileContextMenu.ResumeLayout(false);
            this.CompilerContextMenu.ResumeLayout(false);
            this.HelpContextMenu.ResumeLayout(false);
            this.OptionsContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip FileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip CompilerContextMenu;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runCompiledApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seeCompileLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seeCompileErrorsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip HelpContextMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutSiCodeIDEToolStripMenuItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ContextMenuStrip OptionsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.ToolStripMenuItem showInFileExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInCommandPromptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referencedAssembliesToolStripMenuItem;
    }
}