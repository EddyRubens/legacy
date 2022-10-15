using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;

namespace Braintec2
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		private string result;

		protected System.Web.UI.WebControls.Button SendButton;
		protected System.Web.UI.WebControls.Button ClearButton;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox emailTextBox;
		protected System.Web.UI.WebControls.CheckBox testeeCheckBox;
		protected System.Web.UI.WebControls.CheckBox informationCheckBox;
		protected System.Web.UI.WebControls.Label confirmationLabel;
		protected System.Web.UI.WebControls.TextBox remarksTextBox;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ClearButton_Click(object sender, System.EventArgs e)
		{
			emailTextBox.Text = "";
			remarksTextBox.Text = "";
		}

		private void AppendResult(string newResultLine)
		{
			result += (newResultLine + "\r\n");
		}

		private void SendButton_Click(object sender, System.EventArgs e)
		{
			string panelName;
			string resultPath;
			DateTime now;

			result = "";
			now = DateTime.Now;
			AppendResult("<InformationRequest Date=\"" + 
				now.Date.ToString("dd MMM yyyy") + "\" " +
				"Time=\"" + now.ToString("HH:mm:ss") + "\">");
			AppendResult("  <Testee Value=\"" + testeeCheckBox.Checked + "\"\">");
			AppendResult("  <Information Value=\"" + informationCheckBox.Checked + "\"\">");
			AppendResult("  <Email Value=\"" + emailTextBox.Text + "\"\">");
			AppendResult("  <Remarks Value=\"" + remarksTextBox.Text + "\"\">");
			AppendResult("</InformationRequest>");
			//Debug.WriteLine(result);
			resultPath = Server.MapPath(ConfigurationSettings.AppSettings["FileName"]);
			TextWriter output = File.AppendText(resultPath);
			output.WriteLine(result);
			output.Flush();
			output.Close();
			emailTextBox.Text = "";
			remarksTextBox.Text = "";
			informationCheckBox.Checked = false;
			testeeCheckBox.Checked = false;

			confirmationLabel.Text = "Het bericht is verstuurd. Je hoort nog van ons.";

			//Response.Redirect("RegistrationSubmitted.aspx");

		}
	}
}
