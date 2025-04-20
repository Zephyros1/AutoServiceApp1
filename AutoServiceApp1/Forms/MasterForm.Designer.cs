namespace AutoServiceApp1
{
    partial class MasterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblWelcome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblWelcome
            //
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWelcome.Location = new System.Drawing.Point(30, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(120, 21);
            this.lblWelcome.Text = "Добро пожаловать!";
            //
            // MasterForm
            //
            this.ClientSize = new System.Drawing.Size(400, 100);
            this.Controls.Add(this.lblWelcome);
            this.Name = "MasterForm";
            this.Text = "Мастер";
            this.Load += new System.EventHandler(this.MasterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
