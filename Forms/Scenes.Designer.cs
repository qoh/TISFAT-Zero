namespace TISFAT_ZERO.Forms
{
	partial class Scenes
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
			this.pnl_toolsPanel = new System.Windows.Forms.Panel();
			this.btn_addLayer = new System.Windows.Forms.Button();
			this.btn_removeLayer = new System.Windows.Forms.Button();
			this.btn_moveLayerDown = new System.Windows.Forms.Button();
			this.btn_moveLayerUp = new System.Windows.Forms.Button();
			this.pnl_scenePanel = new System.Windows.Forms.Panel();
			this.sceneObject1 = new TISFAT_ZERO.UserControls.SceneControlObject();
			this.pnl_toolsPanel.SuspendLayout();
			this.pnl_scenePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnl_toolsPanel
			// 
			this.pnl_toolsPanel.Controls.Add(this.btn_moveLayerUp);
			this.pnl_toolsPanel.Controls.Add(this.btn_moveLayerDown);
			this.pnl_toolsPanel.Controls.Add(this.btn_removeLayer);
			this.pnl_toolsPanel.Controls.Add(this.btn_addLayer);
			this.pnl_toolsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnl_toolsPanel.Location = new System.Drawing.Point(0, 307);
			this.pnl_toolsPanel.Name = "pnl_toolsPanel";
			this.pnl_toolsPanel.Size = new System.Drawing.Size(224, 29);
			this.pnl_toolsPanel.TabIndex = 0;
			// 
			// btn_addLayer
			// 
			this.btn_addLayer.Location = new System.Drawing.Point(3, 3);
			this.btn_addLayer.Name = "btn_addLayer";
			this.btn_addLayer.Size = new System.Drawing.Size(23, 23);
			this.btn_addLayer.TabIndex = 1;
			this.btn_addLayer.Text = "+";
			this.btn_addLayer.UseVisualStyleBackColor = true;
			// 
			// btn_removeLayer
			// 
			this.btn_removeLayer.Location = new System.Drawing.Point(32, 3);
			this.btn_removeLayer.Name = "btn_removeLayer";
			this.btn_removeLayer.Size = new System.Drawing.Size(23, 23);
			this.btn_removeLayer.TabIndex = 2;
			this.btn_removeLayer.Text = "-";
			this.btn_removeLayer.UseVisualStyleBackColor = true;
			// 
			// btn_moveLayerDown
			// 
			this.btn_moveLayerDown.Location = new System.Drawing.Point(198, 3);
			this.btn_moveLayerDown.Name = "btn_moveLayerDown";
			this.btn_moveLayerDown.Size = new System.Drawing.Size(23, 23);
			this.btn_moveLayerDown.TabIndex = 3;
			this.btn_moveLayerDown.Text = "↓";
			this.btn_moveLayerDown.UseVisualStyleBackColor = true;
			// 
			// btn_moveLayerUp
			// 
			this.btn_moveLayerUp.Location = new System.Drawing.Point(169, 3);
			this.btn_moveLayerUp.Name = "btn_moveLayerUp";
			this.btn_moveLayerUp.Size = new System.Drawing.Size(23, 23);
			this.btn_moveLayerUp.TabIndex = 4;
			this.btn_moveLayerUp.Text = "↑";
			this.btn_moveLayerUp.UseVisualStyleBackColor = true;
			// 
			// pnl_scenePanel
			// 
			this.pnl_scenePanel.AutoScroll = true;
			this.pnl_scenePanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.pnl_scenePanel.Controls.Add(this.sceneObject1);
			this.pnl_scenePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnl_scenePanel.Location = new System.Drawing.Point(0, 0);
			this.pnl_scenePanel.Name = "pnl_scenePanel";
			this.pnl_scenePanel.Size = new System.Drawing.Size(224, 307);
			this.pnl_scenePanel.TabIndex = 1;
			// 
			// sceneObject1
			// 
			this.sceneObject1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.sceneObject1.Location = new System.Drawing.Point(2, 3);
			this.sceneObject1.Name = "sceneObject1";
			this.sceneObject1.Padding = new System.Windows.Forms.Padding(2);
			this.sceneObject1.Size = new System.Drawing.Size(220, 80);
			this.sceneObject1.TabIndex = 0;
			this.sceneObject1.MouseEnter += new System.EventHandler(this.sceneObject1_MouseEnter);
			this.sceneObject1.MouseLeave += new System.EventHandler(this.sceneObject1_MouseLeave);
			// 
			// Scenes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(224, 336);
			this.ControlBox = false;
			this.Controls.Add(this.pnl_scenePanel);
			this.Controls.Add(this.pnl_toolsPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Scenes";
			this.Text = "Scenes";
			this.Load += new System.EventHandler(this.Scenes_Load);
			this.pnl_toolsPanel.ResumeLayout(false);
			this.pnl_scenePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnl_toolsPanel;
		private System.Windows.Forms.Button btn_moveLayerUp;
		private System.Windows.Forms.Button btn_moveLayerDown;
		private System.Windows.Forms.Button btn_removeLayer;
		private System.Windows.Forms.Button btn_addLayer;
		private System.Windows.Forms.Panel pnl_scenePanel;
		private TISFAT_ZERO.UserControls.SceneControlObject sceneObject1;
	}
}