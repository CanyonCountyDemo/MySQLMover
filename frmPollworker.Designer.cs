namespace MySQLMover
{
  partial class frmPollworker
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.btnImport = new System.Windows.Forms.Button();
      this.myGrid = new System.Windows.Forms.DataGridView();
      this.progress = new System.Windows.Forms.ProgressBar();
      this.ssGrid = new System.Windows.Forms.DataGridView();
      this.textBox2 = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.myGrid)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ssGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.textBox2);
      this.splitContainer1.Panel1.Controls.Add(this.textBox1);
      this.splitContainer1.Panel1.Controls.Add(this.btnUpdate);
      this.splitContainer1.Panel1.Controls.Add(this.btnImport);
      this.splitContainer1.Panel1.Controls.Add(this.myGrid);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.progress);
      this.splitContainer1.Panel2.Controls.Add(this.ssGrid);
      this.splitContainer1.Size = new System.Drawing.Size(677, 454);
      this.splitContainer1.SplitterDistance = 361;
      this.splitContainer1.TabIndex = 0;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(106, 9);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(168, 73);
      this.textBox1.TabIndex = 3;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Location = new System.Drawing.Point(13, 12);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(75, 23);
      this.btnUpdate.TabIndex = 2;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // btnImport
      // 
      this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnImport.Enabled = false;
      this.btnImport.Location = new System.Drawing.Point(283, 15);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(75, 23);
      this.btnImport.TabIndex = 1;
      this.btnImport.Text = "Import ->";
      this.btnImport.UseVisualStyleBackColor = true;
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // myGrid
      // 
      this.myGrid.AllowUserToAddRows = false;
      this.myGrid.AllowUserToDeleteRows = false;
      this.myGrid.AllowUserToOrderColumns = true;
      this.myGrid.AllowUserToResizeColumns = false;
      this.myGrid.AllowUserToResizeRows = false;
      this.myGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.myGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.myGrid.Location = new System.Drawing.Point(13, 44);
      this.myGrid.Name = "myGrid";
      this.myGrid.RowHeadersVisible = false;
      this.myGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.myGrid.Size = new System.Drawing.Size(345, 398);
      this.myGrid.TabIndex = 0;
      // 
      // progress
      // 
      this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progress.Location = new System.Drawing.Point(3, 15);
      this.progress.Name = "progress";
      this.progress.Size = new System.Drawing.Size(297, 23);
      this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progress.TabIndex = 2;
      this.progress.Visible = false;
      // 
      // ssGrid
      // 
      this.ssGrid.AllowUserToAddRows = false;
      this.ssGrid.AllowUserToDeleteRows = false;
      this.ssGrid.AllowUserToOrderColumns = true;
      this.ssGrid.AllowUserToResizeColumns = false;
      this.ssGrid.AllowUserToResizeRows = false;
      this.ssGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ssGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ssGrid.Location = new System.Drawing.Point(3, 44);
      this.ssGrid.Name = "ssGrid";
      this.ssGrid.RowHeadersVisible = false;
      this.ssGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.ssGrid.Size = new System.Drawing.Size(306, 398);
      this.ssGrid.TabIndex = 1;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(106, 88);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(168, 73);
      this.textBox2.TabIndex = 4;
      // 
      // frmPollworker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(677, 454);
      this.Controls.Add(this.splitContainer1);
      this.Name = "frmPollworker";
      this.Text = "MySQL Mover";
      this.Load += new System.EventHandler(this.frmPollworker_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.myGrid)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ssGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView myGrid;
    private System.Windows.Forms.Button btnImport;
    private System.Windows.Forms.DataGridView ssGrid;
    private System.Windows.Forms.ProgressBar progress;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
  }
}

