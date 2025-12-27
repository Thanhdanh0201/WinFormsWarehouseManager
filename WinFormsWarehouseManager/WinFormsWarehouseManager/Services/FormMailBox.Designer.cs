namespace WinFormsWarehouseManager.Forms
{
    partial class MailBoxForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelLeftContent = new System.Windows.Forms.Panel();
            this.btnTrash = new System.Windows.Forms.Button();
            this.btnSent = new System.Windows.Forms.Button();
            this.btnInbox = new System.Windows.Forms.Button();
            this.btnCompose = new System.Windows.Forms.Button();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.flowEmailList = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this._txtSearchBox = new System.Windows.Forms.TextBox();
            this.splitterMain = new System.Windows.Forms.Splitter();
            this.panelRight = new System.Windows.Forms.Panel();
            this._composePanel = new System.Windows.Forms.Panel();
            this._detailPanel = new System.Windows.Forms.Panel();
            this.tableLayoutMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLeftContent.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 4;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutMain.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutMain.Controls.Add(this.panelCenter, 1, 0);
            this.tableLayoutMain.Controls.Add(this.splitterMain, 2, 0);
            this.tableLayoutMain.Controls.Add(this.panelRight, 3, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(2700, 1709);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelLeft.Controls.Add(this.panelLeftContent);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(450, 1709);
            this.panelLeft.TabIndex = 0;
            // 
            // panelLeftContent
            // 
            this.panelLeftContent.Controls.Add(this.btnTrash);
            this.panelLeftContent.Controls.Add(this.btnSent);
            this.panelLeftContent.Controls.Add(this.btnInbox);
            this.panelLeftContent.Controls.Add(this.btnCompose);
            this.panelLeftContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeftContent.Location = new System.Drawing.Point(0, 0);
            this.panelLeftContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLeftContent.Name = "panelLeftContent";
            this.panelLeftContent.Padding = new System.Windows.Forms.Padding(22, 23, 22, 23);
            this.panelLeftContent.Size = new System.Drawing.Size(450, 1709);
            this.panelLeftContent.TabIndex = 1;
            // 
            // btnTrash
            // 
            this.btnTrash.BackColor = System.Drawing.Color.White;
            this.btnTrash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrash.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrash.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnTrash.Location = new System.Drawing.Point(22, 328);
            this.btnTrash.Margin = new System.Windows.Forms.Padding(0, 23, 0, 0);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnTrash.Size = new System.Drawing.Size(406, 97);
            this.btnTrash.TabIndex = 3;
            this.btnTrash.Text = "Trash";
            this.btnTrash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrash.UseVisualStyleBackColor = false;
            this.btnTrash.Click += new System.EventHandler(this.BtnTrash_Click);
            // 
            // btnSent
            // 
            this.btnSent.BackColor = System.Drawing.Color.White;
            this.btnSent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSent.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSent.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSent.Location = new System.Drawing.Point(22, 231);
            this.btnSent.Margin = new System.Windows.Forms.Padding(0, 23, 0, 0);
            this.btnSent.Name = "btnSent";
            this.btnSent.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnSent.Size = new System.Drawing.Size(406, 97);
            this.btnSent.TabIndex = 2;
            this.btnSent.Text = "Sent";
            this.btnSent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSent.UseVisualStyleBackColor = false;
            this.btnSent.Click += new System.EventHandler(this.BtnSent_Click);
            // 
            // btnInbox
            // 
            this.btnInbox.BackColor = System.Drawing.Color.White;
            this.btnInbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInbox.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnInbox.Location = new System.Drawing.Point(22, 134);
            this.btnInbox.Margin = new System.Windows.Forms.Padding(0, 23, 0, 0);
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnInbox.Size = new System.Drawing.Size(406, 97);
            this.btnInbox.TabIndex = 1;
            this.btnInbox.Text = "Inbox";
            this.btnInbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInbox.UseVisualStyleBackColor = false;
            this.btnInbox.Click += new System.EventHandler(this.BtnInbox_Click);
            // 
            // btnCompose
            // 
            this.btnCompose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCompose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCompose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCompose.ForeColor = System.Drawing.Color.White;
            this.btnCompose.Location = new System.Drawing.Point(22, 23);
            this.btnCompose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(406, 111);
            this.btnCompose.TabIndex = 0;
            this.btnCompose.Text = "+ Compose";
            this.btnCompose.UseVisualStyleBackColor = false;
            this.btnCompose.Click += new System.EventHandler(this.BtnCompose_Click);
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelCenter.Controls.Add(this.flowEmailList);
            this.panelCenter.Controls.Add(this.panelSearch);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(450, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(896, 1709);
            this.panelCenter.TabIndex = 1;
            // 
            // flowEmailList
            // 
            this.flowEmailList.AutoScroll = true;
            this.flowEmailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEmailList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowEmailList.Location = new System.Drawing.Point(0, 122);
            this.flowEmailList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowEmailList.Name = "flowEmailList";
            this.flowEmailList.Padding = new System.Windows.Forms.Padding(22, 23, 22, 23);
            this.flowEmailList.Size = new System.Drawing.Size(896, 1587);
            this.flowEmailList.TabIndex = 1;
            this.flowEmailList.WrapContents = false;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.Transparent;
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this._txtSearchBox);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(22, 12, 22, 12);
            this.panelSearch.Size = new System.Drawing.Size(896, 122);
            this.panelSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(693, 25);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(180, 66);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // _txtSearchBox
            // 
            this._txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearchBox.Font = new System.Drawing.Font("Segoe UI", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._txtSearchBox.ForeColor = System.Drawing.Color.Gray;
            this._txtSearchBox.Location = new System.Drawing.Point(22, 25);
            this._txtSearchBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._txtSearchBox.MinimumSize = new System.Drawing.Size(650, 66);
            this._txtSearchBox.Name = "_txtSearchBox";
            this._txtSearchBox.Size = new System.Drawing.Size(650, 66);
            this._txtSearchBox.TabIndex = 0;
            this._txtSearchBox.Text = "Search emails...";
            this._txtSearchBox.Enter += new System.EventHandler(this.TxtSearchBox_Enter);
            this._txtSearchBox.Leave += new System.EventHandler(this.TxtSearchBox_Leave);
            // 
            // splitterMain
            // 
            this.splitterMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.splitterMain.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitterMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterMain.Location = new System.Drawing.Point(1346, 0);
            this.splitterMain.Margin = new System.Windows.Forms.Padding(0);
            this.splitterMain.Name = "splitterMain";
            this.splitterMain.Size = new System.Drawing.Size(8, 1709);
            this.splitterMain.TabIndex = 2;
            this.splitterMain.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelRight.Controls.Add(this._composePanel);
            this.panelRight.Controls.Add(this._detailPanel);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(1354, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(1346, 1709);
            this.panelRight.TabIndex = 3;
            // 
            // _composePanel
            // 
            this._composePanel.AutoScroll = true;
            this._composePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this._composePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._composePanel.Location = new System.Drawing.Point(0, 0);
            this._composePanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._composePanel.Name = "_composePanel";
            this._composePanel.Padding = new System.Windows.Forms.Padding(45, 47, 45, 47);
            this._composePanel.Size = new System.Drawing.Size(1346, 1709);
            this._composePanel.TabIndex = 1;
            this._composePanel.Visible = false;
            // 
            // _detailPanel
            // 
            this._detailPanel.AutoScroll = true;
            this._detailPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this._detailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailPanel.Location = new System.Drawing.Point(0, 0);
            this._detailPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._detailPanel.Name = "_detailPanel";
            this._detailPanel.Padding = new System.Windows.Forms.Padding(45, 47, 45, 47);
            this._detailPanel.Size = new System.Drawing.Size(1346, 1709);
            this._detailPanel.TabIndex = 0;
            this._detailPanel.Visible = false;
            // 
            // MailBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2700, 1709);
            this.Controls.Add(this.tableLayoutMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1800, 1094);
            this.Name = "MailBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MailBox";
            this.Load += new System.EventHandler(this.MailBoxForm_Load);
            this.tableLayoutMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeftContent.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelLeftContent;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.Button btnInbox;
        private System.Windows.Forms.Button btnSent;
        private System.Windows.Forms.Button btnTrash;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox _txtSearchBox;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowEmailList;
        private System.Windows.Forms.Splitter splitterMain;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel _detailPanel;
        private System.Windows.Forms.Panel _composePanel;
    }
}