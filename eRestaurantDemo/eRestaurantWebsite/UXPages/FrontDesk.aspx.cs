using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRestaurantSystem.BLL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
#endregion

public partial class UXPages_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void SeatingGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //extract the table number, number in party and the waiter ID
        //from the gridview.
        //we will also create the time from the MockDateTime controls at the top of this form
        //(Tipically you would use DateTime.Today for current datetime)

        //Once the data is collected then it will be sent to the BLL for processing
        //the command will be done under the control of the MessageUnderControl
        //so if there is an error, the NUC can handle it.
        //We will use the in-Line NUC TryRun technique

        MessageUserControl.TryRun(() =>
            {
                //obtain the selected gridview row
                GridViewRow agvrow = SeatingGridView.Rows[e.NewSelectedIndex];
                //accessing a web control on the gridview row uses .FindControl("xxx") as dataType
                string tablenumber = (agvrow.FindControl("TableNumber") as Label).Text;
                string numberinparty = (agvrow.FindControl("NumberInParty") as TextBox).Text;
                string waiterid = (agvrow.FindControl("WaiterList") as DropDownList).SelectedValue;
                DateTime when = Mocker.MockDate.Add(Mocker.MockTime);

                //standard call to insert a record into the database
                AdminController sysmgr = new AdminController();
                sysmgr.SeatCustomer(when, byte.Parse(tablenumber), int.Parse(numberinparty), int.Parse(waiterid));

                //refresh the gridview
                SeatingGridView.DataBind();

            }, "Customer Seated", "New walk-in customer has been saved");
    
    }

    protected void ReservationSummaryListView_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //this is the method which will gather the seating
        //information for reserations and pass to the BLL
        //for processing

        //no processing will be done unless the e.CommandName is 
        //equal to "Seat"
        if (e.CommandName.Equals("Seat"))
        {
            //execution of the code will under the control
            //of the MessageUserControl
            MessageUserControl.TryRun(() =>
                {
                    //gather the necessary data from the web controls
                    int reservationid = int.Parse(e.CommandArgument.ToString());
                    int waiterid = int.Parse(WaiterDropDownList.SelectedValue);
                    DateTime when = Mocker.MockDate.Add(Mocker.MockTime);

                    //we need to collect possible multiple values from the ListBox control which
                    //contains the selected tables to be assigned to this group of customers
                    List<byte> selectedTables = new List<byte>();

                    //walk through the listBox row by row
                    foreach (ListItem item_tableid in ReservationTableListBox.Items)
                    {
                        if (item_tableid.Selected)
                        {
                            selectedTables.Add(byte.Parse(item_tableid.Text.Replace("Table ", "")));
                        }
                    }

                    //with all data gathered, connect to your library controller,
                    //and send data to processing

                    AdminController sysmgr = new AdminController();
                    sysmgr.SeatCustomer(when, reservationid, selectedTables, waiterid);

                    //refresh the page 
                    SeatingGridView.DataBind();
                    ReservationsRepeater.DataBind();
                    ReservationTableListBox.DataBind();

                }, "Customer Seated","Reservation customer has arrived and has been seated."
                );
        }

    }

    protected bool ShowReservationSeating()
    {
        bool anyReservationToday = false;
        //call the BLL to indicate if there any reservations today
        MessageUserControl.TryRun(() =>
            {
                DateTime when = Mocker.MockDate.Add(Mocker.MockTime);
                AdminController sysmgr = new AdminController();
                anyReservationToday = sysmgr.ReservationsForToday(when);
            }
            ); 
        return anyReservationToday;
    }
}