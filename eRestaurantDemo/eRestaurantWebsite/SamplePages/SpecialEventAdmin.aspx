<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SpecialEventAdmin.aspx.cs" Inherits="SamplePages_SpecialEventAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <table align="center" style="width: 70%">
        <tr>
            <td align ="right" style="width:50%">Select and Event:</td>
            <td>
                <asp:DropDownList ID="SpecialEventList" runat="server" AppendDataBoundItems="True" DataSourceID="ODSSpecialEvents" DataTextField="Description" DataValueField="EventCode">
                    <asp:ListItem Value="z">Select event</asp:ListItem>
                </asp:DropDownList>
                <asp:LinkButton ID="FetchRegistrations" runat="server">Fetch Registrations</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="ReservationList" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" DataSourceID="ODSReservations" AllowPaging="True">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField DataField="CustomerName" HeaderText="Name" SortExpression="CustomerName">
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ReservationDate" DataFormatString="{0:MMM dd, yyyy} " HeaderText="Date" SortExpression="ReservationDate">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumberInParty" HeaderText="Size" SortExpression="NumberInParty">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ContactPhone" HeaderText="Phone" SortExpression="ContactPhone">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ReservationStatus" HeaderText="Status" SortExpression="ReservationStatus">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        No data to display at this time
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" Font-Size="Large" />
                    <PagerSettings FirstPageText="Start" LastPageText="End" Mode="NumericFirstLast" PageButtonCount="4" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:DetailsView ID="ReservationListDV" runat="server" AllowPaging="True" AutoGenerateRows="False" DataSourceID="ODSReservations">
                    <EmptyDataTemplate>
                        No data to display at this time
                    </EmptyDataTemplate>
                    <Fields>
                        <asp:BoundField DataField="CustomerName" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ReservationDate" DataFormatString="{0:MMM dd, yyyy h:mm tt}" HeaderText="Date" />
                        <asp:BoundField DataField="NumberInParty" HeaderText="Size" />
                        <asp:BoundField DataField="ContactPhone" HeaderText="Phone" />
                        <asp:BoundField DataField="ReservationStatus" HeaderText="Status" />
                    </Fields>
                </asp:DetailsView>
            </td>
 
        </tr>
    </table>
    <asp:ObjectDataSource ID="ODSSpecialEvents" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SpecialEvents_List" TypeName="eRestaurantSystem.BLL.AdminController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSReservations" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetReservationByEventCode" TypeName="eRestaurantSystem.BLL.AdminController">
        <SelectParameters>
            <asp:ControlParameter ControlID="SpecialEventList" Name="eventcode" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

