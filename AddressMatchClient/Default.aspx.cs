using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddressMatch;
using AddressMatch.Entities;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Validate button click event
    /// </summary>
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        Validator addressMatch = new Validator();
        Collection<PostalAddress> standardAddresses = addressMatch.ValidateAddress(txtAddress1.Text, txtAddress2.Text, txtCity.Text, txtState.Text, txtZip5.Text, txtZip4.Text);

        dgResult.DataSource = standardAddresses;
        dgResult.DataBind();
    }

    /// <summary>
    /// On selection of a address
    /// </summary>
    protected void OnCheckChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;

        if (checkBox.Checked)
        {
            DataGridItem container = (DataGridItem)checkBox.NamingContainer;
            txtAddress1.Text = container.Cells[2].Text == "&nbsp;" ? string.Empty : container.Cells[2].Text;
            txtAddress2.Text = container.Cells[3].Text == "&nbsp;" ? string.Empty : container.Cells[3].Text;
            txtCity.Text = container.Cells[4].Text == "&nbsp;" ? string.Empty : container.Cells[4].Text;
            txtState.Text = container.Cells[5].Text == "&nbsp;" ? string.Empty : container.Cells[5].Text;
            txtZip5.Text = container.Cells[6].Text == "&nbsp;" ? string.Empty : container.Cells[6].Text;
            txtZip4.Text = container.Cells[7].Text == "&nbsp;" ? string.Empty : container.Cells[7].Text;
        }
    }
}
