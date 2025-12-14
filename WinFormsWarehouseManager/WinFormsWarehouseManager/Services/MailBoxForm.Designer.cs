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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.flowEmailList = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this._txtSearchBox = new System.Windows.Forms.TextBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this._composePanel = new System.Windows.Forms.Panel();
            this._detailPanel = new System.Windows.Forms.Panel();
            this.tableLayoutMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelLeftContent.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 3;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutMain.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutMain.Controls.Add(this.panelCenter, 1, 0);
            this.tableLayoutMain.Controls.Add(this.panelRight, 2, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1200, 700);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelLeft.Controls.Add(this.panelLeftContent);
            this.panelLeft.Controls.Add(this.panelTitleBar);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 700);
            this.panelLeft.TabIndex = 0;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.panelTitleBar.Controls.Add(this.btnClose);
            this.panelTitleBar.Controls.Add(this.btnMaximize);
            this.panelTitleBar.Controls.Add(this.btnMinimize);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(200, 40);
            this.panelTitleBar.TabIndex = 0;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseDown);
            this.panelTitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseMove);
            this.panelTitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseUp);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "MailBox";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseDown);
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseMove);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseUp);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(110, 5);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(25, 25);
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.BtnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMaximize.ForeColor = System.Drawing.Color.White;
            this.btnMaximize.Location = new System.Drawing.Point(140, 5);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(25, 25);
            this.btnMaximize.TabIndex = 2;
            this.btnMaximize.Text = "□";
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.BtnMaximize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(170, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelLeftContent
            // 
            this.panelLeftContent.Controls.Add(this.btnTrash);
            this.panelLeftContent.Controls.Add(this.btnSent);
            this.panelLeftContent.Controls.Add(this.btnInbox);
            this.panelLeftContent.Controls.Add(this.btnCompose);
            this.panelLeftContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeftContent.Location = new System.Drawing.Point(0, 40);
            this.panelLeftContent.Name = "panelLeftContent";
            this.panelLeftContent.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeftContent.Size = new System.Drawing.Size(200, 660);
            this.panelLeftContent.TabIndex = 1;
            // 
            // btnCompose
            // 
            this.btnCompose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnCompose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCompose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCompose.ForeColor = System.Drawing.Color.White;
            this.btnCompose.Location = new System.Drawing.Point(10, 10);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(180, 45);
            this.btnCompose.TabIndex = 0;
            this.btnCompose.Text = "+ Compose";
            this.btnCompose.UseVisualStyleBackColor = false;
            this.btnCompose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompose.Click += new System.EventHandler(this.BtnCompose_Click);
            // 
            // btnInbox
            // 
            this.btnInbox.BackColor = System.Drawing.Color.White;
            this.btnInbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInbox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnInbox.Location = new System.Drawing.Point(10, 65);
            this.btnInbox.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.btnInbox.Name = "btnInbox";
            this.btnInbox.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnInbox.Size = new System.Drawing.Size(180, 40);
            this.btnInbox.TabIndex = 1;
            this.btnInbox.Text = "Inbox";
            this.btnInbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInbox.UseVisualStyleBackColor = false;
            this.btnInbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInbox.Click += (s, e) => BtnFolder_Click("INBOX");
            // 
            // btnSent
            // 
            this.btnSent.BackColor = System.Drawing.Color.White;
            this.btnSent.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSent.Location = new System.Drawing.Point(10, 115);
            this.btnSent.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.btnSent.Name = "btnSent";
            this.btnSent.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSent.Size = new System.Drawing.Size(180, 40);
            this.btnSent.TabIndex = 2;
            this.btnSent.Text = "Sent";
            this.btnSent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSent.UseVisualStyleBackColor = false;
            this.btnSent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSent.Click += (s, e) => BtnFolder_Click("[Gmail]/Sent Mail");
            // 
            // btnTrash
            // 
            this.btnTrash.BackColor = System.Drawing.Color.White;
            this.btnTrash.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTrash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrash.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTrash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnTrash.Location = new System.Drawing.Point(10, 165);
            this.btnTrash.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.btnTrash.Name = "btnTrash";
            this.btnTrash.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnTrash.Size = new System.Drawing.Size(180, 40);
            this.btnTrash.TabIndex = 3;
            this.btnTrash.Text = "Trash";
            this.btnTrash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrash.UseVisualStyleBackColor = false;
            this.btnTrash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrash.Click += (s, e) => BtnFolder_Click("[Gmail]/Trash");
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelCenter.Controls.Add(this.flowEmailList);
            this.panelCenter.Controls.Add(this.panelSearch);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(200, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(400, 700);
            this.panelCenter.TabIndex = 1;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this._txtSearchBox);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelSearch.Size = new System.Drawing.Size(400, 50);
            this.panelSearch.TabIndex = 0;
            // 
            // _txtSearchBox
            // 
            this._txtSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearchBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._txtSearchBox.ForeColor = System.Drawing.Color.Gray;
            this._txtSearchBox.Location = new System.Drawing.Point(10, 10);
            this._txtSearchBox.Name = "_txtSearchBox";
            this._txtSearchBox.Size = new System.Drawing.Size(290, 27);
            this._txtSearchBox.TabIndex = 0;
            this._txtSearchBox.Text = "Search emails...";
            this._txtSearchBox.Enter += new System.EventHandler(this.TxtSearchBox_Enter);
            this._txtSearchBox.Leave += new System.EventHandler(this.TxtSearchBox_Leave);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(51)))), ((int)(((byte)(66)))));
            this.btnSearch.Location = new System.Drawing.Point(310, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 27);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // flowEmailList
            // 
            this.flowEmailList.AutoScroll = true;
            this.flowEmailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEmailList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowEmailList.Location = new System.Drawing.Point(0, 50);
            this.flowEmailList.Name = "flowEmailList";
            this.flowEmailList.Padding = new System.Windows.Forms.Padding(10);
            this.flowEmailList.Size = new System.Drawing.Size(400, 650);
            this.flowEmailList.TabIndex = 1;
            this.flowEmailList.WrapContents = false;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this._composePanel);
            this.panelRight.Controls.Add(this._detailPanel);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(600, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(600, 700);
            this.panelRight.TabIndex = 2;
            // 
            // _detailPanel
            // 
            this._detailPanel.AutoScroll = true;
            this._detailPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailPanel.Location = new System.Drawing.Point(0, 0);
            this._detailPanel.Name = "_detailPanel";
            this._detailPanel.Padding = new System.Windows.Forms.Padding(20);
            this._detailPanel.Size = new System.Drawing.Size(600, 700);
            this._detailPanel.TabIndex = 0;
            this._detailPanel.Visible = false;
            // 
            // _composePanel
            // 
            this._composePanel.AutoScroll = true;
            this._composePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._composePanel.Location = new System.Drawing.Point(0, 0);
            this._composePanel.Name = "_composePanel";
            this._composePanel.Padding = new System.Windows.Forms.Padding(20);
            this._composePanel.Size = new System.Drawing.Size(600, 700);
            this._composePanel.TabIndex = 1;
            this._composePanel.Visible = false;
            // 
            // MailBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tableLayoutMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MailBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MailBox";
            this.tableLayoutMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeftContent.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnClose;
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
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel _detailPanel;
        private System.Windows.Forms.Panel _composePanel;
    }
}