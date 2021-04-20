
namespace FaceRecognition
{
    partial class FaceFrom
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceFrom));
            this.btn_selected = new System.Windows.Forms.Button();
            this.txtBox_selected = new System.Windows.Forms.TextBox();
            this.txtBox_stored = new System.Windows.Forms.TextBox();
            this.btn_stored = new System.Windows.Forms.Button();
            this.txtBox_log = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.folderBrowserDialog_selected = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog_stored = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_selected
            // 
            this.btn_selected.Location = new System.Drawing.Point(23, 22);
            this.btn_selected.Name = "btn_selected";
            this.btn_selected.Size = new System.Drawing.Size(152, 23);
            this.btn_selected.TabIndex = 1;
            this.btn_selected.Text = "请选择被检索文件夹";
            this.btn_selected.UseVisualStyleBackColor = true;
            this.btn_selected.Click += new System.EventHandler(this.btn_selected_Click);
            // 
            // txtBox_selected
            // 
            this.txtBox_selected.Location = new System.Drawing.Point(181, 22);
            this.txtBox_selected.Name = "txtBox_selected";
            this.txtBox_selected.ReadOnly = true;
            this.txtBox_selected.Size = new System.Drawing.Size(607, 21);
            this.txtBox_selected.TabIndex = 2;
            // 
            // txtBox_stored
            // 
            this.txtBox_stored.Location = new System.Drawing.Point(181, 61);
            this.txtBox_stored.Name = "txtBox_stored";
            this.txtBox_stored.ReadOnly = true;
            this.txtBox_stored.Size = new System.Drawing.Size(607, 21);
            this.txtBox_stored.TabIndex = 4;
            // 
            // btn_stored
            // 
            this.btn_stored.Location = new System.Drawing.Point(23, 61);
            this.btn_stored.Name = "btn_stored";
            this.btn_stored.Size = new System.Drawing.Size(152, 23);
            this.btn_stored.TabIndex = 3;
            this.btn_stored.Text = "请选择检索后存储文件夹";
            this.btn_stored.UseVisualStyleBackColor = true;
            this.btn_stored.Click += new System.EventHandler(this.btn_stored_Click);
            // 
            // txtBox_log
            // 
            this.txtBox_log.Location = new System.Drawing.Point(12, 150);
            this.txtBox_log.Multiline = true;
            this.txtBox_log.Name = "txtBox_log";
            this.txtBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox_log.Size = new System.Drawing.Size(776, 326);
            this.txtBox_log.TabIndex = 5;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(516, 92);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(262, 43);
            this.btn_search.TabIndex = 6;
            this.btn_search.Text = "开始检索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // folderBrowserDialog_selected
            // 
            this.folderBrowserDialog_selected.Description = "请选择被检索的文件夹";
            this.folderBrowserDialog_selected.ShowNewFolderButton = false;
            // 
            // folderBrowserDialog_stored
            // 
            this.folderBrowserDialog_stored.Description = "请选择存储的文件夹";
            // 
            // FaceFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 488);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txtBox_log);
            this.Controls.Add(this.txtBox_stored);
            this.Controls.Add(this.btn_stored);
            this.Controls.Add(this.txtBox_selected);
            this.Controls.Add(this.btn_selected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FaceFrom";
            this.Text = "结婚登记证首页检索";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_selected;
        private System.Windows.Forms.TextBox txtBox_selected;
        private System.Windows.Forms.TextBox txtBox_stored;
        private System.Windows.Forms.Button btn_stored;
        private System.Windows.Forms.TextBox txtBox_log;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_selected;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_stored;
    }
}

